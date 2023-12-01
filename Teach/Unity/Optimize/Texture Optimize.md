
<h2><font color="green">一. Tips</font></h2>
>1.能用小图的用小图，能九宫格的九宫格
   2.不用透明通道的删除透明通道
   3.不需要Mipmap的就去掉sRGB选项
   4.使用对应平台的压缩方式


<h2><font color="green">二,Texture属性设置</font></h2>
>1. Read/Write Enable
  ：<strong><font color="yellow">禁用</font></strong>，除非你再运行时需要读取像素的信息，因为Unity需要一块内存来保存一组图像字节格式来供读写。

>2. Generate Mip Maps
 ：3D的贴图才需要使用Mipmap，否则会多出<font color="red">33%</font>的内存开销。Mipmap主要是为了远处的对象使用小而且更合理的贴图，防止出现摩尔纹的现象。像UI贴图则完全不需要。场景较大或开放世界类的游戏，其场景中存在的大量的贴图（比如基础颜色纹理贴图、法线贴图、高光贴图、粗糙度贴图等），再加上为了使远景看起来不那么失真和避免摩尔纹，会对贴图采用mipmap技术，Texture的内存占用会多33%左右。（为什么是0.33 [click](https://zhuanlan.zhihu.com/p/36939174)） 
 >
> 虽说只多了大概0.33倍的内存，但是对于大地图、超多纹理的场景来说，这个内存也是比较恐怖的，Unity 提供了Texture Streaming（纹理串流）来解决这一问题.Texture Streaming可以控制 Unity 加载到内存中的 Mipmap 级别。该系统可减少 Unity 对于纹理所需的内存总量，因为它只加载 Unity 在场景中渲染当前摄像机位置时所需的 Mipmap，而不是默认加载所有 Mipmap。该系统通过消耗少量 CPU 资源来节省潜在的大量 GPU 内存。

![[Unity-Opetimize-Stream.bmp]]

>因为在开放地图中很多远处的场景的贴图可能很久都只用得上级别很low的mipmap，所以完全没必要加载包含每个level的贴图，而只需要加载对应level的需要用到的贴图。(这个功能得在2019往后的版本才有)
>
>3. Override for IOS/Android : 启用，为特定平台做差异化配置。
