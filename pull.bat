f:
cd F:\Obsidian-Vault\Notes\Peanut_Notes
git config --global --unset http.proxy 
git config --global --unset https.proxy
git config --global http.proxy http://127.0.0.1:1080
 
git config --global https.proxy http://127.0.0.1:1080
git pull
pause