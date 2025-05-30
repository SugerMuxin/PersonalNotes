using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LTUnityPlugin {
class MainDemo : MonoBehaviour {

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
        if(GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "初始化&加载主场景")) {
            DeviceInfo.Instance.Init();
            AppInfo.Instance.Init();
            Application.LoadLevel(PluginManager.GetMainScene());
            this.enabled = false;
        }
    }
}
}
