---
author: RuoSaChen
tags:
  - teach
---


>1. 安装 
>     我是安装过程中出现了错误，看错误日志是我出现了文件找不到的情况，然后看到这一些奇怪的类汉字，有应该是出现了编码问题。查了下有可能是下载的安装文件不完整，然后我又重新下载了一次，发现大小一样，再安装也是一样的错误。  于是于是我下载了一个不同的版本(老版本)，在这之前我已经按照如下的链接视频操作了，还是卡住了（每次都是在如下途中初始化数据库的位置失败的）。
>     ![[Pasted image 20250608131539.png]]
```
Attempting to run MySQL Server with --initialize-insecure option... Starting process for MySQL Server 8.0.42... Starting process with command: D:\Program Files (x86)\MySQL\bin\mysqld.exe --defaults-file="C:\ProgramData\MySQL\MySQL Server 8.0\my.ini" --console --initialize-insecure=on --lower-case-table-names=1... D:\Program Files (x86)\MySQL\bin\mysqld.exe (mysqld 8.0.42) initializing of server in progress as process 1740 mysqld: File '.\濞屽湱鍋㈤懞杈╂晸缁?bin.index' not found (OS errno 2 - No such file or directory) The designated data directory C:\ProgramData\MySQL\MySQL Server 8.0\Data\ is unusable. You can remove all files that the server added to it. Aborting
```

	https://www.bilibili.com/video/BV1jcabemEr7/?spm_id_from=333.1007.top_right_bar_window_default_collection.content.click

>3. 安装完成后连接mysql的方式
	. 以管理员身份运行CMD命令行 （当然前提是配置了正确的环境变量）
		mysql -u root -p
		Enter password: ******
    . 通过MySQL ComandLine 点击之后直接输入密码即可