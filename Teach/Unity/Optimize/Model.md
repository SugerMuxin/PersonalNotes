针对FBX文件的属性设置

>1. Mesh Compression : 压缩比越高模型文件越小，需要根据游戏内的实际效果决定，一般可以设置为Medium

>2. Read/Write Enable : 和Texture的Read/Write类似，如果不需要再运行时修改模型的网格，就无须R/W

>3. Optimize Mesh：推荐启用，可以提升GPU性能。但是Unity好像已经弃用了。

>4. Normals：如果你的模型没有法线信息，将其设为None，可以减小模型大小。

```
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This method is no longer supported (UnityUpgradable)", true)]
public void Optimize();
```

<font  
color="yellow"  
size="2">Mesh Comparession 必须在开启Dynamic batching之后才能生效。如果开启了R/W会使得压缩失效。另外，如果模型的定点数小于300是不会压缩的，因为在解压缩的过程中也会带来额外的性能开销，这就没有必要了，当然300这个值不是固定的，随着版本而不同。</font>

<h2><font color="green">Rig</font></h2>

>1. Animation Type: 如果你的模型没有骨骼，将其设为None
>
>2. Optimize Game Objects : 启用，可以将暴露在Hierarchy的子节点移除，极大的减少了模型的层级和Children数量，从而提升运行时性能。如果有挂载点需求，在Extra Transforms to Expose 里添加需要暴露的子节点即可。

<h2><font color="green">Audio</font></h2>
>1. Force To Momo audio会被向下融合为单声道，信号峰值会被减弱，融合后的信号会比原信号弱。但是能极大的减少文件的大小。

>2. Load Type Decompress OnLoad 这种格式的音频会被强制预加载，一个场景用到的音频会在场景加载的时候全部加载到内存。坏处是占用了一定的内存开销，好处是能够减少在解压音频时巨大的临时性能消耗（Cpu and memory）。Compressed In Memory 音频文件以压缩格式存放于内存中，一边播放一边进行解压。这种模式的内存开销会比前一种稍小，但是播放时的CPU开销会较之更大。Streaming：音频文件不会被加载到内存，只有即将播放的一小段才会被读取到内存中。这种模式的内存开销最低，但是CPU开销也最大，因为其伴随着大量的磁盘读写操作和解压缩。

>3. Compression Format
- `PCM`：完全不压缩格式，占据的硬盘和内存相对会较大，由于运行时不需要解压，所以它的CPU开销最小。
- `ADPCM`：一种古老的压缩格式，相对于`PCM`的压缩比为`3.5:1`，但是运行时的解压开销很小，对于音质有一定损耗。
- `Vorbis/MP3`：常见的压缩格式，主流平台全部支持的格式，压缩比较高，但是运行时的解压缩开销较大，对于音质的损耗更加严重。在iOS平台上一般设置为`MP3`，因为iOS支持`MP3`格式的硬解码。
