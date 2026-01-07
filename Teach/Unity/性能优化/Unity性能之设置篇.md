---
author: RuoSaChen
tags:
  - unity
---
![[cover-unity 10.jpg]]

Quality -> Texture Streaming 
>**Texture Streaming（纹理流送）** 系统是一个高级内存管理功能，正确使用它能极大优化项目性能，但理解其工作原理和适用场景至关重要。
>**Texture Streaming**的核心是在不影响视觉效果的前提下，大幅度降低游戏纹理的内存占用（尤其是显存）
在一个开放世界中，距离摄像机非常远的物体可能在屏幕上显示的会非常小，甚至只有几个像素而已，mipmap的等级往往很低，但是按照传统的非流的加载方式，那么需要加载完整的texture，这回对加载和显存造成很大的浪费。


Quality -> Soft Particles
>**Soft Particles** 是一种渲染技术，用于让粒子（通常是烟雾、火焰、蒸汽、灰尘等半透明效果）与场景中的其他几何体**平滑地融合**，避免出现生硬的交叉边缘。
>Soft Particles 就是利用这个当前场景的深度实现插值，通过比较粒子与场景的深度，可以在粒子与几何体相交的地方自动淡出（Alpha Fade）,从而形成柔软的自然的融合效果