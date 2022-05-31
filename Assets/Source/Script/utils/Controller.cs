using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Camera _Camera;

    public Texture2D _FloorTexture = null;
    public bool _FloorNormalMapOn = false;
    public Texture2D _FloorNormalMap = null;
    public Texture2D _WallTexture = null;
    public bool _ShouldFluidTexture = false;
    public Color _FluidColor = Color.blue;
    public Texture2D _FluidTexture = null;
    public bool _ThickFluidType = false;
    [Range(0, 1)]
    public float _ThickFluidTransparency = 1;
    public Color _BackgroundColor = Color.white;
    private bool _ShouldKsSingle = false;
    [Range(0.01f, 1)]
    //_PhongKsIntensity
    public float _SpecularIntensity = 1;
    [Range(0.05f, 20)]
    public float _SpecularIntensityOnFloor = 0.5f;
    //_PhongAlpha
    [Range(0.5f, 1000)]
    public float _HighlightIntensity = 16;
    [Range(0.001f, 1)]
    //_FloorThicknessGamma
    public float _EnvironmentRefractiveIndex = 0.01f;
    [Range(0.001f, 1)]
    //_FloorSideThicknessGamma
    public float _EnvironmentSideRefractiveIndex = 0.01f;
    [Range(0.01f, 30)]
    //FloorThicknessDivider 
    public float _SurfaceThicknessDivider = 3.6f;
    [Range(0.01f, 30)]
    //FloorSideThicknessDivider
    public float _SurfaceSideThicknessDivider = 3;

    public Cubemap _Cube;

    private void Awake()
    {
        //Shader.SetGlobalFloat("_ThicknessSideDivider", _FloorThicknessSide_Divider);
        
    }


    void FixedUpdate()
    {
            Shader.SetGlobalTexture("_TesterTex", _FloorTexture);
            Shader.SetGlobalInt("_FloorNormalMapOn", _FloorNormalMapOn ? 1 : 0);
            Shader.SetGlobalTexture("_FloorNormalMap", _FloorNormalMap);
            Shader.SetGlobalTexture("_WallTex", _WallTexture);
            Shader.SetGlobalInt("_ShouldFluidTexture", _ShouldFluidTexture ? 1 : 0);
            Shader.SetGlobalColor("_FluidColor", _FluidColor);
            Shader.SetGlobalTexture("_FluidTexture", _FluidTexture);
            Shader.SetGlobalInt("_ThickFluidType", _ThickFluidType ? 1 : 0);
            Shader.SetGlobalFloat("_ThickFluidTransparency", _ThickFluidTransparency);
            Shader.SetGlobalColor("_BackgroundColor", _BackgroundColor);
            //_Camera.backgroundColor = _BackgroundColor;
            Shader.SetGlobalInt("_ShouldKsSingle", _ShouldKsSingle? 1 : 0);
            Shader.SetGlobalFloat("_ksSingle", _SpecularIntensity);
            Shader.SetGlobalFloat("_intensity", _SpecularIntensityOnFloor);
            Shader.SetGlobalFloat("_alpha", _HighlightIntensity);
            Shader.SetGlobalFloat("_gamma", _EnvironmentRefractiveIndex);
            Shader.SetGlobalFloat("_side_gamma", _EnvironmentSideRefractiveIndex);
            Shader.SetGlobalFloat("_ThicknessDivider", _SurfaceThicknessDivider);
            Shader.SetGlobalFloat("_ThicknessSideDivider", _SurfaceSideThicknessDivider);
            Shader.SetGlobalTexture("_CubeMap", _Cube);
    }

    // below are setters

    public void UpdateCubeMap(Cubemap v)
    {
        _Cube = v;
    }

    public void UpdateUseFloorNormalMap(bool v)
    {
        _FloorNormalMapOn = v;
    }

    public void UpdateGamma(float v)
    {
        _EnvironmentRefractiveIndex = v;
    }

    public void UpdateSideGamma(float v)
    {
        _EnvironmentSideRefractiveIndex = v;
    }

    public void UpdateThicknessDivider(float v)
    {
        _SurfaceThicknessDivider = v;
    }

    public void UpdateThicknessSideDivider(float v)
    {
        _SurfaceSideThicknessDivider = v;
    }

    public void UpdateFloorTexture(Texture2D v)
    {
        _FloorTexture = v;
    }

    public void UpdateFloorNormalMap(Texture2D v)
    {
        _FloorNormalMap = v;
    }

    public void UpdateWallTexture(Texture2D v)
    {
        _WallTexture = v;
    }

    public void UpdateFluidColor(Color v)
    {
        _FluidColor = v;
    }
    public void UpdateShouldFluidTexture(bool v)
    {
        _ShouldFluidTexture = v;
    }

    public void UpdateFluidTexture(Texture2D v)
    {
        _FluidTexture = v;
    }

    public void UpdateThickFluidType(bool v)
    {
        _ThickFluidType = v;
    }
    public void UpdateThickFluidTransparency(float v)
    {
        _ThickFluidTransparency = v;
    }
    public void UpdateAlphaN(float v)
    {
        _HighlightIntensity = v;
    }

    public void UpdateLightIntensity(float v)
    {
        _SpecularIntensityOnFloor = v;
    }
}
