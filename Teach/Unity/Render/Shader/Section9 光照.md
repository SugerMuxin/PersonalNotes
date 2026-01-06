---
author: RuoSaChen
tags:
  - unity
  - shader
---



在Unity里，渲染路径（Rending Path）决定了光照是如何应用到Unity Shader中的。因此，如果需要和光源打交道的情况下，就需要为每个Pass指定使用的渲染路径。

Unity支持的渲染路径包括
1.前向渲染路径 Forward Rending Path
2.延迟渲染路径 Deferred Rending Path
3.顶点照明渲染路径 Vertex Lit Rending Path


1.前向渲染路径

在Unity中，一个Pass不仅仅可可以进行逐像素的光照，还可以进行逐顶点的其他光照。这取决于光照所处流水线阶段以及计算时使用的数学模型。
前向渲染路径又三种处理光照的方式<span style="color:green;">逐顶点处理</span><span style="color:green;">、逐像素处理</span><span style="color:green;">、球谐函数（Spherical Harmonics）SH</span>。决定一个光源使用哪种处理模式取决于它的类型和渲染模式。光源类型指的是光源是平行光还是其他类型的光源，而光源的渲染模式指的是该光源的重要程度。
![[Pasted image 20240413141227.png]]

RenderMode如果被设置称为Improtant。就代表这个光源很重要，那么就会把它当成一个逐像素光源来处理。

Unity对光源有一个基本的规则
1.场景中最亮的平行光总是按照逐像素处理的。
2.渲染模式总被设置成NotImportant的光源，会按照逐顶点或者 SH 处理
3.渲染模式被设置成Import的光源，会按逐像素处理。
4.如果以上规则得到的逐像素光源数量小于QualitySetting中的逐像素光源数量（Pixel Light Count），会有更多的额光源以逐像素的方式进行渲染。

![[Pasted image 20240413144353.png]]
![[Pasted image 20240413144424.png]]


