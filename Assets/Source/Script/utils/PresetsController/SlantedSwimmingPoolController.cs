using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _SlantedSwimmingPool = false;

    private void ApplySlantedSwimmingPool()
    {
        // update transform
        SetFluidHeight(6.2f);
        SetFloorHeight(1.9f);
        RotateFloorZ(-9.327f);

        // effects
        _MyController.UpdateShouldFluidTexture(false);
        _MyController.UpdateFluidColor(new Color(0.01f, 0.334f, 0.707f, 1));
        _MyController.UpdateThickFluidType(false);
        _MyController.UpdateAlphaN(90);
        _MyController.UpdateGamma(0.025f);
        _MyController.UpdateSideGamma(0.15f);
        _MyController.UpdateThicknessDivider(11.7f);
        _MyController.UpdateThicknessSideDivider(12f);
        _MyController.UpdateCubeMap(_Swimming);

        // update mats
        _MyController.UpdateWallTexture(_Wall8);
        _MyController.UpdateFloorTexture(_Wall8);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_SwimmingNormalMap);

        // update lights
        _MyController.UpdateLightIntensity(1.1f);
        _LightController.UpdateLightColor(0, new Color(0.13f, 0.23f, 0.53f, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
    }
}
