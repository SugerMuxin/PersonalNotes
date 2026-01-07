---
author: RuoSaChen
tags:
  - unity
  - shader
---

![[cover-shader 14.png]]
>1. ComputeScreenPos  

ComputeScreenPos()  用于将一个坐标转换到屏幕坐标，因为在裁剪坐标系中的坐标范围取值是（-1,1）的，但是在屏幕坐标中取值范围是（0,1）的，所以需要将（-1,1）/2 +0.5 就能将裁剪坐标系中的坐标映射到对应的屏幕坐标了。

```
v2f vert (appdata v){
	v2f o;
	o.vertex = UnityObjectToClipPos(v.vertex);
	o.screenPosition = ComputeScreenPos(o.vertex);
}

fixed4 frag (v2f i) : SV_Target{
	float existingDepth01 = tex2D(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition / i.screenPosition.w)).r;
}
```

上述计算之后会得到一个 float4 的屏幕坐标，值得注意的是其中的w值，这个值是因为在透视投影中，需要将一个椎体内的对象关系压缩到屏幕上，所以远处和近处的压缩比例是不一样的。但是这个计算是在齐次的，所以这个W可以理解为这个点到摄像机的距离（也就是可以用于计算某个像素点在场景中的真实的深度）

existingDepth01 拿到的是 0~1 的值，因为_CameraDepthTexture中使用的r通道存储的都是0~1的值。

>2. LinearEyeDepth

```
float4 frag (v2f i) : SV_Target{
	float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
	float existingDepthLinear = LinearEyeDepth(existingDepth01);
	float depthDifference = existingDepthLinear - i.screenPosition.w;
}

```
在正交投影中，摄像机的 Far 和 Near .LinearEyeDepth方法就是将从_CameraDepthTexture中拿到的深度转化成在裁剪坐标中的某个片元在摄像机空间中（或者说裁剪空间奇次空间或者说正交空间都是这个意思）的真正深度值。

depthDifference 就能得到现在渲染的这个片元和深度缓冲区的插值，通常用这个值来做水的深浅的颜色插值。这里我的代码也是用于模拟水面的。

但是这里有个问题也是我不太理解的。我的理解是既然深度是线性的，为什么使用下述的Method2实际上却得不到正确的效果
```
Method1--->
float cameraDepth = tex2D(_CameraDepthTexture, screenPos).r;
float eyeDepth = LinearEyeDepth(cameraDepth);
Method2--->
float cameraDepth = tex2D(_CameraDepthTexture, screenPos).r; 
float eyeDepth = cameraDepth * (far - near) + near;
```



>3. tex2D 和 tex2Dproj 的区别

tex2D是用于取一个纹理上某个点的像素的。
tex2Dproj 用于取正交投影中所产生的纹理，有点拗口。比如在渲染的过程中会产生一个_CameraDepthTexture 和
_CameraNormalsTexture ，深度纹理和法线纹理，这两个纹理都是在摄像机的渲染下产生的，所以是根据裁剪空间中对象的各个顶点和相互关系产生的。所以如果你使用 tex2D来取这个纹理的话就会出现偏差，因为已经压缩了。
同样也不能是用屏幕坐标取取，需要先将屏幕坐标转化为裁剪空间的坐标（这一步必须在片元着色器中计算，因为如果）。