# 66.Fluid Rendering in Real-Time - Final Version
This project focus on fluid rendering in real-time with configurable controls. The design of this project is based on an illumination model proposed  by an existing fluid rendering method: Screen Space Fluid Rendering With Curvature Flow.
## Tech & Support
- Unity with Scriptible Render Pipeline
- Multiple Render Targets
- C#
- HLSL
## Design
The proposed illumination model: The Visible Fluid Color = The Refraction Color + The Reflection Color + The Specular Highlight.
![diagram_4](https://user-images.githubusercontent.com/73683515/171355524-56175710-2af8-4f0f-bd63-6a1101963690.JPG)
## Simple Motion Simulation
- Lineaer wave
- Circular ripple
## Contains
- Custom Menu (debugging purpose)
- Scene
- Shader files: Fluid Objects (Wave x 2, Ripple x 2), Environment Objects (Floor x 1, Wall x 1), Cover Objects (Cover x 1), Other (CombineShader x 1)
- C# files: SRP Instance and Assest/ Render Object / Execute CombineShader / Mesh Related / Controllers Related / Preset Settings Related
- Cubemap for the reflection effect
- Images for the fluid surface and the container environment
## Configurable parameters
### Objects Appearances Control: 
- Floor Texture
- Floor Normal Map and on/off switch
- Wall Texture, Fluid Color
- Fluid Texture and on/off switch
- Thick Fluid Type and Transparency
- Cubemap Texture
### Fluid Geometry Control:
- Wave/Ripple toggle
- Amplitude Rate
- Frequency
- Speed
- Fluid Height
- Floor Height
### Light Source Control: 
- Light types (directional or point)
- Specular Colors
-Intensity
### Illumination Control: 
- Specular Intensity
- Floor’s Specular Intensity
- Highlight Intensity
- Environment Refractive Index
- Surface Thickness Divider
- Surface Thickness Side Divider
### Background: 
- Color
## Debugging/Visualization  on Partial Results
1. Modify if-statements in the CombineShader to output the color.
2. Or to add additional debugging items, add additional method and menu item in Editor/Menu.cs file
3. Current, we have:
4. ![menu2](https://user-images.githubusercontent.com/73683515/171358585-e0ec622d-0062-45d6-b025-42c59c9c7013.JPG)
## Some rendering results
![coloredwater](https://user-images.githubusercontent.com/73683515/171358636-62e53cb7-9f85-49ee-8258-42227b6c5883.gif)
![creek](https://user-images.githubusercontent.com/73683515/171358668-42ea1827-9db6-4395-b347-7c9a087727c5.gif)
![dirt](https://user-images.githubusercontent.com/73683515/171358684-f1c852cb-c435-45a7-afe2-1fd83f18d475.gif)
![swimmingpool](https://user-images.githubusercontent.com/73683515/171358691-b1971014-e4fb-44e0-a376-2be22dca2392.gif)
## Other
### Many thanks to Professor Kelvin Sung.

