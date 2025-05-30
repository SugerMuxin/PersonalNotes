using UnityEngine;
using System;

namespace LTUnityPlugin {
public class NetSDKDemo : MonoBehaviour {

    public string userServer = "http://10.80.1.87:8088/user/";

    public string redirctServer = "10.80.1.87:60000";

    public string connectServer = string.Empty;

    public uint gameServerInstance = 1;

    void Start() {
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            PluginManager.PluginInstance<NetSDK>().Quit();
        }
    }

    void OnGUI() {
        float scale = 3.0f;

        if(Application.platform == RuntimePlatform.IPhonePlayer) {
            scale = Screen.width / 320;
        }

        float btnWidth = 200 * scale;
        float btnHeight = 45 * scale;
        float btnTop = 10 * scale;
        GUI.skin.button.fontSize = Convert.ToInt32(16 * scale);

        btnTop += btnHeight + 10 * scale;
        if(GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "初始化")) {
            AppInfo.Instance.SetKeyValue(AppInfo.Key.UserServer, userServer);
            AppInfo.Instance.SetKeyValue(AppInfo.Key.ServerUrl, redirctServer);
            PluginManager.PluginInstance<AccountCenter>().ServerInstanceId = gameServerInstance ;
        }

        btnTop += btnHeight + 10 * scale;
        if(GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "快速注册&登录")) {
            PluginManager.PluginInstance<NetSDK>().Init();
            if(PluginManager.PluginInstance<AccountCenter>().Count == 0) {
                PluginManager.PluginInstance<AccountCenter>().QuickReg();
            } else {
                Account acount = PluginManager.PluginInstance<AccountCenter>()[0];
                PluginManager.PluginInstance<AccountCenter>().Login(acount.Username, acount.Password);
            }
        }

        btnTop += btnHeight + 10 * scale;
        if(GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "测试连接")) {
            string[] serurl = new string[] { "10.80.1.87", "40000" };
            NetworkManager.Connector.BindSocket(new LTNet.NetSocket(serurl[0], int.Parse(serurl[1])));
            NetworkManager.Connector.UsingAsync = false;
            NetworkManager.Connector.Connect();
        }


        btnTop += btnHeight + 10 * scale;
        if(GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "数据发送")) {
            LiteRequest enterReq = new LiteRequest(NetProtocols.NTC_DTCD_ENTER_IN_TEST, "w", new object[] { "Hello Server!" });
            enterReq.Send(NetworkManager.Connector);
        }
    }
}
}