---
author: RuoSaChen
tags:
  - work
---


1.登录中进度条不能出现的问题。原因是64位测试包中 Facebook 和 Firebase 的sdk初始化过程中需要连接后台地址，但是连接不上导致。解决方式：打32位包，或者开VPN。正式包没问题（可忽略）
![[Pasted image 20241210104252.png]]


2.UI的动画导致的合批导致的层级问题。UIconfig表里的useBatching字段修改位false;