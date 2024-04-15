
Shader "Study/AlphaTest01"
{
   Properties 
   {
     _TintColor("Test Color", color) = (1, 1, 1, 1)
	 _Intensity("Range Sample", Range(0, 1)) = 0.5
	 _MainTex("Main Texture", 2D) = "white" {}

     [Toggle] _AlphaOn("AlphaTest", float) = 1 // 아직 미적용
     _AlphaCut("AlphaCut", Range(0,10)) = 1

     [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("Src Blend", Float) = 1
     [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Dst Blend", Float) = 0
     [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull Blend", Float) = 1
   }  

   SubShader
   {  	
	    Tags
        {
	        "RenderPipeline"="UniversalPipeline"
            "RenderType"="TransparentCutout"          
            "Queue"="Transparent"
        }
        Pass
    	{  		
            Blend [_SrcBlend] [_DstBlend]
            Cull [_Cull]
          //  Zwrite off

     	    Name "Universal Forward"
            Tags {"LightMode" = "UniversalForward"}

HLSLPROGRAM
        	#pragma prefer_hlslcc gles
        	#pragma exclude_renderers d3d11_9x
        	#pragma vertex vert
        	#pragma fragment frag       	       	

       	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
           
	    half4 _TintColor;
	    float _Intensity;
        
     	Texture2D _MainTex;
        float4 _MainTex_ST;
         
        float _AlphaCut;

        SamplerState sampler_MainTex;

    struct VertexInput
    {
	    float4 vertex       : POSITION;
	    float2 uv           : TEXCOORD0;
    };

    struct VertexOutput
    {
        float4 vertex   	: SV_POSITION;
        float2 uv       	: TEXCOORD0;           	
    };

    VertexOutput vert(VertexInput v)
    {
        VertexOutput o;

        o.vertex = TransformObjectToHClip(v.vertex.xyz);            
        o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
        return o;
    }

    half4 frag(VertexOutput i) : SV_Target
    {
        half4 color = _MainTex.Sample(sampler_MainTex, i.uv);
        
        color.rgb *= _TintColor;
        color.a *= _AlphaCut;

        return color;
 }

ENDHLSL
    	}
     }
}
