ubuntu 18

①--WSL  升级------------
|  

-------------------------



②--WSL2 迁移------------
|  wsl --export Ubuntu-18.04 d://wslubuntu//ubuntu-18.04.tar
|  wsl --unregister Ubuntu-18.04
|  wsl --import Ubuntu-18.04 d://wslubuntu d://wslubuntu//ubuntu-18.04.tar
-------------------------


sudo apt-get install xxxx

安装步骤
cd /home

wget https://nodejs.org/dist/latest-v18.x/node-v18.13.0-linux-x64.tar.gz 
tar -xvf node-v18.13.0-linux-x64.tar.gz

输入命令 ：注意，带sudo是可编辑，不带的是只读
sudo vim /etc/profile

按小 i    打开编辑模式，加上以下代码，注意  /usr/local/nodejs根据你本地的路径改一下
export NODE_HOME=/home/node-v18.13.0-linux-x64
export PATH=${NODE_HOME}/bin:$PATH

保存退出后，输入一下命令使环境变量立即生效。
source /etc/profile

chmod -R 777 /root

node -v
npm -v

npm install -g ironfish
如果报错再执行
chmod -R 777 /root

ironfish config:set blockGraffiti "smallcelery"
ironfish config:set enableTelemetry true
ironfish config:show
ironfish accounts:create vultrfish
ironfish accounts:use vultrfish
{
   "name": "vultrfish",
   "spendingKey": "b3f4ac8224c32e653e5d4d10f68e4d73e6871367943567b59a624c95d73e3f37",
   "incomingViewKey": "10b5612e5087da2eb5a68d3881375b3808d763d526ef142d17bbaadba8dd2b01",
   "outgoingViewKey": "f1c579283c68989882d4a3bb6695c32db3c02552e0e8da6a99a1cdeb53f2aa0b",
   "publicAddress": "e81ba02a830221d4cd4bdb26a01894f63ccd50ef4dc0c97de519d2e9441ee9c7a43570151d4500e7f75169",
   "rescan": null
}

关闭防火墙
sudo ufw disable
apt-get install screen

ironfish testnet
ironfish start

chmod +x monitor.sh
./monitor.sh


卸载
npm uninstall -g ironfish

sudo ufw status
sudo ufw enable


=====================================================
pro1: wsl --unregister Ubuntu-18.04 的目的是注销对应版本的Ubuntu的注册记录，注销之后就可以重新安装了
pro2：ubuntu18.04上 /lib/x86_64-linux-gnu/libc.so.6: version `GLIBC_2.28‘ not found   需要更新glbic