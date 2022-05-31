using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDepth : MonoBehaviour
{
    public Transform _Fluid = null;
    public Transform _Floor = null;
    private static float _FloorHeight;

    private void Start()
    {
        _FloorHeight = _Floor.localPosition.y;
    }
    void Update()
    {
        float distance = Vector3.Distance(_Fluid.transform.position, _Floor.transform.position);
        float hight = -(this.transform.localScale.y / 2 - distance) - _Floor.localPosition.y;
        //Debug.Log("1"+hight);
        if (_Floor.localPosition.y != _FloorHeight)
        {
            //Debug.Log(_Floor.localPosition.y - _FloorHeight);
            hight += (_Floor.localPosition.y - _FloorHeight) * 2;
        }
        //Debug.Log("2" + hight);
        Vector3 wallPosition = new Vector3(this.transform.localPosition.x, hight, this.transform.localPosition.z);
        this.transform.localPosition = wallPosition;
    }
}
