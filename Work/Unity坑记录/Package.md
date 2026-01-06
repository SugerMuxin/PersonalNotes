---
author: RuoSaChen
tags:
  - teach
  - work
---


>1. obb 是APK 扩展文件用作应对 Google Play 应用商店 100MB 应用大小限制的解决方案。Unity 会自动将输出包拆分为 APK 和 OBB。这不是拆分应用程序包的唯一方法（其他选项包括第三方插件和 [AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)），但却是 Unity 官方支持的唯一自动拆分机制。
>     apk和obb的分配方式：
> （1）APK - 由可执行文件（Java 和本机文件）、插件、脚本以及第一个场景（索引为 0）的数据组成。
>   (2)   OBB - 包含其他所有内容，包括所有剩余的场景、资源和流媒体资源。