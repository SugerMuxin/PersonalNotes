using UnityEngine;
using System.Collections;
using LTNet;

public class RoomManager : MonoBehaviour{

    public RoomList RoomList
    {
        get;
        set;
    }

    void Start()
    {
		NetworkManager.Self.RegisterHandler<UpdateRoomListResponse>(this.UpdateRoomList);
		NetworkManager.Self.RegisterHandler<EnterRoomResponse>(this.UpdatePlayerList);
		NetworkManager.Self.RegisterHandler<StartLoadingResponse>(this.LoadingGameScene);
    }

    public void UpdateRoomList(Message msg)
    {
        UpdateRoomListResponse data = (UpdateRoomListResponse)msg.GetMessageData();
        RoomList.lv_room.Items.Clear();
        for (int i = 0; i < data.mRoomIdList.Count; i++)
        {
            RoomList.lv_room.Items.Add(data.mRoomIdList[i]);
        }
    }

    public void UpdatePlayerList(Message msg)
    {
        RoomList.lv_player.Items.Clear();
        PlayerVOManager.Self.mPlayerList.Clear();

        EnterRoomResponse data = (EnterRoomResponse)msg.GetMessageData();
        for (int i = 0; i < data.mUserNumber; i++)
        {
            string str = data.mUsernameList[i] + "---------" + data.mUserIdList[i].ToString();
            RoomList.lv_player.Items.Add(str);

            PlayerVO temp_vo = new PlayerVO();
            temp_vo.mUsername = data.mUsernameList[i];
            temp_vo.mUserId = data.mUserIdList[i];
            PlayerVOManager.Self.mPlayerList.Add(temp_vo);
            
        }
        RoomList.state = 1;
    }

	public void LoadingGameScene(Message msg)
	{
		StartLoadingResponse data = (StartLoadingResponse)msg.GetMessageData();
		for (int i = 0; i < data.mUserNumber; i++ )
		{
			DebugUtil.Log("userId: " + data.mUserList[i]);
			DebugUtil.Log("username: " + data.mUsernameList[i]);
			
			PlayerVO temp_pv = PlayerVOManager.Self.GetPlayerById(data.mUserList[i]);
			if (temp_pv != null)
			{
				temp_pv.mPosition = data.mPostionList[i];
			}
		}
		
		SceneManager.Instance.LoadScene("Demo", () =>{
			EndLoadingRequest req = new EndLoadingRequest();
			req.Send(NetworkManager.Connector);
			//与服务器对时
			TimeManager.Self.RequestServerTime();
		});   
	}

}
