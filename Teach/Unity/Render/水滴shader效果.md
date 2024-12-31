> version 1.0

Shader "Unlit/RainyWindow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Size("Size",float) = 1
		_T("Time",float) = 0
		_Distortion("Distortion",Range(-5 , 5)) = 0

			
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
			#define S(a,b,t) smoothstep(a,b,t)
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Size, _T, _Distortion;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float N21(float2 p) {
				p = frac(p * float2(123.34,345.45));
				p += dot(p, p + 34.345);
				return frac(p.x * p.y);
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float t = fmod(_Time.y + _T,7200);
				float4 col = 0;
				float2 aspect = float2(2, 1);
				float2 uv = i.uv*_Size * aspect;
				uv.y += t * .25;
				float2 gv = frac(uv) - .5; //获取小数部分,为了获得重复的效果//
				float2 id = floor(uv);

				float n = N21(id);
				t += n * 6.2831;

				float w = i.uv.y * 10;
				float x = (n - .5) * .8;// 
				x += (.4 - abs(x)) * sin(3 * w)*pow(sin(w), 6)*.45;
				float y = -sin(t + sin(t + sin(t)*.5)) *.45;
				y -= (gv.x - x)*(gv.x - x);  //目的是让水滴的上面部分尖锐一点//

				float2 dropPos = (gv - float2(x, y)) / aspect;
				float drop = S(.05, .03, length(dropPos));

				float2 trialPos = (gv - float2(x, t * .25)) / aspect;
				trialPos.y = (frac(trialPos.y * 8) - .5) / 8;
				float trial = S(.03, .01, length(trialPos));
				float fogTrail = S(-.05, .05, dropPos.y);
				fogTrail *= S(.5, y, gv.y);
				trial *= fogTrail;
				fogTrail *= S(.05, .04, abs(dropPos.x));

				col += fogTrail * .5;
				col += trial;
				col += drop;

				float2 offs = drop * dropPos + trial * trialPos;
				if (gv.x > .48 || gv.y > .49) { col = float4(1, 0, 0, 1);}
				//col *= 0; col += N21(id);
				col = tex2D(_MainTex, i.uv + offs * _Distortion);
                return col;
            }
            ENDCG
        }
    }
}



-------------------------------------------------------------------------------------
> Version 2.0

Shader "Unlit/UIRainWindow"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_AlphaTex("Alpha Texture", 2D) = "black"{}
		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0

		_Size("Size",float) = 1
		_T("Time",float) = 0
		_Distortion("Distortion",Range(-5 , 5)) = 0
		_Blur("Blur",Range(0 , 1)) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend One OneMinusSrcAlpha
			ColorMask[_ColorMask]

			LOD 100
			GrabPass{ "_GrabTexture"}

			Pass
			{
				Name "Default"
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#define S(a,b,t) smoothstep(a,b,t)
				#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
				#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex	: SV_POSITION;
					fixed4 color : COLOR;
					float2 uv  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					half4  mask : TEXCOORD2;
					UNITY_VERTEX_OUTPUT_STEREO
					float4 grabUv : TEXCOORD3;
					UNITY_FOG_COORDS(1)
				};

				sampler2D _MainTex;
				fixed4 _Color;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				float4 _MainTex_ST;
				float _UIMaskSoftnessX;
				float _UIMaskSoftnessY;
				sampler2D _AlphaTex;

				sampler2D _GrabTexture;
				float _Size, _T, _Distortion, _Blur;

				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
					float4 vPosition = UnityObjectToClipPos(v.vertex);
					OUT.worldPosition = v.vertex;
					OUT.vertex = vPosition;

					float2 pixelSize = vPosition.w;
					pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

					float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
					float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
					OUT.mask = half4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));


					OUT.uv = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
					OUT.grabUv = UNITY_PROJ_COORD(ComputeGrabScreenPos(OUT.vertex));
					//OUT.color = v.color * _Color;

					UNITY_TRANSFER_FOG(OUT, OUT.vertex);
					return OUT;
				}

				float N21(float2 p) {
					p = frac(p * float2(123.34, 345.45));
					p += dot(p, p + 34.345);
					return frac(p.x * p.y);
				}

				float3 Layer(float2 UV, float t) {
					float2 aspect = float2(2, 1);
					float2 uv = UV * _Size * aspect;
					uv.y += t * .25;
					float2 gv = frac(uv) - .5; //获取小数部分,为了获得重复的效果//
					float2 id = floor(uv);

					float n = N21(id);
					t += n * 6.2831;

					float w = UV.y * 10;
					float x = (n - .5) * .8;// 
					x += (.4 - abs(x)) * sin(3 * w)*pow(sin(w), 6)*.45;
					float y = -sin(t + sin(t + sin(t)*.5)) *.45;
					y -= (gv.x - x)*(gv.x - x);  //目的是让水滴的上面部分尖锐一点//

					float2 dropPos = (gv - float2(x, y)) / aspect;
					float drop = S(.05, .03, length(dropPos));

					float2 trialPos = (gv - float2(x, t * .25)) / aspect;
					trialPos.y = (frac(trialPos.y * 8) - .5) / 8;
					float trial = S(.03, .01, length(trialPos));
					float fogTrail = S(-.05, .05, dropPos.y);
					fogTrail *= S(.5, y, gv.y);
					trial *= fogTrail;
					fogTrail *= S(.05, .04, abs(dropPos.x));

					//col += fogTrail * .5;
					//col += trial;
					//col += drop;

					float2 offs = drop * dropPos + trial * trialPos;
					//if (gv.x > .48 || gv.y > .49) { col = float4(1, 0, 0, 1); }

					return float3(offs, fogTrail);
				}


				fixed4 frag(v2f i) : SV_Target
				{
					half4 alphaColor = tex2D(_AlphaTex, i.uv);

					float t = fmod(_Time.y + _T,7200);
					float4 col = 0;

					//col *= 0; col += N21(id);
					float3 drops = Layer(i.uv,t);
					drops += Layer(i.uv *1.23 + 7.54, t);
					drops += Layer(i.uv *1.35 + 1.54, t);
					drops += Layer(i.uv *1.23 - 7.54, t);

					float fade = 1 - saturate(fwidth(i.uv) * 60);

					float blur = _Blur * 7 * (1 - drops.z * fade);
					//col = tex2Dlod(_MainTex, float4(i.uv + drops.xy * _Distortion,0, blur));

					float2 projUv = i.grabUv.xy / i.grabUv.w;
					projUv += drops.xy * _Distortion * fade;
					blur *= .01;

					const float numSamples = 32;
					float a = N21(i.uv)*6.2831;
					for (float i = 0; i < numSamples; i++) {
						float offs = float2(sin(a), cos(a)) * blur;
						float d = frac(sin((i + 1) * 546.) * 5424.);
						d = sqrt(d);
						offs *= d;
						col += tex2D(_GrabTexture, projUv + offs);
						a++;
					}
					col /= numSamples;

					
					//half4 color = IN.color * (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
					

					col.a = col.a * (1.0 - alphaColor.r); // revert alpha

					#ifdef UNITY_UI_CLIP_RECT
					//half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
					//color.a *= m.x * m.y;
					#endif

					#ifdef UNITY_UI_ALPHACLIP
					//clip (color.a - 0.001);
					#endif

					col.rgb *= col.a;

					return col;
				}
			ENDCG
			}
		}
}

