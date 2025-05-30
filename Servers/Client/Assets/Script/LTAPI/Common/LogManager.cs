using UnityEngine;
using System.Collections;
using System.IO;
using LTNet;

public class LogManager : MonoBehaviour {

    public static LogManager Self;

    FileStream mFileStream;
    StreamWriter mStreamWriter;

    public bool ShowLogInConsole = true;

    void Awake()
    {
        Self = this;
        string path = Path.Combine(Application.dataPath,"../log.txt");
        mFileStream = new FileStream(path, FileMode.Create);
        mStreamWriter = new StreamWriter(mFileStream);
    }

    public void LogInfo(string format , params object [] args)
    {
       string log =  string.Format(format, args);
		if (ShowLogInConsole)
       {
           DebugUtil.Log(log);
       }     
       mStreamWriter.WriteLine(log);
       mStreamWriter.Flush();
       mFileStream.Flush();
    }

    
    void OnDestroy()
    {
        mFileStream.Flush();
        mStreamWriter.Flush();
        mFileStream.Close();
        mStreamWriter.Close();
    }

}
