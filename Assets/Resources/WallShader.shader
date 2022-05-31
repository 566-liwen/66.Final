Shader "Unlit/WallShader"
{
    Properties
    {
         _Color("Color", Color) = (0,0,0,1) 
        _AmplitudeRate ("Wave Amplitude Rate", Range(0, 1.5)) = 0.7
        _Frequency ("Wave Frequency", Range(0, 30)) = 0.1
        _AnimationSpeed ("Wave Speed", Range(0, 10)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1"}
        LOD 100

        Cull OFF
        ZTest Off 

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

            sampler2D _WallTex;

            float _AmplitudeRate;
            float _Frequency;
            float _AnimationSpeed;
            float _FluidThickness;

            v2f vert (appdata v)
            {
                v2f o;

                //if (v.uv.y < 2) {
                    //v.vertex.z += sin(v.vertex.x * _Frequency + _Time.y * _AnimationSpeed) * _AmplitudeRate * 1.6;
                //}
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv;
                o.worldpositions = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1));

                return o;
            }

            void frag (v2f i,
            out float4 GRT5:SV_Target5,
            out float4 GRT6:SV_Target6
            )
			{   
                // vertext world position + stencil(-2)
                GRT5 = float4(i.worldpositions.xyz, -2);
                GRT6 = tex2D(_WallTex, i.uv);
			}
            ENDCG
        }
    }
}
