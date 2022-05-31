using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    public bool _SlantedFloor = false;

    private void ApplySlantedFloor()
    {
        // update transform
        SetFluidHeight(6.2f);
        SetFloorHeight(1.9f);
        RotateFloorZ(-9.327f);

        // effects
        _MyController.UpdateShouldFluidTexture(false);
        _MyController.UpdateFluidColor(new Color(0.1405f, 0.368f, 0.355f, 1));
        _MyController.UpdateThickFluidType(false);
        _MyController.UpdateAlphaN(63);
        _MyController.UpdateGamma(0.2f);
        _MyController.UpdateSideGamma(0.06f);
        _MyController.UpdateThicknessDivider(9.1f);
        _MyController.UpdateThicknessSideDivider(11.7f);
        _MyController.UpdateCubeMap(_Regular);

        // update mats
        _MyController.UpdateWallTexture(_Wall2);
        _MyController.UpdateFloorTexture(_RiverBed1);
        _MyController.UpdateUseFloorNormalMap(true);
        _MyController.UpdateFloorNormalMap(_NormalMap2);

        // update lights
        _MyController.UpdateLightIntensity(1.8f);
        _LightController.UpdateLightColor(0, new Color(0.833f, 0.8999f, 0.9056f, 1));
        _LightController.RemoveAllOtherLightsAfterIndex(0);
    }
}
