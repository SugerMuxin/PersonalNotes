一.安装Open - WebUI

  step1:  双击installer_files 安装一些需要的文件
	|-- conda
	|-- env
	|-- pytorch

  step2:
	双击 cmd_windows.bat 执行完成后输入以下命令
		pip install open-webui

		/---------------------------/
                等待安装完成--预计几分钟左右
		/---------------------------/

  step3:	
	安装完成会看到一个 http://0.0.0.0:8080的字样，这就是安装成功了
	然后就可以启动服务了，启动服务通过以下命令
		open-webui serve

  step4:  在浏览器中输入http:localhost:8080 

二. 安装Ollama
 	1.官网下载安装即可
	2.执行 set_path.bat（这一步是为了设置模型的配置PATH）
        3.去官网下载需要的模型