---
author: RuoSaChen
tags:
  - unity
  - shader
---

>1.以下是Shader支持的Properties语义块
>    Properties
    {
        _Int("Int",Int) = 2
        _Float("Float",Float) = 1.5
        _Range("Range",Range(0,5.0)) = 3
        _Color("Color",Color) = (1,1,1,1)
        _Vector("Vector",Vector) = (2,3,6,1)
        _MainTex("Texture", 2D) = "white" {}
        _Cube("Cube",Cube) = "bump"{}
        _3D("3D",3D) = "black"{}
    }
    其中“bump”,"white","black"是Untiy默认支持的内置纹理名称，

>2. 重量级成员Subshader
>	每一个Unity Shader文件至少要包含一个SubShader，如果Shader中的所有Sub都不支持的话，Unity就会使用Fallback语义制定的UnityShader。Untiy提供这种语义的原因是不同的显卡具有不同的能力。

   
    -------------------------------------------------------------------------------------
    SubShader{
	    Tags{ "RenderType" = "Opaque" }
		Cull Off
        ZTest true
        Zwrite false
	    Pass
	    {
	    }
    }


    -------------------------------------------------------------------------------------
    Cull   Back | Front | Off
    ZTest  Less | Greater | LEqual | GEqual | Equal | NotEqual | Always
    ZWrite On | Off                     开启关闭深度写入
    Blend SrcFactor | DisFactor   开始关闭混合模式


    --------------------------------------------------------------------------------------
    其中Tags（称为标签）是可以选择的
    Tags
    Queue   | 控制渲染顺序，制定该物体属于那一个渲染队列，通过这种方式可以保证  
               所有的透明物体可以在不透明物体之后渲染
            Tags{"Queue" = "Transparent"}
     RenderType |  对着色器进行分类，透明着色器，不透明着色器
	        Tags{"RenderType" = "Opaque"}
	 DisableBatching 一些SubShader在使用Unity的批处理功能时会出现问题
			 Tags{"DisableBatching" = "True"}

>3. FallBack的作用是，如果shader中所定义的Pass都不能在当前的显卡上执行的话就使用FallBack定义的这个默认的Shader，主打一个擦屁股。
	FallBack Off 就代表如果行不通就不管了



