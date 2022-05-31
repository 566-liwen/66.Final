using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _MuddyFluid = false;

    private void ApplyMuddyFluid()
    {
        // update transform
        SetFluidHeight(1.52f);
        SetFloorHeight(0);
        RotateFloorZ(0);

        // effects
        _MyController.UpdateShouldFluidTexture(true);
        _MyController.UpdateFluidColor(new Color(0.179f, 0.127f, 0.075f, 1));
        _MyController.UpdateFluidTexture(dirt_leaf_3);
        _MyController.UpdateThickFluidType(true);
        _MyController.UpdateThickFluidTransparency(0.2f);
        _MyController.UpdateAlphaN(798);
        _MyController.UpdateGamma(0.283f);
        _MyController.UpdateSideGamma(0.086f);
        _MyController.UpdateThicknessDivider(0.01f);
        _MyController.UpdateThicknessSideDivider(0.05f);
        _MyController.UpdateCubeMap(_Muddy);

        // update mats
        _MyController.UpdateWallTexture(_DirtWall2);
        _MyController.UpdateFloorTexture(_Dirt2);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_DirtNormalMap);

        // update lights
        _MyController.UpdateLightIntensity(3);
        _LightController.UpdateLightColor(0, new Color(0, 0, 0, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
    }
}
