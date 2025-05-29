1.模板测试：
	https://zhuanlan.zhihu.com/p/612811622



![[Pasted image 20250424134602.png]]
1.Compute Internal-Skinning 是什么，干什么，又有什么消耗
	Compute Internal-Skinning 和Unity的蒙皮网格有关， Unity将蒙皮的相关计算转移到GPU。- 传统蒙皮动画（Skinning）通过CPU计算骨骼对顶点的权重变换，再将结果传递给GPU渲染（称为 **CPU Skinning**）。- **Compute Internal-Skinning** 则利用GPU的Compute Shader，直接在GPU上并行计算骨骼对顶点的变换，称为 **GPU Skinning**。


![[Pasted image 20250424140817.png]]
2.生成深度纹理的消耗取决于几个因素：场景的复杂度（顶点数量、覆盖的像素数量）、渲染分辨率、以及是否启用了MSAA等抗锯齿技术。如果是通过复制深度缓冲区，可能在带宽上有较大的消耗，尤其是在高分辨率下。如果是通过渲染Pass生成，那么需要额外的绘制调用和着色器计算，可能增加GPU的工作负载。
```
     - 在Forward 渲染Path 下开启深度
     camera.depthTextureMode = DepthTextureMode.Depth; 

	 - 在Shader中可以使用 _CameraDepthTexture
	 - 在Shader中使用 `#if defined(UNITY_DEPTH_TEXTURE_SUPPORT)` 条件编译，避免在不支持的设备上采样深度。
	 - 
```