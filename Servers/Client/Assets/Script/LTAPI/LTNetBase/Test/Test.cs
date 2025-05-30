using UnityEngine;
using System.Collections;
using LTNet;
using System.Net;
using System.IO;

public class Test : MonoBehaviour {

	SocketConnector connector;
	string userId;
	string token;

	void Awake()
	{
		ProtoManager.Instance.AddProtocol<ServerRedirResponse>(0xFF, 0);
		ProtoManager.Instance.AddProtocol<Response>(0x1, 0x3);

		//发起http请求获得token，user id
		string uri = "http://netgame.joymeng.com/account/login?uname=qlj004&password=qlj004";
		HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
		request.Method = WebRequestMethods.Http.Get;
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string jsonResp = reader.ReadToEnd();
		response.Close();

		MyJson.JsonNode_Object  obj = 	(MyJson.JsonNode_Object)MyJson.Parse(jsonResp);
		userId = obj["content"].asDict()["uid"].AsString();
		token = obj["content"].asDict()["token"].AsString();
	}

	// Use this for initialization
	void Start () {
		MessageDispatcher.Instance.RegisterHandler(this.OnMessageReceived);
		connector = gameObject.AddComponent<SocketConnector>();
		connector.BindSocket(new NetSocket("wuxiadns.joymeng.com", 60000));
		connector.UsingAsync = true;
		connector.RegisterConnectCallback(ConnectDone, null);
		connector.Connect();
	}

	public void ConnectDone(SocketConnector connector, object userdata)
	{
		if (connector.IsConnected())
			DebugUtil.Log("connected successfully");
		else
			DebugUtil.Log("conntection failed");
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
			//do something
		}
	}
}