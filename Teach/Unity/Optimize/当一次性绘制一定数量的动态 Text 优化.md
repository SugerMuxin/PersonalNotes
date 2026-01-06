---
author: RuoSaChen
tags:
  - unity
  - personal
  - teach
---


当场景中有群攻技能，当对很多的怪物同时造成伤害时，需要产生大量的伤害弹出时，如果使用传统的UI的方式，或者场景中的3D Text时，会消耗巨大的性能。

解决方法：
	使用GPU动画和GPUInstance的方法来实现这样的效果，可以一个Drawcall绘制完成。
	![[Pasted image 20250828170049.png]]
需要一张数字贴图，使用的一些其他的暴击之类的效果也可以放在其中
![[Pasted image 20250828170909.png]]

Shader 代码如下
```
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DamageNumberInstanced"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
        _VerticalHeightFactor ("VerticalHeightFactor",Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float4 outlineColor : TEXCOORD1;
                float outlineWidth: TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _VerticalHeightFactor;
            
            // 实例化数据
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InstanceColor)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InstanceOutlineColor)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceCreationTime)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceDamageDuration)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceRandomOffset)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceIsCritical)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceDamageValue)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceOutlineWidth)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceMaintainSizeTime)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstancesizeScalePercent)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceFadeInTime)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceFadeOutTime)
                UNITY_DEFINE_INSTANCED_PROP(float, _InstanceSign)
            UNITY_INSTANCING_BUFFER_END(Props)

            // 数字纹理中每个数字的UV布局（假设是2x10网格布局0-9和符号）
            float2 GetDigitUV(float digit, float sign,float2 baseUV)
            {
                // 将数字转换为整数并限制在0-9范围内
                int intDigit = (int)digit;
                intDigit = clamp(intDigit, 0, 10);
                float x = (intDigit % 10) * 0.1;
                float y = ((int)sign % 2) * 0.5;
                
                // 调整UV到正确的数字区域
                float2 uv = baseUV;
                uv.x = uv.x * 0.1 + x;
                uv.y = uv.y * 0.5 + y;
                
                return uv;
            }
            
            v2f vert (appdata v, uint instanceID : SV_InstanceID)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                
                // 获取实例数据
                float creationTime = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceCreationTime);
                float duration = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceDamageDuration);
                float randomOffset = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceRandomOffset);
                float isCritical = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceIsCritical);
                float4 instanceColor = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceColor);
                float damageValue = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceDamageValue);
                float4 outlineColor = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceOutlineColor);
                float outlineWidth = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceOutlineWidth);
                float maintainSizeTime = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceMaintainSizeTime); // 维持大小的时间
                float sizeScalePercent = UNITY_ACCESS_INSTANCED_PROP(Props, _InstancesizeScalePercent); //缩放比例
                float fadeInTime = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceFadeInTime);             //淡入时间
                float fadeOutTime = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceFadeOutTime);           //淡出时间
                float sign = UNITY_ACCESS_INSTANCED_PROP(Props, _InstanceSign);                         //符号图像位

                // 计算动画进度
                float elapsed = _Time.y - creationTime;
                float progress = saturate(elapsed / duration);
                
                // 动画效果：上浮
                float verticalOffset = progress * _VerticalHeightFactor;

                 // 淡入淡出效果
                float fadeInProgress = saturate(elapsed / fadeInTime); // 淡入进度
                float fadeOutProgress = saturate(duration - elapsed / fadeOutTime); // 淡出进度
                float alpha = fadeInProgress * fadeOutProgress; // 结合淡入淡出
                
                // 随机水平偏移
                float horizontalOffset = sin(progress * 10.0 + randomOffset * 10.0) * 0.3;

                // 字体大小变化效果
                float sizeProgress = saturate((elapsed - maintainSizeTime) / (duration - maintainSizeTime));
                float sizeScale = lerp(1.0, sizeScalePercent, sizeProgress); // 从原始大小缩小到50%
                // 应用变换
                float4 worldPos =  mul(UNITY_MATRIX_M,v.vertex);
                worldPos.xyz += float3(0, verticalOffset, 0);
                // 应用字体大小缩放
                worldPos.xyz = worldPos.xyz * sizeScale;

                o.vertex = mul(UNITY_MATRIX_VP, worldPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
   
                // 设置颜色和透明度
                o.color = instanceColor;
                o.outlineColor = outlineColor;
                o.outlineWidth = outlineWidth;
                o.color.a *= alpha;

                // 根据数字值计算UV偏移
                o.uv = GetDigitUV(damageValue, sign,o.uv);
                
                return o;
            }
            
            
            fixed4 frag (v2f i) : SV_Target
            {
                // 如果轮廓宽度为0，直接返回文字颜色
                if (i.outlineWidth < 0.001)
                {
                   fixed4 center = tex2D(_MainTex, i.uv);
                   fixed4 col = i.color;
                   col.a *= center.a;
                   return col;
                }

                // 文字轮廓效果
                fixed4 outline = i.outlineColor;
                fixed4 center = tex2D(_MainTex, i.uv);
                
                // 简单的轮廓检测
                if (center.a < 0.5)
                {
                    // 检查周围像素
                    float2 uvOffset = _MainTex_TexelSize.xy * i.outlineWidth;
                    fixed a1 = tex2D(_MainTex, i.uv + float2(uvOffset.x, 0)).a;
                    fixed a2 = tex2D(_MainTex, i.uv + float2(-uvOffset.x, 0)).a;
                    fixed a3 = tex2D(_MainTex, i.uv + float2(0, uvOffset.y)).a;
                    fixed a4 = tex2D(_MainTex, i.uv + float2(0, -uvOffset.y)).a;
                    
                    if (a1 + a2 + a3 + a4 > 0)
                    {
                        outline.a = i.color.a;
                        return outline;
                    }
                }
                
                fixed4 col = i.color;
                col.a *= center.a;
                return col;
            }
            ENDCG
        }
    }
}

```



```
C# Code

public void SetDamageNumber(
    float damage,
    Color color,
    Color outline,
    float outlineWidth,
    float furation,
    float randomOffest,
    float maintainSizeTime,
    float scalePercent,
    float fadeInTime,
    float fadeOutTime
    )
{
    // 获取Shader全局的_Time值
    Vector4 globalTime = Shader.GetGlobalVector("_Time");
    float timeY = globalTime.y;
    propertyBlock.SetVector("_InstanceColor", color);
    propertyBlock.SetVector("_InstanceOutlineColor", outline);
    propertyBlock.SetFloat("_InstanceCreationTime", timeY);
    propertyBlock.SetFloat("_InstanceDamageDuration", furation);
    propertyBlock.SetFloat("_InstanceRandomOffset", randomOffest);
    propertyBlock.SetFloat("_InstanceDamageValue", damage);
    propertyBlock.SetFloat("_InstanceOutlineWidth", outlineWidth);
    propertyBlock.SetFloat("_InstanceMaintainSizeTime", maintainSizeTime);
    propertyBlock.SetFloat("_InstancesizeScalePercent", scalePercent);
    propertyBlock.SetFloat("_InstanceFadeInTime", fadeInTime);
    propertyBlock.SetFloat("_InstanceFadeOutTime", fadeOutTime);
    propertyBlock.SetFloat("_InstanceSign", 1f);
    meshRenderer.SetPropertyBlock(propertyBlock);
}

```


切换场景会重新设置  <span style="color:yellow;">_Time.y</span> 的时间，这会造成 CPU中的 Time.time 和 <span style="color:yellow;">_Time.y</span> 的时间不同步，这样就会造成在需要严格控制 时间的shader中的一些效果出现偏差，比如上面的数字的飘动的效果。

