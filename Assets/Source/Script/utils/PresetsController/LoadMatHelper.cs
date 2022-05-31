using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FluidFloorHeightController : MonoBehaviour
{
    private Texture2D _DirtWall2;
    private Texture2D _DirtWall3;
    private Texture2D _Dirt;
    private Texture2D _Dirt2;
    private Texture2D _DirtWall;
    private Texture2D _RiverBed1;
    private Texture2D _RiverBed2;
    private Texture2D _GreenWall;
    private Texture2D _Wall1;
    private Texture2D _Wall2;
    private Texture2D _Wall3;
    private Texture2D _Wall4;
    private Texture2D _Wall5;
    private Texture2D _Wall6;
    private Texture2D _Wall7;
    private Texture2D _Wall8;
    private Texture2D dirt_leaf_3;

    // normal maps
    private Texture2D _DirtNormalMap;
    private Texture2D _NormalMap;
    private Texture2D _NormalMap2;
    private Texture2D _SwimmingNormalMap;

    // cube maps
    private Cubemap _Regular;
    private Cubemap _Flower;
    private Cubemap _Muddy;
    private Cubemap _Swimming;

    void Awake()
    {
        _DirtWall2 = Resources.Load<Texture2D>("Images/dirt-wall2");
        _DirtWall3 = Resources.Load<Texture2D>("Images/dirt-wall3");
        _Dirt = Resources.Load<Texture2D>("Images/dirt");
        _Dirt2 = Resources.Load<Texture2D>("Images/dirt2");
        _DirtWall = Resources.Load<Texture2D>("Images/dirt-wall");
        _RiverBed1 = Resources.Load<Texture2D>("Images/riverbed1");
        _RiverBed2 = Resources.Load<Texture2D>("Images/riverbed2");
        _GreenWall = Resources.Load<Texture2D>("Images/green-wall");
        _Wall1 = Resources.Load<Texture2D>("Images/wall1");
        _Wall2 = Resources.Load<Texture2D>("Images/wall2");
        _Wall3 = Resources.Load<Texture2D>("Images/wall3");
        _Wall4 = Resources.Load<Texture2D>("Images/wall4");
        _Wall5 = Resources.Load<Texture2D>("Images/wall5");
        _Wall6 = Resources.Load<Texture2D>("Images/wall6");
        _Wall7 = Resources.Load<Texture2D>("Images/wall7");
        _Wall8 = Resources.Load<Texture2D>("Images/wall8");
        dirt_leaf_3 = Resources.Load<Texture2D>("Images/dirt_leaf_3");
        // normal maps
        _DirtNormalMap = Resources.Load<Texture2D>("Images/normal_maps/DirtNormalMap");
        _NormalMap = Resources.Load<Texture2D>("Images/normal_maps/NormalMap");
        _NormalMap2 = Resources.Load<Texture2D>("Images/normal_maps/NormalMap2");
        _SwimmingNormalMap = Resources.Load<Texture2D>("Images/normal_maps/SwimmingNormalMap");
        // cube maps
        _Regular = Resources.Load<Cubemap>("cubemaps/regular");
        _Flower = Resources.Load<Cubemap>("cubemaps/flower");
        _Muddy = Resources.Load<Cubemap>("cubemaps/muddy");
        _Swimming = Resources.Load<Cubemap>("cubemaps/swimming_pool");
    }
}
