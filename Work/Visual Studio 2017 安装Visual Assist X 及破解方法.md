   pojie步骤

**步骤0**--------------------------------------------------------------------------------------------------------------------

0. 如果之前装了Visual assist需要先卸载掉，工具---拓展与更新---找到visual assist 点击卸载，确定。关闭vs编辑器，然后等待一下，会出现一个弹框，点击modify。

1. 下载visual assist
		![[Pasted image 20240708085246.png]]

 2. 双击上图exe文件，选择要pojie的版本
![](https://img-blog.csdnimg.cn/2020033120321232.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1N0cnV1Z2xlX0d1eQ==,size_16,color_FFFFFF,t_70)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")​编辑
3.将第一步Crack文件夹中的VA_X.dll 复制到

C:\Users\**YourName**\AppData\Local\Microsoft\VisualStudio\**15.0_c7ce1ac1**\Extensions\**5l3tatws.3ca** 目录下

其中红色字体部分的文件名是生成的，所以都不同。替换掉该目录下的VA_X.dll文件
![](https://img-blog.csdnimg.cn/20200331203750403.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1N0cnV1Z2xlX0d1eQ==,size_16,color_FFFFFF,t_70)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")​
如果一切顺利的话，到这一步就结束了。

**尝试1**----------------------------------------------------------------------------------------------------------------------

但是如果之前装了其他的版本的vs，也pojie过，当你再次pojie的时候可能就会出现如下的错误

The security key for this program currently stored on your system does not appear to be valid for this version of the program. Select Yes to enter a new key, or No to revert to the default setting (if any).
![](https://img-blog.csdnimg.cn/20200331204421956.png)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")​
这个问题自然是因为装的visual assist 导致的。所以我卸载了它。重新找了一个版本pojie。重新pojie，but涛声依旧。

**尝试2**----------------------------------------------------------------------------------------------------------------------

于是我再次卸载了visual assist，去看了C:\Users\**YourName**\AppData\Local\Microsoft\VisualStudio\**15.0_c7ce1ac1**\Extensions\，发现文件也都被删除了

然后去注册表中查找（命令行regedit），找到了下图中Whole Tomato，并将其删除，然后重新pojie。but涛声依旧。

![](https://img-blog.csdnimg.cn/20200331205852743.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1N0cnV1Z2xlX0d1eQ==,size_16,color_FFFFFF,t_70)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")![](https://img-blog.csdnimg.cn/20200331205921490.png)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")​
​

 **步骤3**---------------------------------------------------------------------------------------------------------------------

同样先重复了一次**尝试2**

**下载注册表清理工具trial-reset** 链接：[http://www.pc6.com/softview/SoftView_105357.html](https://link.jianshu.com/?t=http%3A%2F%2Fwww.pc6.com%2Fsoftview%2FSoftView_105357.html "http://www.pc6.com/softview/SoftView_105357.html")   

并下载了注册表清理工具，

右击Trial-Reset.exe管理员权限运行，即可安装，这时，可能会出现下面的提示：缺少 mscomctl.ocx 

mscomctl.ocx 下载链接：[http://www.pc6.com/softview/SoftView_64068.html](https://link.jianshu.com/?t=http%3A%2F%2Fwww.pc6.com%2Fsoftview%2FSoftView_64068.html "http://www.pc6.com/softview/SoftView_64068.html")

![](https://img-blog.csdnimg.cn/20200331210206926.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1N0cnV1Z2xlX0d1eQ==,size_16,color_FFFFFF,t_70)![](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw== "点击并拖拽以移动")​
点击图中的绿色图标，然后等一会，等右边检索结束。选中每一项Armadillo，右键选择删除键值，此时状态由 已找到 变成 已删除!注册表清理后，然后在重新步骤0破解，打开项目，可正常使用！

**总结一下：**

        **如果正常安装的就是步骤0就可以了**

        **如果出现错误呢，就先执行步骤3，再执行步骤0。**
​