Shader "Unlit/WaveShader"
{
    Properties
    {
        //_AmplitudeRate ("Wave Amplitude Rate", Range(0, 1.5)) = 0.7
        //_Frequency ("Wave Frequency", Range(0, 30)) = 0.1
        //_AnimationSpeed ("Wave Speed", Range(0, 10)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 100

        Cull OFF
        ZTest OFF

        Pass
        {
            Tags { "LightMode"="ObjectA" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent: TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float dist : float;
                float3 world_vertex: TEXCOORD9;
                float4 tangent : TEXCOORD10;
                float4 bitangent: TEXCOORD11;
                float3 ntangent: TEXCOORD12;
                float3 cameradir: TEXCOORD14;
                float3 lightdir: TEXCOORD15;
            };

            float _AmplitudeRate;
            float _Frequency;
            float _AnimationSpeed;
            float _FluidThickness;

            float4 _CameraPosition;
            float4 _LightPosition;

            sampler2D _FluidTexture;
            samplerCUBE  _CubeMap;


            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;

                // ref: https://www.ronja-tutorials.com/post/015-wobble-displacement/
                //float randomno =  frac(sin(dot(float2(0.02f, 0.5f),float2(12.9898,78.233)))*43758.5453123);
                //float randomno = (random(v.uv) + 0.1f) /5;
                //float randomno = (_Time.y % 100)/100;
                float randAmp = (_SinTime.w /50 + 0.05f) * _AmplitudeRate;
                randAmp *= _AmplitudeRate;

                o.vertex = v.vertex;
                o.vertex.y += sin(v.vertex.x * _Frequency + _Time.y * _AnimationSpeed) * randAmp;
                float3 distval = o.vertex;

                o.tangent = v.tangent;
                float3 posPlusTangent = v.vertex + v.tangent * 0.01;
                posPlusTangent.y += sin(posPlusTangent.x * _Frequency + _Time.y * _AnimationSpeed) * randAmp;

                float3 bitangent = cross(v.normal, v.tangent);
                o.bitangent = float4(bitangent, 1);
                float3 posPlusBitangent = v.vertex + bitangent * 0.01;
                posPlusBitangent.y += sin(posPlusBitangent.x * _Frequency + _Time.y * _AnimationSpeed) * randAmp;

                float3 modifiedTangent = posPlusTangent - o.vertex;
                float3 modifiedBitangent = posPlusBitangent - o.vertex;

                float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
                o.normal = normalize(UnityObjectToWorldNormal(modifiedNormal));

                o.ntangent = normalize(modifiedTangent);

                float3 worlddir = mul(unity_ObjectToWorld, float4(o.vertex.xyz, 1)).xyz;

                o.vertex = UnityObjectToClipPos(o.vertex);

                o.dist = length(WorldSpaceViewDir(float4(distval, v.vertex.w)));

                // for phong: 
                // light direction
                o.lightdir = worlddir - _LightPosition;
                // camera direction
                o.cameradir = worlddir - _CameraPosition;
                // vertex in world position
                o.world_vertex = worlddir;

                o.uv = v.uv;
                return o;
            }

            void frag (v2f i,
            out float4 GRT0:SV_TARGET0, // vertex in world position + depth (wave vertex to camera)
            out float4 GRT1:SV_TARGET1, // normal (3)
            out float4 GRT2:SV_TARGET2, // camera direction
            out float4 GRT4:SV_TARGET4  // fluid texture
            )
			{   // vertext in world position + depth : d1 color (wave to camera)   
				GRT0 = float4(i.world_vertex, i.dist);
                // normal + stencil
                GRT1 = float4(i.normal, 1);
                // phong: camera direction
                GRT2 = float4(i.cameradir, 1);
                // texture for fluid
                GRT4 = tex2D(_FluidTexture, i.uv);

                //float3 reflect_r =  reflect(i.cameradir, normalize(i.normal));
                //GRT2 = texCUBE(_CubeMap, reflect_r);
			}
            ENDCG
        }
    }
}
