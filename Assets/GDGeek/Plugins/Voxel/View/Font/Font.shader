// ======================================================================================
// File         : SpriteBlendClipping.shader
// Author       : Wu Jie 
// Last Change  : 03/05/2012 | 15:19:25 PM | Monday,March
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
//
///////////////////////////////////////////////////////////////////////////////



Shader "GDGeek/Font" {
    Properties {
    
   		 _Color ("Main Color", Color) = (1,1,1,1)
   		 _FontColor ("Font Color", Color) = (0,0,0,1)
        _MainTex ("Font Texture", 2D) = "white" {}
        
        
        _FontUV ("Font Rect", Vector) = (0,0,0,0)
        _FontUV2 ("Font Rect2", Vector) = (0,0,0,0)
        _Size ("Size", Vector) = (0,0,0,0)
      //  _Alpha ("Alpha", float) = 0
       
    }


    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }
        
        Blend SrcAlpha OneMinusSrcAlpha 
       
        Lighting Off 
        Fog { Mode Off }

        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			fixed4 _Color;
			fixed4 _FontColor;
			float4 _FontUV;
			float4 _FontUV2;
			fixed4 _Size;
		
		
			struct vertexInput {
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal :NORMAL;
			};

			struct vertexOutput {
				float4 vertex        : POSITION;
				float2 texcoord      : TEXCOORD0;
				float pos : TEXCOORD1;
				fixed3 color : COLOR;
			};

			vertexOutput vert ( vertexInput input ) {
				vertexOutput output;
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
				
				float3 viewDir = normalize(WorldSpaceViewDir(input.vertex));
				float3 normalDir =  normalize(mul(float4(input.normal, 0.0), _World2Object).xyz);
				output.color = float3(1,1,1) * min(1.0, max(0.3, dot(normalDir, viewDir)) *1.5);
				output.pos = input.normal.z;
				return output;
			}

			fixed4 frag ( vertexOutput _in ) : COLOR {
				
				fixed4 size = _Size;
				float2 uv0 = _in.texcoord;
				float4 uv1 = _FontUV;
				float4 uv2 = _FontUV2;
				
				int w = _Size.x - _Size.y;
				int h = _Size.z - _Size.w;
				
				float x1 = (uv0.x-1) * (8.0/w) + 1;
				float y1 = uv0.y * (8.0/h);
				
				float x = 1-(floor(w*x1) +0.5)/w;
				float y = (floor(h*y1)+ 0.5)/h;
				
				float2 top = float2( uv1.x * (1 - x) + uv1.z* x, uv1.y * (1 - x) + uv1.w* x);
				float2 bottom = float2(uv2.x * (1 - x) + uv2.z* x, uv2.y * (1 - x) + uv2.w* x);
				
				float2 uv = top * (1-y) + bottom *y; 
				fixed4 c = tex2D (_MainTex, uv);
				
				 
				fixed4 color = fixed4(_in.color, 1)* _Color;
				if(_in.pos == -1){
					fixed4 co = _Color * (1-c.a) + _FontColor * c.a;
					color *= co;
					
				}
				if(_in.texcoord.x < (1-w/8.0)){
					color =_Color;
				}
				return color;
		
			}
			ENDCG
        }
    }

    // ======================================================== 
    // fallback 
    // ========================================================

    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }
        Cull Off 
        Lighting Off 
        ZWrite Off 
        Fog { Color (0,0,0,0) }
        Blend SrcAlpha OneMinusSrcAlpha 
        BindChannels {
            Bind "Color", color
            Bind "Vertex", vertex
            Bind "TexCoord", texcoord
        }

        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
