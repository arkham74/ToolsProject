// Created based on unity Unity built-in shader
// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Hidden/SquircleShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
			Name "Default"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			#include "ShapeUtils.hlsl"

			#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
			#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 params1 : TEXCOORD1;
				float4 params2 : TEXCOORD2;
				float4 emission : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 params1 : TEXCOORD1;
				float4 params2 : TEXCOORD2;
				float4 emission : TEXCOORD3;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;
			float4 _MainTex_ST;

			v2f vert(appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
				o.params1 = v.params1;
				o.params2 = v.params2;
				o.emission = v.emission;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float2 size = float2(i.params1.x, i.params1.y); //512x512
				float aspect = size.x / size.y; //1
				float scale = i.params2.x; //100
				float useFill = i.params2.y;

				float2 correct = 1;
				float2 delta = size;

				if(aspect > 1)
				{
					correct.x = aspect;
					delta /= size.y; //1
					scale /= size.y; //100 / 512
				}
				else
				{
					correct.y = 1.0 / aspect;
					delta /= size.x; //1
					scale /= size.x; //100 / 512
				}

				float radius = i.params1.w * scale; // 0.1953125
				float fill = i.params1.z * scale;

				float2 uv = center(i.texcoord) * correct;

				float sdf = sdRoundedBox(uv, delta - fill * useFill, radius - fill * useFill);

				if(useFill > 0)
				{
					sdf = opOnion(sdf, fill);
				}

				float mask = AA(sdf);

				float4 color = (tex2D(_MainTex, i.texcoord) + _TextureSampleAdd) * (i.color + i.emission) * float4(1, 1, 1, mask);

				#ifdef UNITY_UI_CLIP_RECT
					color.a *= UnityGet2DClipping(i.vertex.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
					clip (color.a - 0.001);
				#endif

				return color;
			}
			ENDCG
		}
	}
}