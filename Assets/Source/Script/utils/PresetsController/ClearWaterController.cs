using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _ColoredWaterWithFlowers = false;

    private void ApplyClearWater()
    {
        // update transform
        SetFluidHeight(1.9f);
        SetFloorHeight(0);
        RotateFloorZ(0);

        // effects
        _MyController.UpdateShouldFluidTexture(false);
        _MyController.UpdateFluidColor(new Color(0.33f, 0.42f, 0.707f, 1));
        _MyController.UpdateThickFluidType(false);
        _MyController.UpdateAlphaN(53);
        _MyController.UpdateGamma(0.026f);
        _MyController.UpdateSideGamma(0.019f);
        _MyController.UpdateThicknessDivider(1);
        _MyController.UpdateThicknessSideDivider(0.9f);
        _MyController.UpdateCubeMap(_Flower);

        // update mats
        _MyController.UpdateWallTexture(_Wall6);
        _MyController.UpdateFloorTexture(_RiverBed1);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_NormalMap2);

        // update lights
        _MyController.UpdateLightIntensity(6.3f);
        _LightController.UpdateLightColor(0, new Color(0.783f, 0.576f, 0.905f, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
    }
}
