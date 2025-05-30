using UnityEngine;
using System.Collections;
using LTNet;
using System.Net;
using System.IO;

public class P2PTest : MonoBehaviour {

	string userId = string.Empty;
	string appId = string.Empty;

	string token;
	SocketConnector connector;

	P2PPlayer P2P_Self = null;
	P2PPlayer P2P_Dest = null;

	void Awake()
	{
		ProtoManager.Instance.AddProtocol<ServerRedirResponse>(0xFF, 0);
		ProtoManager.Instance.AddProtocol<Response>(0x1, 0x3);

		ProtoManager.Instance.AddProtocol<P2P_StartShakingHand>(P2PProtocolDefine.P2P_PROTOCOL, P2PProtocolDefine.START_SHAKING_HAND);
		ProtoManager.Instance.AddProtocol<P2P_ShakingHandConfirmed>(P2PProtocolDefine.P2P_PROTOCOL, P2PProtocolDefine.SHAKING_HAND_CONFIRMED);
		ProtoManager.Instance.AddProtocol<P2P_EndShakingHand>(P2PProtocolDefine.P2P_PROTOCOL, P2PProtocolDefine.END_SHAKING_HAND);
		ProtoManager.Instance.AddProtocol<P2P_Test>(P2PProtocolDefine.P2P_PROTOCOL, P2PProtocolDefine.TEST_ONLY);
	}

	// Use this for initialization
	void Start () {
		MessageDispatcher.Instance.RegisterHandler(this.OnMessageReceived);
	}

	public void ConnectDone(SocketConnector connector, object userdata)
	{
		if (connector.IsConnected())
			DebugUtil.Log("connected successfully");
		else
			DebugUtil.Log("conntection failed");
	}

	int state = 0;//0 - 未登陆， 1-登陆后， 2-申请匹配
	string dataToSend = string.Empty;

	void OnGUI()
	{
		switch(state)
		{
		case 0:
			GUI.Label(new Rect(20, 20, 50, 20), "User ID");
			userId = GUI.TextField(new Rect(80, 20, 150, 20), userId);
			GUI.Label(new Rect(20, 60, 50, 20), "App ID");
			appId = GUI.TextField (new Rect(80, 60, 150, 20), appId);
			if (GUI.Button(new Rect(20, 100, 50, 20), "登陆"))
			{
				//发起http请求获得token
				string uri = "http://hijoywar.joymeng.com/check/login?uid=" + userId + "&appid=" + appId;
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
				request.Method = WebRequestMethods.Http.Get;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string jsonResp = reader.ReadToEnd();
				response.Close();
				
				MyJson.JsonNode_Object  obj = 	(MyJson.JsonNode_Object)MyJson.Parse(jsonResp);
				token = obj["token"].AsString();

				connector = gameObject.AddComponent<SocketConnector>();
				connector.BindSocket(new NetSocket("10.80.1.99", 60000));
				connector.UsingAsync = true;
				connector.RegisterConnectCallback(ConnectDone, null);
				connector.Connect();
			}
			break;
		case 1:
			if (GUI.Button(new Rect(20, 20, 80, 20), "申请对战"))
			{
				string uri = "http://hijoywar.joymeng.com/hall/matching?token=" + token;
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
				request.Method = WebRequestMethods.Http.Get;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string jsonResp = reader.ReadToEnd();
				response.Close();
				
				MyJson.JsonNode_Object  obj = 	(MyJson.JsonNode_Object)MyJson.Parse(jsonResp);
				int status = obj["status"].AsInt();
				
				DebugUtil.Log("matching response from web: " + status);
				
				if (status == 2)//matching successful
				{
					string strUId = obj["uid"].AsString();
					string strCId = obj["cid"].AsString();
					string strEId = obj["eid"].AsString();
					
					P2P_Dest = new P2PPlayer(ulong.Parse(strUId), uint.Parse(strCId), uint.Parse(strEId));
					
					//send hand-shaking message
					(new P2P_StartShakingHand()).Send(this.P2P_Self, this.P2P_Dest, this.connector);
				}
			}
			break;
		case 2:
			GUI.Label(new Rect(20, 20, 80, 20), "对战中...");
			dataToSend = GUI.TextField(new Rect(20, 40, 80, 20), dataToSend);
			if (GUI.Button(new Rect(120, 40, 80, 20), "发送消息"))
			{
				(new P2P_Test(uint.Parse(dataToSend))).Send(this.P2P_Self, this.P2P_Dest, this.connector);
			}
			break;
		default:
			break;
		}
	}

	public void OnMessageReceived(Message message)
	{
		DebugUtil.Log("got a message, protocol: " + message.GetProtocol());

		if (message.GetProtocol() == 0xFF)
		{
			ServerRedirResponse redirMsg = (ServerRedirResponse) message.GetMessageData();

			connector.BindSocket(new NetSocket(redirMsg.GetRedirHost(), redirMsg.GetRedirPort(), null));
			connector.UsingAsync = false;
			connector.Connect();

			LoginRequest loginReq = new LoginRequest(ulong.Parse(userId), token);
			loginReq.Send(connector);
		}

		if (message.GetProtocol() == 0x1)
		{
			Response resp = (Response)message.GetMessageData();
			if (resp.mMessageId == 0x3 && resp.mResult == 0)
			{
				state = 1;//login succeeded
				P2P_Self = new P2PPlayer(resp.mReserved_1, resp.mReserved_2, resp.mReserved_3);
			}
		}

		//p2p messages
		if (message.GetProtocol() == 0x2)
		{
			P2PMessage p2pMsg = (P2PMessage)message.GetMessageData();

			if (p2pMsg.mMessageId == P2PProtocolDefine.START_SHAKING_HAND)
			{
				DebugUtil.Log("received hand-shaking message, send out confirmed message");
				if (this.P2P_Dest == null)
				{
					this.P2P_Dest = new P2PPlayer(p2pMsg.mSrcUId, p2pMsg.mSrcCId, p2pMsg.mSrcEId);
				}

				(new P2P_ShakingHandConfirmed()).Send(this.P2P_Self, this.P2P_Dest, this.connector);
			}

			if (p2pMsg.mMessageId == P2PProtocolDefine.SHAKING_HAND_CONFIRMED)
			{
				state = 2;//match succeeded
				DebugUtil.Log("received hand-shaking confirmed message, send out hand-shaking end message");
				(new P2P_EndShakingHand()).Send(this.P2P_Self, this.P2P_Dest, this.connector);
			}

			if (p2pMsg.mMessageId == P2PProtocolDefine.END_SHAKING_HAND)
			{
				state = 2;//match succeeded
				DebugUtil.Log("hand-shaking done");
			}

			if (p2pMsg.mMessageId == P2PProtocolDefine.TEST_ONLY)
			{
				P2P_Test testMsg = (P2P_Test)p2pMsg;
				DebugUtil.Log(string.Format("receive message from user: {0}, data: {1}", testMsg.mSrcUId, testMsg.mData));
			}
		}
	}
}