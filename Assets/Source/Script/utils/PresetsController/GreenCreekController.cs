using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _GreenCreek = false;

    private void ApplyGreenCreek()
    {
        // update transform
        SetFluidHeight(5.15f);
        SetFloorHeight(0);
        RotateFloorZ(0);

        // effects
        _MyController.UpdateShouldFluidTexture(false);
        _MyController.UpdateFluidColor(new Color(0.17f, 0.575f, 0.143f, 1));
        _MyController.UpdateThickFluidType(false);
        _MyController.UpdateAlphaN(85);
        _MyController.UpdateGamma(0.14f);
        _MyController.UpdateSideGamma(0.001f);
        _MyController.UpdateThicknessDivider(6.3f);
        _MyController.UpdateThicknessSideDivider(2.5f);
        _MyController.UpdateCubeMap(_Regular);

        // update mats
        _MyController.UpdateWallTexture(_GreenWall);
        _MyController.UpdateFloorTexture(_RiverBed1);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_NormalMap2);

        // update lights
        _MyController.UpdateLightIntensity(3);
        _LightController.UpdateLightColor(0, new Color(0.476f, 0.882f, 0.962f, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
        Transform light = GameObject.Find("Directional Light2").transform;
        _LightController.AddLightWithColor(light, new Color(0.801f, 0.774f, 0.2988f, 1));
    }
}

