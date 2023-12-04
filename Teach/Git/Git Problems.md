>1.连接失败
>使用git同步仓库中遇到的问题记录,常常会遇到无法连接到仓库的问题，这个很多情况是由于VPN的网络或者代理造成的。首先可以多试两次，这时候可能连接成功，如果不能连接成功的话使用如下的命令来取消SSL验证

```
git config --global http.sslVerify "false"
git config --global https.sslVerify "false"
git config http.postBuffer 524288000
```


>2.Fatal error
>如果出现下图的报错问题,这个原因则是因为仓库不支持代理服务器的原因，这时候我们需要关闭代理。方法是使用如下的命令
![[gitpull_err.png]]

```
git config --global --unset http.proxy 
git config --global --unset https.proxy
```


<<<<<<< HEAD
>3. Clear Cache
>超过了50M的PDF上传超过了git的限制，但是删除之后依然报错。大概是缓存的问题。
>![[push_file_err.png]]

=======
>>>>>>> 865b1e225242febea56dd60cf8fafbf22c53de51


>1.Clone仓库
>    git init
>	git clone https://github.com/SugerMuxin/Peanut_Notes.git
>	
>    问题：        10054报错的问题  OpenSSL SSL_read: Connection was reset, errno 10054
       解决办法：修改设置，解除ssl验证 之后再执行**git**操作即可。
       ` git config --global http.sslVerify "false" `
       

>2.Push
>	git add .
>	git commit -m "your description"
>	git push


>3.Pull
>    git pull

私有仓库的Clone方法
