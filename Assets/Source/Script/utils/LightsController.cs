using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{

    public List<Transform> Lights;
    public List<Color> _SpecularColors;

    //[Range(0.001f, 30)]
    //public float _SpecularIntensityOnFloor = 0.5f;

    //[Range(0.01f, 1)]
    //_PhongKsIntensity
    //public float _SpecularIntensity = 1;
    //_PhongKs
    //public Color _SpecularColor = Color.white;
    //[Range(0.5f, 1000)]
    //_PhongAlpha
    //public float _HighlightIntensity = 16;

    // max lights for now is 10
    Vector4[] positions = new Vector4[10];
    Vector4[] colors = new Vector4[10];
    void Start()
    {
    }

    void FixedUpdate()
    {
        positions = new Vector4[10];
        colors = new Vector4[10];
        for (int i = 0; i < Lights.Count; i++)
        {
            positions[i] = Lights[i].position;
        }
        for (int i = 0; i < _SpecularColors.Count; i++)
        {
            Color c = _SpecularColors[i];
            colors[i] = new Vector4(c.r, c.g, c.b, c.a);
        }
        Shader.SetGlobalVectorArray("_LightPositions", positions);
        Shader.SetGlobalVectorArray("_LightColors", colors);
    }

    public void UpdateLightColor(int index, Color c)
    {
        _SpecularColors[index] = c;
    }

    public void AddLightWithColor(Transform light, Color c)
    {
        Lights.Add(light);
        _SpecularColors.Add(c);
    }

    public void RemoveAllOtherLightsAfterIndex(int index)
    {
        int count = Lights.Count;
        if (count <= index + 1) return;
        Lights.RemoveRange(index + 1, count-(index+1));
        _SpecularColors.RemoveRange(index + 1, count-(index+1));
    }
}
