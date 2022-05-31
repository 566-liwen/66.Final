using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidShapeHelper : MonoBehaviour
{
    public bool ShowWave = false;
    public bool ShowRipple = false;

    [Range(0, 1.5f)]
    public float _RippleRate = 0.2f;
    [Range(0.2f, 2)]
    public float _RippleFrequency = 0.77f;
    [Range(0, 0.5f)]
    public float _RippleAnimationSpeed = 0.1f;

    [Range(0, 1.5f)]
    public float _WaveAmplitudeRate = 0.032f;
    [Range(0, 30)]
    public float _WaveFrequency = 8.3f;
    [Range(0, 10)]
    public float _WaveAnimationSpeed = 1.23f;

    public Renderer _Fluid;
    public Renderer _SideFluid;

    private Material _MainRipple;
    private Material _MainWave;
    private Material _SideRipple;
    private Material _SideWave;

    private void Awake()
    {
        _MainRipple = Resources.Load<Material>("Ripple");
        _MainWave = Resources.Load<Material>("Wave");
        _SideRipple = Resources.Load<Material>("SideRipple");
        _SideWave = Resources.Load<Material>("SideFluidMat");
    }

    void Start()
    {
        //Renderer meshRenderer = GetComponent<Renderer>();
        // Get the current material applied on the GameObject
        _Fluid.material = _MainWave;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowWave)
        {
            _Fluid.material = _MainWave;
            _SideFluid.material = _SideWave;
            ShowWave = false;
        } else if (ShowRipple)
        {
            _Fluid.material = _MainRipple;
            _SideFluid.material = _SideRipple;
            ShowRipple = false;
        }
        Shader.SetGlobalFloat("_RippleRate", _RippleRate);
        Shader.SetGlobalFloat("_RippleFrequency", _RippleFrequency);
        Shader.SetGlobalFloat("_RippleAnimationSpeed", _RippleAnimationSpeed);

        Shader.SetGlobalFloat("_AmplitudeRate", _WaveAmplitudeRate);
        Shader.SetGlobalFloat("_Frequency", _WaveFrequency);
        Shader.SetGlobalFloat("_AnimationSpeed", _WaveAnimationSpeed);
    }
}
