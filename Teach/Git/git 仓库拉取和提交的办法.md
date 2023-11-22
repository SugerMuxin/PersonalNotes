1.私有仓库

其实设置了用户名和邮箱好像是可以直接拉取的。但是按道理是不应该，所以还需要一层保障，这个保障就是PersonalToken

如果clone一个公有仓库的命令是
git clone https://github.com/SugerMuxin/PublicPersonalNotes.git
那么clone 一个私有仓库的代码命令就是
git clone https://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@github.com/SugerMuxin/PersonalNotes.git

其中 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就是你的个人PersonalToken，这个令牌是在github上生成的，具体怎么生成这里不做记录了。

到这一步基本上clone是没什么问题了。但是现在我想做的是本地做了一些修改怎么提交到github上。可以使用如下的命令进行上传

>  git add . 
(这个命令是上传本地的所有修改，如果只上传一部分的修改的话可以到指定的目录下去执行此命令)
  
  >git commit -m "description"
  >description 是你的提交说明，就是一个提交日志，这个时候控制台会log出你需要提交的部分
  
  >git push 
  >到这一步就直接上传了
  
  在这个过程中可能会遇到一些问题，比如
  ![[git_err.bmp]]
 或者
 ![[git_err3.bmp]]
总之呢，这两种报错都是因为你连不上github服务器，这有可能是网络真的连不上造成的，因为服务器在国外嘛。这时候可以开梯子试试。也可以打开github网页，先确认下自己能否登录成功等来辅助验证。

也有可能是开了系统代理的缘故，这时候可以使用如下的命令进行一次尝试
```
git config --global http.proxy http://127.0.0.1:7890
git config --global https.proxy http://127.0.0.1:7890

```

如果不行也可以关闭代理再试试，总之到了这一步就只能解决网络上的问题了，配置上已经没有问题了
```
git config --global --unset https.proxy
git config --global --unset http.proxy
```