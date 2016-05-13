// ======================================================================================
// File         : SpriteBlendClipping.shader
// Author       : Wu Jie 
// Last Change  : 03/05/2012 | 15:19:25 PM | Monday,March
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
//
///////////////////////////////////////////////////////////////////////////////

Shader "EZ/Side2" {
    Properties {
        _MainTex ("Atlas Texture", 2D) = "white" {}
        _skewX ("skewX", float) = 0
        _skewY ("skewY", float) = 0
        _offsetX ("offsetX", float) = 0
        _offsetY ("offsetY", float) = 0 
        _side ("side", float) = 0 
        _alpha ("alpha", float) = 0 
        
    }

    // ======================================================== 
    // cg 
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
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _skewX = 0.0; 
			float _skewY = 0.0; 
			float _side = 0.1; 
			float _alpha = 1; 
			float _offsetX = 0.0; 
			float _offsetY = 0.0; 
			float4x4 _ClipMatrix;

			struct appdata_t {
				float4 vertex   : POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex        : POSITION;
				fixed4 color         : COLOR;
				float2 texcoord      : TEXCOORD0;
				float2 worldPosition : TEXCOORD1;
			};

			v2f vert ( appdata_t _in ) {
				v2f o;
                float4 wpos = mul(_Object2World, _in.vertex);
                o.worldPosition = mul(_ClipMatrix, wpos).xy;
                float4 ve = _in.vertex;
                 
				float x = sin(_skewX) *  (_in.vertex.y - _offsetY);
				float y = cos(_skewX) *  (_in.vertex.y - _offsetY);
				
				
				float x2 = cos(_skewY) *  (_in.vertex.x - _offsetX);
				float y2 = sin(_skewY) *  (_in.vertex.x - _offsetX);
			
			 
				ve.x = x + x2 + _offsetX;
				ve.y = y + y2 + _offsetY;
			
			
                
				o.vertex = mul(UNITY_MATRIX_MVP, ve); 
				o.color = _in.color; 
				o.texcoord = TRANSFORM_TEX(_in.texcoord, _MainTex); 
				
				
	 	 	
	 	 	
	 	 	
				return o;
			}

			fixed4 frag ( v2f _in ) : COLOR {
            	
            	fixed4 outColor = tex2D ( _MainTex, _in.texcoord);
				
				fixed4 out1 = tex2D ( _MainTex, _in.texcoord +float2(_side,0) );
				fixed4 out2 = tex2D ( _MainTex, _in.texcoord +float2(0,_side) );
				
				fixed4 out3 = tex2D ( _MainTex, _in.texcoord +float2(0.5f * _side, -0.866f * _side) );
				fixed4 out4 = tex2D ( _MainTex, _in.texcoord +float2(-0.5f * _side, -0.866f * _side) ); 
				
				
				fixed4 out5 = tex2D ( _MainTex, _in.texcoord +float2(0.5f * _side, -0.866f * _side) );
				fixed4 out6 = tex2D ( _MainTex, _in.texcoord +float2(-0.5f * _side, -0.866f * _side) ); 
            	outColor = _in.color*(1-outColor.a) + outColor*outColor.a;
            	outColor.a = out1.a+out2.a +out3.a+out4.a + out5.a+out6.a;
            	outColor.a = outColor.a * _alpha;	
                return outColor;
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
