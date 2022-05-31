Shader "Unlit/BaseColor"
{
    Properties
    {
         _Color("Color", Color) = (0,0,0,1) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 100

        Cull OFF
        ZTest OFF 

        Pass
        {
            Tags { "LightMode"="ObjectA" "Queue"="Geometry + 1"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float4 worldpositions : TEXCOORD14;
            };

            
            fixed4 _Color;
            float4 _MainTex_ST;
            sampler2D _TesterTex;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv;
                o.worldpositions = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1));

                return o;
            }

            void frag (v2f i,
            out float4 GRT7:SV_Target7
            )
			{   
                // vertext world position + stencil(-1)
                GRT7 = float4(1,0,0,-6);
			}
            ENDCG
        }
    }
}
