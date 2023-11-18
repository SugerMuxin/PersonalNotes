
在Unity中，每次引擎准备数据并通知GPU的过程称为一次Draw Call（DC）。Draw Call次数是一项非常重要的性能指标。UI系统一般包含数量众多的物体，有效控制DC是衡量一个UI系统是否实用的关键因素，特别是在移动设备上。

>NGUI 策略
>
>NGUI把UIPanel中的Widget按depth排序，然后将相同材质的Widget进行Mesh合并，例如使用相同图集的图片，或者文字。Mesh合并的优点是合并后这些Widget就只产生一个DC。但这个合并过程需要计算所有Widget坐标相对于Panel的变换，而且如果Widget行为改变，例如平移，缩放等，都会触发Mesh重新合并，这会带来一定的CPU消耗，这就需要开发者谨慎组织UI元素到各个UIPanel，并且对深度需要细致安排，否则达不到减少DC效果的同时更可能带来比较大的CPU消耗。


>UGUI 策略
>
>UGUI的原理大体也相似，Canvas和UIPanel是类似的，每个Canvas将优化为1个Mesh或多个SubMesh。


>FGUI 策略
>
>FairyGUI基于Unity的`Dynamic Batching`技术，提供了`深度调整技术`进行 Draw Call优化 。FairyGUI能在不改变最终显示效果的前提下，尽可能的把相同材质的物体调整到连续的RenderingOrder值上，以促使他们能够被Unity Dynamic Batching优化。Dynamic Batching是Unity提供的Draw Call Batching技术之一。如果动态物体共用着相同的材质，那么Unity会自动对这些物体进行批处理。但Dynamic Batching的一个重要的前提是这些动态物体是连续渲染的。
>https://www.fairygui.com/docs/unity/drawcall


