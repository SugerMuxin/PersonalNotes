
>1. SkinmeshRender  （一些使用了Skinmesh的对象未显示问题）
>  出现的问题是打出来的abb包中的一些使用了SkinmeshRender的模型没有显示出来。原因是在打包的过程中勾选了# stripEngineCode 选项，如果勾选了这个选项在打IL2CPP包的时，会去掉一些没有引用到的模块和脚本。
>  https://docs.unity3d.com/ScriptReference/PlayerSettings-stripEngineCode.html   出问题就是在整个工程的代码中没有 SkinnedMeshRenderer 的任何引用，可以在项目中添加如下代码就可以解决了。
>  {
> 	 SkinnedMeshRenderer = null;
>  }

>2. abb包大小会限制200M![[{F1ABEDC0-DA6F-484D-B6E3-A0461A04432B} 1.png]]
>原因：gp审核对abb包下的base文件夹做了限制
>解决方法： 勾选  PublisSettings/SplitApplicationBinary  即可

