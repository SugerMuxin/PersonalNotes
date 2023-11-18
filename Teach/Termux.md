git clone -b xdag https://github.com/swordlet/xmrig.git

cmake .
make -j8

备注


libuv 1.44.1
OpenSSL 1.1.1o
hwloc 2.7.1





更新
pkg update

安装ubuntu22.04
pkg install proot-distro
proot-distro install ubuntu
proot-distro login ubuntu

apt-get update
apt-get install wget
apt install git
apt install build-essential
apt install python3

安装 nvm
curl -sL https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.2/install.sh -o install_nvm.sh
chmod +x install_nvm.sh
./install_nvm.sh
重新登录ubuntu后
nvm install v16.12.0

编译 openssl 1.1
wget https://www.openssl.org/source/openssl-1.1.1o.tar.gz
tar -xvf openssl-1.1.1o.tar.gz
cd openssl-1.1.1o
./config
make -j8

链接到库
sudo ln -s  /root/openssl-1.1.1o/libcrypto.so.1.1 /usr/lib/libcrypto.so.1.1  
sudo ln -s  /root/openssl-1.1.1o/libssl.so.1.1 /usr/lib/libssl.so.1.1

建立下载文件夹
mkdir npm_link
chmod -R 777 npm_link
ln -s npm_link .npm

npm install -g ironfish



其他命令备份

git clone https://github.com/iron-fish/ironfish.git


apt install bust
apt install binutils

手动下载 node
wget https://nodejs.org/dist/latest-v16.x/node-v16.18.0-linux-arm64.tar.gz
tar -xvf node-v16.18.0-linux-arm64.tar.gz
export NODE_HOME=/root/node-v16.18.0-linux-arm64
export PATH=${NODE_HOME}/bin:$PATH

安装yarn
npm install -g yarn

export YARN_HOME=${NODE_HOME}/yarn
export PATH=${YARN_HOME}:$PATH

编译
yarn install


make test
make install


检查版 openssl version
