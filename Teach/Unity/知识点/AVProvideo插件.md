---
tags:
  - unity
---


1. 记录AVProvideo 插件的使用




2. AssetsBundle热更新视频的方式
><span style="color:green;">setp1.</span>采用的方案是将所有的视频资源更新到一个或者多个bundle中，当然这视情况而定。(直接将视频源的后缀修改为 .bytes 这样不会损失视频信息)
><span style="color:green;">setp2.</span> 热更新到视频相关的ab，使用流的方式读取到 Application.persistentDataPath
><span style="color:green;">step3.</span> 通过path加载 
>videoPlayer.OpenMedia(new MediaPath(@videoId, MediaPathType.RelativeToPersistentDataFolder), false);

```
    public string inputFileName = "Mafia_PV_Sniper01.bytes"; // Your .bytes file
    public string outputFileName = "Mafia_PV_Sniper01.mp4"; // Output video file name

    /// <summary>
    /// 测试视频的ab热更新//
    /// </summary>
    public void StartConvert()
    {
        string inputPath = Path.Combine(Application.dataPath, inputFileName);
        string outputPath = Path.Combine(Application.persistentDataPath, outputFileName);

        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "video"));
        TextAsset txt = bundle.LoadAsset("Mafia_PV_Sniper01", typeof(TextAsset)) as TextAsset;
        byte[] bytes = txt.bytes;

        using (Stream videoStream = new FileStream(outputPath, FileMode.Create))
        {
            BinaryWriter videoStreamWriter = new BinaryWriter(videoStream);
            videoStreamWriter.Write(bytes, 0, bytes.Length);
        }
    }
```