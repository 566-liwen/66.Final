using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public Controller _MyController;
    public LightsController _LightController;

    public Transform _Fluid = null;
    public Transform _Floor = null;

    [Range(0, 11)]
    public float _FluidHeight = 2;
    [Range(0, 5)]
    public float _FloorHeight = 0;


    private static float _FluidHightOriginal = -11;
    private static float _FloorHightOriginal = 2.5f;

    private static float _FluidFloorDepth;

    public void SetFluidHeight(float height)
    {
        _FluidHeight = height;
        float fluid_height = _FluidHightOriginal + height;
        _Fluid.localPosition = new Vector3(_Fluid.localPosition.x, fluid_height, _Fluid.localPosition.z);
    }

    public void SetFloorHeight(float height)
    {
        _FloorHeight = height;
        float floor_height = _FloorHightOriginal + height;
        _Floor.localPosition = new Vector3(_Floor.localPosition.x, floor_height, _Floor.localPosition.z);
    }

    public void RotateFloorZ(float z)
    {
        //_Floor.Rotate(0, 0, z);
        Vector3 angles = _Floor.localRotation.eulerAngles;
        angles.z = z;
        _Floor.transform.localRotation = Quaternion.Euler(angles);
    }

    void Start()
    {
        float fluid_height = _FluidHightOriginal + _FluidHeight;
        _Fluid.localPosition = new Vector3(_Fluid.localPosition.x, fluid_height, _Fluid.localPosition.z);

        float floor_height = _FloorHightOriginal + _FloorHeight;
        _Floor.localPosition = new Vector3(_Floor.localPosition.x, floor_height, _Floor.localPosition.z);

        _FluidFloorDepth = _Fluid.position.y - _Floor.position.y;
    }

    void Update()
    {
        float fluid_height = _FluidHightOriginal + _FluidHeight;
        float floor_height = _FloorHightOriginal + _FloorHeight;

        if (_Fluid.position.y + _FluidHeight <= _Floor.position.y + _FloorHeight)
        {
            return;
        }

        _Fluid.localPosition = new Vector3(_Fluid.localPosition.x, fluid_height, _Fluid.localPosition.z);
        _Floor.localPosition = new Vector3(_Floor.localPosition.x, floor_height, _Floor.localPosition.z);

        // below is for auto-adjust transparency of fluid side
        float depth = _Fluid.position.y - _Floor.position.y;
        float setting = 3;
        float new_setting = (depth * setting) / _FluidFloorDepth;
        //Shader.SetGlobalFloat("_ThicknessSideDivider", new_setting);

        if (_SlantedFloor)
        {
            ApplySlantedFloor();
            _SlantedFloor = false;
        } 
        else if (_SwimmingPoolRegular)
        {
            ApplySwimmingPoolRegular();
            _SwimmingPoolRegular = false;
        } 
        else if (_SlantedSwimmingPool)
        {
            ApplySlantedSwimmingPool();
            _SlantedSwimmingPool = false;
        }
        else if (_ColoredWaterWithFlowers)
        {
            ApplyClearWater();
            _ColoredWaterWithFlowers = false;
        }
        else if (_GreenCreek)
        {
            ApplyGreenCreek();
            _GreenCreek = false;
        }
        else if (_MuddyFluid)
        {
            ApplyMuddyFluid();
            _MuddyFluid = false;
        }
        else if (_GreenSwimmingPool)
        {
            ApplyGreenSwimmingPool();
            _GreenSwimmingPool = false;
        }
        else if (_MuddySwimmingPool)
        {
            ApplyMuddySwimmingPool();
            _MuddySwimmingPool = false;
        }
    }
}
