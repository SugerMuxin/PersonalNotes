using UnityEngine;
using System.Collections;
using System.IO;

namespace LTUnityPlugin {
public class PluginMgrTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
        PluginManager.PluginInstance<NetSDKDemo>();
    }

    void Update() {
    }

    void Test() {
        
    }

}
}