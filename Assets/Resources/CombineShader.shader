Shader "Unlit/CombineShader"
{
   Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Transparent+400" "Queue"="Transparent"}


        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.0

            #include "UnityCG.cginc"

            Texture2D _Buffer0;
            Texture2D _Buffer1;
            Texture2D _Buffer2;
            Texture2D _Buffer3;
            Texture2D _Buffer4;
            Texture2D _Buffer5;
            Texture2D _Buffer6;
            Texture2D _Buffer7;

            Texture2D _Depth;
            SamplerState sampler_pointer_clamp;

            int _Mode;
            float4 _CameraPosition;
            float4 _LightPosition;
            float _AngleCameraLight;
            float4 _BackgroundComboColor;
            float4 _BackgroundColor;
            float4 _FluidColor;
            int _ShouldKsSingle;
            float _ksSingle;
            float4 _ks;
            float _alpha;
            float _gamma;
            float _side_gamma;
            float _ThicknessDivider;
            float _ThicknessSideDivider;
            int _FloorNormalMapOn;


            // lights
            //float4 _LightPosition_0;
            //loat4 _LightPosition_1;
            //float4 _ks_0;
            //float4 _ks_1;

            //static int _PointLightsLength = 2;
            //static int _DirectionalLightsLength = 2;

            //uniform float3 _PointLights[2];
            //uniform float4 _PointLightsColor[2];

            //uniform float3 _DirectionalLights[2];

            static int _LightPositionsCount = 10;

            uniform float4 _LightPositions[10];
            uniform float4 _LightColors[10];

            float _intensity;

            float4 fluid_pattern;
            int _ShouldFluidTexture;
            int _ThickFluidType;
            float _ThickFluidTransparency;

            samplerCUBE  _CubeMap;


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2ff
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 eyepositions : TEXCOORD14;
            };

            struct GBufferOutput
            {
                half4 GBuffer4 : SV_Target4;
            };

            v2ff vert (appdata v)
            {
                v2ff o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                o.eyepositions = mul(UNITY_MATRIX_MV, v.vertex);
                return o;
            }

            float4 getColorWithStencil(float4 _stencil, float4 o_color) {
                //return o_color * _stencil.a;
                if (_stencil.a != 0) {
                    return o_color;
                }
                return o_color * 0;
            }

            float4 getRefractedFluidColor(float thickness, float4 _stencil, float4 _floorTexture) {
                // function for a
                float e_constant = 2.71828f;

                float4 _color1_fluid = _FluidColor;
                float4 _color2_scene = _floorTexture; 
                if (_ShouldFluidTexture == 1) {
                    _color1_fluid = fluid_pattern; 
                }
                //if (_color2_scene.a == 0) {
                //    _color2_scene = _BackgroundComboColor;
                //}
                // ref: https://www.desmos.com/calculator

                float4 _blend = pow(e_constant, -thickness);
                float4 a = lerp(_color1_fluid, _color2_scene, _blend);
                return a;
            }

            float fresnelFunction(float3 normal, float3 viewDir){
                float fresnelValue = 1 - saturate(dot(normal, viewDir));
                return fresnelValue;
            }

            float4 colorout (
                float3 normal, 
                float3 vert, 
                float thickness, 
                float4 _stencil, 
                float3 _cameraDir, 
                //float3 _lightDir, 
                float4 _floorTexutre,
                float environment_obstacle_stencil
            ) {
                if (_stencil.a == 0 && environment_obstacle_stencil == 0) {
                    return _BackgroundColor;
                }

                if (_stencil.a == 0) {
                    return _BackgroundComboColor;
                }
                // the refracted fluid color
                float4 a = getRefractedFluidColor(thickness, _stencil, _floorTexutre);

                // the reflected scene color - making something up...for testing...
                float4 b = _BackgroundComboColor;
                
                // constants for the specular highlight -> Phong
                float4 ks = _ks;
                float alpha = _alpha;
                // surface normal
                float3 n = normal;
                // half-angle between canmera and the Light
                float h = _AngleCameraLight/2;
                // camera vector
                float3 v = _cameraDir; // seems to be depth? the camera vector?
                //float3 l = _lightDir;
                float3 nnv = _CameraPosition - vert;
                // normalize
                float3 nv = normalize(v);
                //float3 nl = normalize(l);
                float3 nn = normalize(n);

                //float3 normal_light_0 = normalize(vert - _LightPositions[0]);
                //float3 nr = normalize((2 * dot(nv,nn) * nn) - nv);


                // cubemap
                //float4 _SampleCubemap_Out = SAMPLE_TEXTURECUBE_LOD(Cubemap, Sampler, reflect(-ViewDir, Normal), LOD);
                //float3 reflect_r = 2 * (dot(nv, nn) * nn-nv);
                float3 reflect_r =  reflect(-normalize(nnv), n);
                float4 sample_Cube = texCUBE(_CubeMap, reflect_r) * 0.3 + b *0.7;
                //float4 sample_Cube = b;
                //float4 sample_Cube = texCUBE(_CubeMap, vert) * 0.4 + b *0.6;
                if (_stencil.a == -7) {
                    // fluid side should not have reflection!
                    sample_Cube = b;
                }
                //return sample_Cube;

                
                float4 color1 = a * (1-fresnelFunction(nn,nv)); // can be removed stencil, now for debug purpose
                float4 color2 = float4(sample_Cube.xyz * fresnelFunction(nn,nv), 1); //fresnelFunction(nn,nv)
                //color1 = a;
                //color2 = float4(_cameraDir,1);
                //float4 color2 = float4(b.xyz * fresnelFunction(nn,nv), 1);
                //color2 = sample_Cube;
                // method 1
                float3 colors_lights = float3(0,0,0);
                /*
                for (int i=0; i<_PointLightsLength; i++) {
                    float3 normal_light = normalize(_PointLights[i]);
                    float3 nr = normalize((2 * dot(normal_light,nn) * nn) - normal_light);
                    float3 spec = float3(0,0,0);
                    if (_ShouldKsSingle == 1) {
                        spec =  _ksSingle * pow(dot(nr,nv), alpha);
                    } else {
                        spec =  _PointLightsColor[i].xyz * pow(dot(nr,nv), alpha);
                    }

                    if (dot(normal_light,nn) <0 || dot(nr,nv) < 0) {
                        spec = float3(0,0,0);
                    }
                    colors_lights += spec;
                }
                */

                
                for (int i=0; i<_LightPositionsCount; i++) {
                
                    //if (_LightColors[i].a != -1) {

                    float3 normal_light = normalize(vert - _LightPositions[i]);
                    float3 nr = normalize((2 * dot(normal_light,nn) * nn) - normal_light);
                    float3 spec = float3(0,0,0);
                    if (_ShouldKsSingle == 1) {
                        spec =  _ksSingle * pow(dot(nr,nv), alpha);
                    } else {
                        spec =  _ksSingle * _LightColors[i] * pow(dot(nr,nv), alpha);
                    }

                    if (dot(normal_light,nn) <0 || dot(nr,nv) < 0) {
                        spec = float3(0,0,0);
                    }
                    colors_lights += spec;

                    //}
                    
                }
                

                float4 color3 = float4(colors_lights, 1);

                // method 2 from paper
                /*
                spec =  0.2 * pow(dot(n, dot(nv, nl)/2), alpha);
                color3 = float4(spec, 1);
                if (dot(n, dot(nv, nl)/2) < 0) {
                    color3 = float4(0,0,0,0);
                }
                */
                //return color1 + color2 + color3;
                float4 c = getColorWithStencil(_stencil, color1 + color2 + color3);
                if (_ThickFluidType) {
                    c = getColorWithStencil(_stencil, color1 + color2 * _ThickFluidTransparency + color3);
                }

                // for menu
                if (_Mode == 5) {
                    // refrection
                   c = color2;
                } else if (_Mode == 6) {
                    // reflaction
                   c = color1;
                } else if (_Mode == 7) {
                    // Phong specular highlight
                    c = color3;
                }
                return c;
            }

            float getThickness(
                float wave_stencil, 
                float3 wave_world_position, 
                float3 environment_obstacle_world_position, 
                float environment_obstacle_stencil
            ) {
                if (wave_stencil == 1 || wave_stencil == -7) {
                    if (environment_obstacle_stencil != -1 && environment_obstacle_stencil != -2) {
                        return 5;
                    }
                    float thickness = length(environment_obstacle_world_position - wave_world_position);
                    if (wave_stencil == 1) {
                        thickness /= _ThicknessDivider;
                    } else if (wave_stencil == -7) {
                        thickness /= _ThicknessSideDivider;
                    }
                    return thickness;
                }
                return 5;
            }

            float4 getScene_Sfunction(
                float wave_stencil,
                float thickness, 
                float2 _uv, 
                float3 _floor_vertex, 
                float _stencil
            ) {
                float _thisgamma = _gamma;
                if (wave_stencil == -7) {
                    _thisgamma = _side_gamma;
                }
                float2 uv_extra = float2(_uv.x + thickness * _thisgamma, _uv.y + thickness * _thisgamma);
                //float4 g6 = _GBuffer6.Sample(sampler_pointer_clamp, uv_extra);
                //float4 floor_normal_map = _FloorNormalMap.Sample(sampler_pointer_clamp, uv_extra);
                float4 g6 = _Buffer6.Sample(sampler_pointer_clamp, uv_extra);
                float4 floor_normal_map = _Buffer3.Sample(sampler_pointer_clamp, uv_extra);
                //return (_FloorNormalMapFactor_1 + _FloorNormalMapFactor_2 * cos(floor_normal_map.y)) * g6;
                //return  cos(floor_normal_map.y) * g6;

                //ColorFloor * light intensity * ColorLight * (n dot l)

                if (_stencil == -1) {
                    float4 colors = float4(0,0,0,0);
                    for (int i=0; i<_LightPositionsCount; i++) {
                        float3 light_dir = _LightPositions[i] - _floor_vertex;
                        float ld = length(light_dir);
                        light_dir = light_dir * (1.0/ld);
                        float4 n4 = (2 * (floor_normal_map))-1.0;
                        float3 n = normalize(n4.xyz);
                        float3 un = float3(n.x, n.z, n.y);
                        float n_dot_l = max(dot(un,light_dir), 0.01);
                        colors += _LightColors[i] * n_dot_l;
                    }
                    if (_FloorNormalMapOn == 1) {
                        return g6 * _intensity * colors;
                    }
                    return g6;

                }
                return g6;
            }

            /*
            void addLightDirectionsToList(float3 fluid_vertex) {
                float3 light0 = fluid_vertex - _LightPosition_0.xyz;
                float3 light1 = fluid_vertex - _LightPosition_1.xyz;
                _PointLights[0] = light0;
                _PointLights[1] = light1;
                _PointLightsColor[0] = _ks_0;
                _PointLightsColor[1] = _ks_1;
            }
            */

			half4 frag (v2ff i): SV_Target
			{            
                float4 g0 = _Buffer0.Sample(sampler_pointer_clamp, i.uv); // (4) vertex in world position + depth (wave vertex to camera)
                float4 g1 = _Buffer1.Sample(sampler_pointer_clamp, i.uv); // (4) normal + wave stencil(1)
                float4 g2 = _Buffer2.Sample(sampler_pointer_clamp, i.uv); // (3) camera direction
                float4 g3 = _Buffer3.Sample(sampler_pointer_clamp, i.uv); // (3) floor normal map texture
                float4 g4 = _Buffer4.Sample(sampler_pointer_clamp, i.uv); // (empty) - front wall position + setncil (-3) texture
                float4 g5 = _Buffer5.Sample(sampler_pointer_clamp, i.uv); // (4) floor/wall world position + setncil wall(-2) floor(-1)
                float4 g6 = _Buffer6.Sample(sampler_pointer_clamp, i.uv); // (3/4) back walls + floor texture
                float4 g7 = _Buffer7.Sample(sampler_pointer_clamp, i.uv); // (empty) - front wall texture

                // fluid
                fluid_pattern = g4;
                float3 fluid_vertex_world_position = g0.xyz;
                float depth = g0.a;
                float3 fluid_normal = g1.xyz;
                float fluid_stencil = g1.a;
                float3 camera_direction = g2.xyz;

                
                //lights
                //addLightDirectionsToList(fluid_vertex_world_position);
                //float3 light_direction = g3.xyz;
                // environment: wall, floor
                float3 environment_obstacle_world_position = g5.xyz;
                float environment_obstacle_stencil = g5.a;
                // environment generate
                //if (g5.a != -1) {
                    _BackgroundComboColor = g6;
                //}
                //float3 _BackgroundComboColor = g6;
                // fluid thickness
                float thickness = getThickness(
                    fluid_stencil, 
                    fluid_vertex_world_position, 
                    environment_obstacle_world_position, 
                    environment_obstacle_stencil
                    );
                // For S function refraction part
                float4 environment_obstacle_floor_texture_with_refraction = getScene_Sfunction(fluid_stencil, thickness, i.uv, environment_obstacle_world_position, environment_obstacle_stencil);
                //return environment_obstacle_floor_texture_with_refraction;
                //return environment_obstacle_floor_texture_with_refraction;
                // expensive, maybe should have a overall flag on debug mode or not
                if (_Mode == 0) {
                    // all

                    if (g7.a == -6 && g5.a != -1) {
                        //return float4(g7.xyz, 1);
                        return _BackgroundColor;
                    }
                    return colorout(
                        fluid_normal, 
                        fluid_vertex_world_position, 
                        thickness, 
                        fluid_stencil, 
                        camera_direction, 
                        //light_direction, 
                        environment_obstacle_floor_texture_with_refraction,
                        environment_obstacle_stencil);
                } else if (_Mode == 1) {
                    // depth
                    return float4(depth, depth, depth, 1);
                } else if (_Mode == 2) {
                    // normal
                    return float4(fluid_normal, 1);
                } else if (_Mode == 3) {
                    // thickness
                    return float4(thickness, thickness, thickness, 1);
                } else if (_Mode == 4) {
                    // floor texture modified
                    return environment_obstacle_floor_texture_with_refraction;
                } else if (_Mode == 5) {
                    // reflection
                    return colorout(
                        fluid_normal, 
                        fluid_vertex_world_position, 
                        thickness, 
                        fluid_stencil, 
                        camera_direction, 
                        //light_direction, 
                        environment_obstacle_floor_texture_with_refraction,
                        environment_obstacle_stencil);
                } else if (_Mode == 6) {
                    // refraction
                    return colorout(
                        fluid_normal, 
                        fluid_vertex_world_position, 
                        thickness, 
                        fluid_stencil, 
                        camera_direction, 
                        //light_direction, 
                        environment_obstacle_floor_texture_with_refraction,
                        environment_obstacle_stencil);
                } else if (_Mode == 7) {
                    // Phong specular highlight
                    return colorout(
                        fluid_normal, 
                        fluid_vertex_world_position, 
                        thickness, 
                        fluid_stencil, 
                        camera_direction, 
                        //light_direction, 
                        environment_obstacle_floor_texture_with_refraction,
                        environment_obstacle_stencil);
                } else if (_Mode == 8) {
                    // lerp scene and fluid color 
                    return getRefractedFluidColor(
                        thickness, 
                        fluid_stencil, 
                        environment_obstacle_floor_texture_with_refraction);
                } else if (_Mode == 9) {
                    // wall testing
                    
                    float4 _color = colorout(
                        fluid_normal, 
                        fluid_vertex_world_position, 
                        thickness, 
                        fluid_stencil, 
                        camera_direction, 
                        //light_direction, 
                        environment_obstacle_floor_texture_with_refraction,
                        environment_obstacle_stencil);
                    
                    if (g4.a == -3) {
                        return g7;
                    }
                    if (environment_obstacle_stencil == -2 && (fluid_stencil != 1 || fluid_stencil != 7))
                    if (fluid_stencil != 1 || fluid_stencil != 7) {
                        return g6;
                    }
                    
                    return _color;
                    
                    
                    //return float4(fluid_stencil, 0,0,1);
                }

                return colorout(
                    fluid_normal, 
                    fluid_vertex_world_position, 
                    thickness, 
                    fluid_stencil, 
                    camera_direction, 
                    //light_direction, 
                    environment_obstacle_floor_texture_with_refraction,
                    environment_obstacle_stencil);
              
			}

            ENDCG
        }
    }
}
