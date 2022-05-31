using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _GreenSwimmingPool= false;
    public bool _MuddySwimmingPool = false;

    private void ApplyGreenSwimmingPool()
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
        _MyController.UpdateCubeMap(_Swimming);

        // update mats
        _MyController.UpdateWallTexture(_Wall8);
        _MyController.UpdateFloorTexture(_Wall8);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_SwimmingNormalMap);

        // update lights
        _MyController.UpdateLightIntensity(3);
        _LightController.UpdateLightColor(0, new Color(0.476f, 0.882f, 0.962f, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
        Transform light = GameObject.Find("Directional Light2").transform;
        _LightController.AddLightWithColor(light, new Color(0.801f, 0.774f, 0.2988f, 1));
    }

    private void ApplyMuddySwimmingPool()
    {
        // update transform
        SetFluidHeight(1.52f);
        SetFloorHeight(0);
        RotateFloorZ(0);

        // effects
        _MyController.UpdateShouldFluidTexture(true);
        _MyController.UpdateFluidColor(new Color(0.179f, 0.127f, 0.075f, 1));
        _MyController.UpdateFluidTexture(_Dirt2);
        _MyController.UpdateThickFluidType(true);
        _MyController.UpdateThickFluidTransparency(0.2f);
        _MyController.UpdateAlphaN(798);
        _MyController.UpdateGamma(0.283f);
        _MyController.UpdateSideGamma(0.086f);
        _MyController.UpdateThicknessDivider(0.01f);
        _MyController.UpdateThicknessSideDivider(0.05f);
        _MyController.UpdateCubeMap(_Swimming);

        // update mats
        _MyController.UpdateWallTexture(_Wall8);
        _MyController.UpdateFloorTexture(_Wall8);
        _MyController.UpdateFloorNormalMap(_SwimmingNormalMap);

        // update lights
        _MyController.UpdateLightIntensity(3);
        _LightController.UpdateLightColor(0, new Color(0, 0, 0, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
    }
}
