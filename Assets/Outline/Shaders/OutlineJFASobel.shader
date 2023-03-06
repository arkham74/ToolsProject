//based on https://gist.github.com/bgolus/a18c1a3fc9af2d73cc19169a809eb195

Shader "Hidden/OutlineJFASobel"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Thickness ("Thickness", Float) = 1
		_DepthSensitivity ("DepthSensitivity", Range(0,1)) = 1
		_NormalSensitivity ("NormalSensitivity", Range(0,1)) = 1
		_ColorSensitivity ("ColorSensitivity", Range(0,1)) = 1
		_Color ("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always
		
		HLSLINCLUDE
		#include "JFAGlobal.hlsl"
		ENDHLSL

		Pass //0
		{
			Name "JFAINIT"
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "JFAInitPass.hlsl"
			ENDHLSL
		}

		Pass //1
		{
			Name "JFAVORONOI"
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "JFAVoronoiPass.hlsl"
			ENDHLSL
		}

		Pass //2
		{
			Name "JFAOUTLINE"
			Blend SrcAlpha OneMinusSrcAlpha
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "JFAOutlinePass.hlsl"
			ENDHLSL
		}

		Pass //3
		{
			Name "SOBELOUTLINE"
			Blend SrcAlpha OneMinusSrcAlpha
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "SobelOutlinePass.hlsl"
			ENDHLSL
		}

		// Tags
		// {
		// 	"RenderPipeline"="UniversalPipeline"
		// 	"RenderType"="Opaque"
		// 	"UniversalMaterialType" = "Unlit"
		// 	"Queue"="Geometry"
		// }

		Pass //4
		{
			Name "SURFACEID"
			// Tags { "LightMode"="UniversalForward" }
			Cull Back
			Blend One Zero
			ZTest LEqual
			ZWrite On
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "SurfaceIDPass.hlsl"
			ENDHLSL
		}

		Pass //5
		{
			Name "DECAL"
			Blend One OneMinusSrcAlpha
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "DecalPass.hlsl"
			ENDHLSL
		}

		Pass //6
		{
			Name "COLORDEPHTNORMAL"
			Blend SrcAlpha OneMinusSrcAlpha
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "ColorDepthNormalPass.hlsl"
			ENDHLSL
		}
	}
}
