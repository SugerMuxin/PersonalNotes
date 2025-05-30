using UnityEngine;
using System.Collections;

namespace LTUnityPlugin {
public class PluginLog {

    private static bool LogOpen = true;

    public static bool OpenLog {
        set {
            LogOpen = value;
        }
    }

    public static void Log(object message, Object content) {
        if(LogOpen) {
            Debug.Log("Info >> " + message, content);
        }
    }
    public static void Log(object message) {
        if(LogOpen) {
            Debug.Log("Info >> " + message);
        }
    }

    public static void Err(object message) {
        if(LogOpen) {
            Debug.LogError("Error >> " + message);
        }
    }
}
}
