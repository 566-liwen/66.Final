Shader "Unlit/FloorShader"
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
        ZTest Off 

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
                float3 worldpositions : TEXCOORD14;
            };

            
            fixed4 _Color;
            float4 _MainTex_ST;
            sampler2D _TesterTex;
            sampler2D _FloorNormalMap;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv;
                float4 worldpositions_all = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1));
                o.worldpositions = worldpositions_all.xyz/worldpositions_all.w;

                return o;
            }

            void frag (v2f i,
            out float4 GRT5:SV_Target5, // for position
            out float4 GRT6:SV_Target6, // for texture
            out float4 GRT3:SV_Target3 // for normal texture
            )
			{   
                // vertext world position + stencil(-1)
                GRT5 = float4(i.worldpositions, -1);
                // texture
                GRT6 = tex2D(_TesterTex, i.uv);
                GRT3 = tex2D(_FloorNormalMap, i.uv);
			}
            ENDCG
        }
    }
}
