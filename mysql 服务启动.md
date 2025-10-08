
1. 启动命令

```
dotnet run
```

   2. 启动之后可能会出现如下报错,从图中可以看出是和https的协议有关，此时需要生成https的证书
    ![[Pasted image 20250612053319.png]]
```
PS D:\A-Ruosa\FrameworkServer\AuthService> dotnet dev-certs https --trust
PS D:\A-Ruosa\FrameworkServer\AuthService> dotnet dev-certs https --check
PS D:\A-Ruosa\FrameworkServer\AuthService> dotnet run
```




以下的编码问题怎么解决，具体肯定是两台电脑的编码方式不同

![[Pasted image 20250612061842.png]]