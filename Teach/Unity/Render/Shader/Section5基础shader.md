---
author: RuoSaChen
tags:
  - unity
  - shader
---
![[cover-shader 9.png]]

1. Unity支持的语义
	DirextX 10.0 以后，有一种新的语义类型
	 SV-  SystemValue 系统数值

	---------------------从应用阶段传递模型数据给顶点着色器时的常用语义-------------------------
	POSITION     模型空间中的顶点位置，通常是float4类型
	NORMAL       顶点法线，通常是float3类型
	TANGENT      顶点切线，通常是float4类型
	TEXCOORD(n)   该顶点的纹理坐标，TEXCOORD0表示第一组纹理坐标，依次类推，通常是float2或者float4类型
	COLOR         顶点颜色，通常是float4类型


	----------------------从顶点着色器传递数据给片元着色器时的常用语义--------------------------
	SV_POSTION    裁剪空间中的顶点坐标，结构体中必须包含一个使用该语义修饰的变量
	COLOR0        通常用于输出第一组顶点颜色，但不是必须的
	COLOR1        通常用于输出第二组顶点颜色，但不是必须的
	TEXCOORD0~TEXTOORD7    通常同于输出纹理坐标，但不是必须的


2. <span style="color:yellow;">float</span> ,<span style="color:yellow;">half</span> 还是 <span style="color:yellow;">fixed</span>
	这三种数据类型都是浮点类型，不过精度范围不同。float > half > fixed。具体使用什么类型其实是要根据硬件的支持来决定的，在PC上其实不需要怎么考虑这个问题。
	tip: 尽可能的使用较低的精度类型，因为这可以优化Shader的性能,同样这一点在移动平台上比较重要。


3. Unity支持的ShaderTarget
	pragma target 2.0   Deault Shader Target 相当于Direct3D 9 上的ShaderModel 2.0
	pragma target 3.0   Direct3D 9 上的ShaderModel 3.0
	pragma target 4.0   Direct3D 10 上的ShaderModel 4.0
	pragma target 5.0   Direct3D 11 上的ShaderModel 5.0
	
	<span style="color:yellow">什么是ShaderModel?</span>
	ShaderModel是微软提出的一套规范，通俗的理解就是它们决定了Shader中各个特性（Feature）和能力（Capability）。这些特性和能力体现了Shader能使用的运算指令数目、寄存器个数等各个方面。ShaderModel等级越高，Shader的能力就越大。