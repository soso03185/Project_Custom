// Shader ����. ���̴��� ������ �̸��� ���⼭ �����մϴ�.
Shader "URP_Basic01"
{
   Properties
    {   
        // Properties Block : ���̴����� ����� ������ �����ϰ� �̸� material inspector�� �����ŵ�ϴ�
        _TintColor("Color", color) = (1,1,1,1)
        _Intencity("Range Sample", Range(0,1)) = 0.5
    }  
 	SubShader
	{  
	    Tags
        {
            //Render type�� Render Queue�� ���⼭ �����մϴ�.
	        "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"          
            "Queue"="Geometry"
        }
    	Pass
    	{  		
     	    Name "Universal Forward"
            Tags { "LightMode" = "UniversalForward" }

       	HLSLPROGRAM

        	#pragma prefer_hlslcc gles
        	#pragma exclude_renderers d3d11_9x
        	#pragma vertex vert
        	#pragma fragment frag

//cg shader�� .cginc�� hlsl shader�� .hlsl�� include�ϰ� �˴ϴ�.
       	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"        	
  
//vertex buffer���� �о�� ������ �����մϴ�. 	
         	struct VertexInput
         	{
            	float4 vertex : POSITION;
          	};

//�����⸦ ���� ���ؽ� ���̴����� �ȼ� ���̴��� ������ ������ �����մϴ�.
        	struct VertexOutput
          	{
           	    float4 vertex  	: SV_POSITION;
      	    };

            half4 _TintColor;

//���ؽ� ���̴�
      	VertexOutput vert(VertexInput v)
        	{
          	VertexOutput o;      
          	o.vertex = TransformObjectToHClip(v.vertex.xyz);

         	return o;
        	}

//�ȼ� ���̴�
        	half4 frag(VertexOutput i) : SV_Target
        	{ 
          	return half4(_TintColor);
        	}
        	ENDHLSL  
    	}
     }
}