---
tags:
  - unity
  - fgui
---
![[cover-unity 15.jpg]]

1. GTTextField 处理文字的方式
	在Loadfont之后，fgui会对字符串进行切割，然后根据各个字符的 FontInfo 来设置每个字符的网格大小和相对的偏移量等来动态绘制一系列的网格。

文字的处理方案大抵如此，有空可以看一下 TextMeshPro 的处理方案，补全该文档。TextMeshPro 使用了矢量图的方案