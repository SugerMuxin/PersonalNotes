---
author: RuoSaChen
tags:
  - unity
  - teach
---
![[cover-unity 25.jpg]]

1. 在BRP中渲染一个点需要三个批次，如果使用URP只需要两个批次。That's because URP doesn't use a separate depth pass for directional shadows. It does have more set-pass calls, but that doesn't appear to be a problem.
https://catlikecoding.com/unity/tutorials/basics/measuring-performance/


2. URP 的Dynamic batching 对阴影并不生效，这就是为啥在URP中使用dynamic batching的效果不如 BRP中的 db的原因。而GPU Instancing 却会表现优异
 ![[Pasted image 20250714151022.png]]

在实时光照的开放世界或者说大一些的3D场景中使URP 会得到非常大的性能提升。主要是阴影能够合批


