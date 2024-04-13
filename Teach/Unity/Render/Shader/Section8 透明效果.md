1.Unity中实现透明效果的方法
（1）使用透明度测试（Alpha Test），这种方法其实无法得到真正的透明效果
（2）透明度混合（Alpha Blending）

2.渲染顺序
（1）对于不透明（Opaque）物体，不考虑渲染顺序其实也能得到正确的渲染结果，这是因为有深度缓冲区z-buffer的存在。（ZWrite 功能需要开启 ，ZTest 也需要开启）
（2）对于透明的物体就没那么简单了
	i. <span style="color:green;">透明度测试 ： </span> 会设置一个阈值，如果一个片元的alpha值小于该阈值就会直接被舍弃，不会再进行后续的任何处理，不会对颜色缓冲区产生任何影响。如果大于该阈值，就会当做普通的不透明物体处理，需要进行后续的深度测试和深度写入操作。这也就是说透明度测试时不需要关闭深度写入的
	ii:<span style="color:green;">透明度混合 ： </span> 这种方法可以得到真正的半透明效果。它会使用当前的透明度作为混合因子，与已经存储在颜色缓冲区颜色进行混合，得到新的颜色。但是透明度混合需要关闭深度写入，但是不会关闭深度测试。这是因为可能有不透明的物体需要渲染在透明物体的前面。
	
-----<span style="color:yellow;">为什么要关闭深度写入呢？</span>
	因为半透明物体背后的物体本来是可以被看到的，也写入了深度缓冲区。但是如果透明物体开启了深度写入，就会导致后面的不透明物体的片元会被剔除，从而不能正确渲染。但是这样一来，渲染顺序就变的非常非常非常非常重要


2.UnityShader的渲染顺序
Unity为了解决渲染顺序的问题提供了渲染队列的这一解决方案（Render Queue），我们可以使用SubShader中的Queue标签来决定我们的模型归于那个渲染队列。Unity内部使用一些列整数索引来表示每个渲染队列，且索引号越小表示越先被渲染。

	--------------------Unity预定义的渲染队列 Queue ----------------------
	Name               Index                               Description                        
	BackGround         1000            最先渲染，通常使用该队列来渲染那些需要渲染在背景上的物体         
	Geometry           2000            默认渲染队列，不透明物体使用这个渲染队列                      
	AlphaTest          2450            需要透明度测试的物体使用这个队列
	Transparent        3000            这个队列会按照从后往前的顺序渲染。任何使用了透明度的物体使用这个队列
	Overlay            4000            改队列用于实现一些叠加效果。任何需要在最后渲染的物体使用这个队列   



3.为了实现混合效果，还需要Unity提供的Blend命令
	
	------------ShaderLab的Blend命令---------------------
	Belend Off                    关闭混合
	Blend SrcFactor DstFactor     开启混合并设置混合因子。源颜色会乘以SrcFactor,而目标颜色（已经存在于缓                                     冲区的颜色会乘以DstFactor,然后把两者相加再存入颜色缓冲中
	Blend SrcFactor DstFactor     和上面几乎一样，只是使用不同的混合因子来混合Alpha通道
	     SrcFactorA DstFactorB 
	Blend BlendOption            并非把源颜色和目标颜色简单的混合，而是使用BlendOperation对它进行其他操作


	------------常见的混合类型-----------
	Blend SrcAlpha OneMinusSrcAlpha     正常混合，即透明度混合
	Blend OneMinusDstColor One          柔和相加（soft additive）
	Blend DstColor SrcColor             两倍相乘（2X Multiply）
	BlendOp Max
	Blend One One                       变暗Darken
	Blend OneMinusDstColor One          滤色
	Blend One OneMinusSrcColor          滤色（和上面的效果相同）
	Blend One One                       线性减淡

4.<span style="color:green;">ColorMask </span>
ColorMask用于设置颜色通道的写掩码（write mask）它的语义如下：
	ColorMask RGB  |  A | 0 其他任何 R/G/B/A的组合
	如果 ColorMask 0 就代表该Pass不写入任何颜色通道，即不会输出任何颜色