using UnityEngine;
using System.Collections;
using LTNet;
using System.Net;
using System.IO;
using LTUnityPlugin;

public class LoginBtn : MonoBehaviour {

	void Start()
	{
		NetworkManager.Self.RegisterHandler<EnterGameResponse>((message) => {
			Application.LoadLevel("Room");
		});
	}

    void OnClick()
    {
        EnterGameRequest enterReq = new EnterGameRequest(ulong.Parse(PluginManager.PluginInstance<AccountCenter>().JoyId));
		enterReq.Send(NetworkManager.Connector);
    }
}
