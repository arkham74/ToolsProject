//based on https://gist.github.com/bgolus/a18c1a3fc9af2d73cc19169a809eb195

Shader "Hidden/OutlineJFASobel"
{
	Properties
	{
	}

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always
		
		Pass //4
		{
			Name "SURFACEID"
			Cull Back
			Blend One Zero
			ZTest LEqual
			ZWrite On
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "JFAGlobal.hlsl"
			#include "SurfaceIDPass.hlsl"
			ENDHLSL
		}
	}
}
