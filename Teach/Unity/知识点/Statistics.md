---
author: RuoSaChen
tags:
  - teach
  - unity
  - work
---
![[cover-unity 18.jpg]]

>首先看FPS，很高就基本没问题（移动平台>30,PC > 60）
1. FPS低，CPU消耗的时间过多就是程序的复杂度太高，一般是在Unity的Update中的代码过于复杂
2. FPS低，GPU消耗的时间过多就是使用的Shader过于复杂，比如实时光照的使用
3. DC过高那就是材质或者网格未进行合并，解决方案：合并材质

>Tris 和 Verts 保持在哪个范围内合适
1. 移动端 Tris < 1M  ， Verts < 2M

>DC控制在多少合适
1. DC是CPU绘制指令，与Batches强相关。优化目标： 移动端<200


