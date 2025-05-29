
##### <span stype>Blend One Zero
|-- 前面的One代表本次渲染的的颜色参数因子  ，Zero代表缓冲区的目标的参数因子
（Blend One Zero 则代表的是覆盖缓冲区）

<span style="color:yellow;">Tips：不会有 Blend Zero One这种，因为这种代表不使用源，那么本次渲染就没有必要了</span>

> Blend SrcAlpha OneMinusSrcAlpha      (正常混合)
	 Color1 = 当前片元的RGB
	 Alpha1 = 当前片元的A 
	 Color2 = 缓冲区颜色
	 Color = Color1 * Alpha1 + Color2 * (1 - Alpha1)

> Blend OneMinusDstColor One （柔和）
> 	 Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = Color1 * （1- Color2）+ Color2

> Blend DstColor Zero  (正片叠底)
> 	 Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = Color1 * Color2 + Color2 * 0

> Blend DstColor SrcColor
>      Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = Color1 * Color2 + Color2 * Color1 = Colo1 * Color2 * 2

> BlendOp Min   (变暗效果)
> Blend One One
>     Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = min(Color1,Color2) *1  + min(Color1,Color2) *1 

>  BlendOp Max  
>  Blend One One
>     Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = max(Color1, Color2) + min (Color1,Color2) 

> Blend OneMinusDstColor One || Blend One OneMinusSrcColor
> 	 Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = Color1 * (1 - Color2) + Color2  ||  Color = Color1 + Color2*(1 - Color1) 


>  Blend One One
>     Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = Color1 + Color2


>  Blend SrcAlpha One
> 	 Color1 = 当前片元的RGB
> 	 Color2 = 缓冲区颜色
> 	 Color = (1 - Color1).A * Color1 + Color2
> 	 等于在缓冲区上叠加
 
   
   