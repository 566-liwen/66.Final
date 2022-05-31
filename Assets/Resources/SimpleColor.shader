Shader "Unlit/SimpleColor"
{
    Properties
    {
         _Color("Color", Color) = (0,0,0.5,1) 
         //_TesterTex("Background Tester Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Cull OFF

        Pass
        {
            Tags { "LightMode"="ObjectA" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            
            fixed4 _Color;
            float4 _MainTex_ST;
            sampler2D _TesterTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i): SV_Target
			{                
                //float2 topDownProjection = i.worldPos.xz;
				//GRT5 = tex2D(_TesterTex, i.uv);
                //GRT6 = fixed4(0,0,0,1); // stencil
                return fixed4(0,1,0,1);
			}
            ENDCG
        }
    }
}
