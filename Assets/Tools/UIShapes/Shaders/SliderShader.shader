// Created based on unity Unity built-in shader
// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Hidden/SliderShader"
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
				float4 params : TEXCOORD1;
				float4 fillColor : TEXCOORD2;
				float4 emission : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 screenPos : POSITION1;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				float4 params : TEXCOORD1;
				float4 fillColor : TEXCOORD2;
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
				o.screenPos = ComputeScreenPos(o.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
				o.params = v.params;
				o.fillColor = v.fillColor;
				o.emission = v.emission;
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float aspect0 = i.params.x / i.params.y;
				float fill = i.params.z;
				float direction = i.params.w;

				float4 fillColor = i.fillColor;
				float2 aspect = float2(aspect0, 1);
				float2 uv = i.texcoord;

				uv = center(uv);
				uv = uv * aspect;

				float fillMask = 0;
				float sdf = 0;

				if(direction == 0)
				{
					fillMask = AA(i.texcoord.x - fill);
				}
				else if(direction == 1)
				{
					fillMask = AA(1 - i.texcoord.x - fill);
				}
				else if(direction == 2)
				{
					fillMask = AA(i.texcoord.y - fill);
				}
				else if(direction == 3)
				{
					fillMask = AA(1 - i.texcoord.y - fill);
				}

				if(aspect0 >= 1)
				{
					uv *= 0.001 * aspect.yx + 1;
					sdf = sdSegment(uv, float2(-(aspect0 - 1), 0.0), float2((aspect0 - 1), 0.0));
					sdf = opRound(sdf, 1.0);
				}
				else
				{
					uv *= 0.001 * (1.0 / aspect.xy) + 1;
					sdf = sdSegment(uv, float2(0.0, -(1.0 - aspect0)), float2(0.0, 1.0 - aspect0));
					sdf = opRound(sdf, aspect0);
				}

				float mask = AA(sdf);
				float4 col = lerp(i.color * i.color.a, fillColor * fillColor.a, fillMask);
				float4 color = (tex2D(_MainTex, i.texcoord) + _TextureSampleAdd) * (col + i.emission) * float4(1, 1, 1, mask);

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