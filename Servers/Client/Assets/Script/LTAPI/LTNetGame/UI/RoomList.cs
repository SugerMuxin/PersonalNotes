using UnityEngine;
using System.Collections;
using LTNet;

public class RoomList : MonoBehaviour {

    public  ListView lv_room = new ListView();
    public  ListView lv_player = new ListView();
    public  int state = 0;

    void Awake()
    {
      RoomManager rm =  gameObject.AddComponent<RoomManager>();
      rm.RoomList = this;
    }

    void display_roominfo()
    {
        if (GUILayout.Button("获取房间列表", GUILayout.Width(100), GUILayout.Height(40)))
        {
            UpdateRoomListRequest req = new UpdateRoomListRequest();
            req.Send(NetworkManager.Connector);
        }

        lv_room.OnGui();

        if (GUILayout.Button("进入房间", GUILayout.Width(100), GUILayout.Height(40)))
        {
            if (lv_room.SelectIndex != -1)
            {
                int room_id = (int)lv_room.Items[lv_room.SelectIndex];
                EnterRoomRequest req = new EnterRoomRequest((uint)room_id);
                req.Send(NetworkManager.Connector);
            }
        }
    }
	
    void display_playerinfo()
    {
        lv_player.OnGui();
        if (GUILayout.Button("开始游戏", GUILayout.Width(100), GUILayout.Height(40)))
        {
            Debug.Log("enter game");
            ReadyGameRequest req = new ReadyGameRequest();
            req.Send(NetworkManager.Connector);
            state = 2;
        }
    }

    void display_wait()
    {
       GUILayout.Label("等待其他玩家...", GUILayout.Width(100), GUILayout.Height(40));
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        switch(state)
        {
            case 0:
             display_roominfo();
             break;
            case 1:
             display_playerinfo();
             break;
            case 2:
             display_wait();
             break;
        }
           
        GUILayout.EndVertical();
    }
}
