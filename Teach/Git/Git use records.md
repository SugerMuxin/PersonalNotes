
1. common git commands

![[git_common_command.jpg]]

2.如果出现如下错误，可以多试几次，是因为网络问题造成的，因为github的服务器在国外，容易出现连接超时等网络问题

![[git_push_error1.png]]

如果出现下图这个错误，可以打开Git命令页面，执行git命令脚本：修改设置，解除ssl验证

git config --global http.sslVerify "false"
![[git_pusherr.png]]


2 如何使用Token来拉取私有仓库

>步骤1：甚至一个Token
>步骤2：使用Tocken代替身份验证来拉取私有仓库的资源

https://www.educative.io/answers/how-to-clone-a-private-repository-from-github

设置Token的方法
https://stackoverflow.com/questions/2505096/clone-a-private-repository-github

command:
git clone https://oauth2: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@github.com/SugerMuxin/Peanut_Notes.git
格式类似于  git clone https://oauth2:<AuthToken>@github.com/SugerMuxin/Peanut_Notes.git




