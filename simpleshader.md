

Shader "Unlit/SimpleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FloorOffset ("Floor Offset (XYZ)", Vector) = (0, 0, 0, 0)
        _RotationX ("Rotation X (Degrees)", float) = 0
        _RotationY ("Rotation Y (Degrees)", float) = 0
        _RotationZ ("Rotation Z (Degrees)", float) = 0
        _LightDir ("Light Direction", Vector) = (0, -1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 100
        blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Stencil
        {
            Ref 0
            Comp Equal
            Pass IncrSat
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float2 ty: TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Color;
            float _ShadowYOffset;
            float4 _FloorOffset; // 偏移向量 (x, y, z)
            float _RotationX; // 绕 X 轴的旋转角度
            float _RotationY; // 绕 Y 轴的旋转角度
            float _RotationZ; // 绕 Z 轴的旋转角度
            float4 _LightDir;     // 光照方向（世界空间）

            v2f vert (appdata v)
            {
                v2f o;
                float4 wPos = mul(unity_ObjectToWorld, v.vertex);
                 // 应用 XYZ 偏移
                wPos.xyz += _FloorOffset.xyz;

                float3 lightDir = normalize(_LightDir.xyz);
                wPos.xz -= (lightDir.xz) * (wPos.y + _FloorOffset.y - 0.3f) * 1.5;
                //wPos.y =  -_FloorOffset + 0.3f;
                o.ty.y = wPos.y;
                wPos.y = 0;
                o.vertex = mul(UNITY_MATRIX_VP, wPos);//UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float ta = (2-atan(i.ty.y))*0.5f ;
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(0,0,0,ta);
            }
            ENDCG
        }
    }
}
