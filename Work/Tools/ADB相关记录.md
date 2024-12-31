
------------------------------------------------------------------------------------
#### <span style="color:yellow;"> 1. Android11 adb push Permission denied</span>
>在Android11设备上使用adb推送文件时遇到Permission denied错误。为解决此问题，应避免使用mnt/sdcard作为目标路径，而应使用实际路径如storage/emulated/0/。
>正确命令示例：adb push testFolder /storage/emulated/0/。
```
	adb push testFolder /storage/emulated/0/
```

------------------------------------------------------------------------------------

