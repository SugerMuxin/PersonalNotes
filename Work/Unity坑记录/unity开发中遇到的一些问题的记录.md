---
author: RuoSaChen
tags:
  - work
---

>1. SkinmeshRender  （一些使用了Skinmesh的对象未显示问题）
>  出现的问题是打出来的abb包中的一些使用了SkinmeshRender的模型没有显示出来。原因是在打包的过程中勾选了# stripEngineCode 选项，如果勾选了这个选项在打IL2CPP包的时，会去掉一些没有引用到的模块和脚本。
>  https://docs.unity3d.com/ScriptReference/PlayerSettings-stripEngineCode.html   出问题就是在整个工程的代码中没有 SkinnedMeshRenderer 的任何引用，可以在项目中添加如下代码就可以解决了。
>  {
> 	 SkinnedMeshRenderer = null;
>  }
>  （这个说明打包的时候Untiy进行了一次全脚本的类型检测，检测的结果可能保存在一个Dict中，其中包括类型、dll等信息，打包的时候会去检测是否将对应的dll打进包，以此来删除不必要的依赖）

>2. abb包大小会限制200M![[{F1ABEDC0-DA6F-484D-B6E3-A0461A04432B} 1.png]]
>原因：gp审核对abb包下的base文件夹做了限制
>解决方法： 勾选  PublisSettings/SplitApplicationBinary  即可


>3.图片不显示问题（IOS上未知的图片不显示问题）
>![[Pasted image 20240531095309.png]]

>4. 在使用 BoxCollider 检测子弹或者技能是否打中对象的时候，出现真机上不能检测的问题。原因是开发中添加的 Tag 之后并未上传 TagManager.assest 。而在碰撞检测中使用了 该Tag 。但是后来上传之后还是在真机上不能检测成功，而在 后来的 LOG 日志中并没有找到对应的 Enemy 层。后来在敌人的预制体上检查发现一些预制体出现脚本丢失的情况（按道理资源是不会出现影响这种结果的），修复之后在打包就成功检测了。![[Pasted image 20250425173403.png]]
>

5. 泰语的一些文字不显示问题（因为FGUI使用的自定义字体问题），在UI中设置的自定义字体，删除UI上的默认设置就行了
>![[Pasted image 20250516141143.png]]



6. 在新版本发布之后的短时间内出现很多的玩家不能进入游戏的情况，通过打点发现的在登录环节出现大量的玩家丢失情况。
		原因:     1. Google在不同地区的商店更新时间不同步
			2. 登录失败，连接不上服务器
			3. ![[Pasted image 20250523165329.png]]