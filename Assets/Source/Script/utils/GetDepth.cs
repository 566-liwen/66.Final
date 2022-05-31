using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDepth : MonoBehaviour
{
    public Transform _Fluid = null;
    public Transform _Floor = null;
    public Transform _Wall1 = null;
    public Transform _Wall2 = null;
    public Transform _Wall3 = null;
    public Transform _Wall4 = null;

    private Material _FluidSideMat = null;
    private Material _WallMat = null;

    public bool _1WallOff = true;
    public bool _2WallsOff = false;
    public bool _3WallsOff = false;
    public bool _4WallsOff = false;

    private int _CurrentSelected = 1;

    private Transform[] _WallsOff;
    private float _PrevDistance = 0;

    private static float _WallHight = 12.5f;
    private static float _FloorHeight;

    void Start()
    {
        _FloorHeight = _Floor.localPosition.y;
        _FluidSideMat = Resources.Load<Material>("SideFluidMat");
        _WallMat = Resources.Load<Material>("WallMat");

        float distance = Vector3.Distance(_Fluid.transform.position, _Floor.transform.position);
        _PrevDistance = distance;

        _WallsOff =  new Transform[] { _Wall1, _Wall2, _Wall3, _Wall4 };
        ChangeDepth(distance);
    }

    void Update()
    {
/*        if (NoneSelected())
        {
            _1WallOff = true;
            _CurrentSelected = 1;
        }*/


        
        /*if (_1WallOff)
        {
            _2WallsOff = false;
            _3WallsOff = false;
            _4WallsOff = false;
        }

        if (_2WallsOff)
        {
            _1WallOff = false;
            _3WallsOff = false;
            _4WallsOff = false;
        }

        if (_3WallsOff)
        {
            _1WallOff = false;
            _2WallsOff = false;
            _4WallsOff = false;
        }

        if (_4WallsOff)
        {
            _1WallOff = false;
            _2WallsOff = false;
            _3WallsOff = false;
        }*/

        float distance = Vector3.Distance(_Fluid.transform.position, _Floor.transform.position);
        bool IsDistanceChanged = false;
        if (distance != _PrevDistance)
        {
            IsDistanceChanged = true;
            _PrevDistance = distance;
        }
            /*            IsDistanceChanged = true;
                        _PrevDistance = distance;
                        // depth changed
                        if (_Fluid.transform.position.y < _Floor.transform.position.y)
                        {
                            return;
                            // todo: remove the wall
                        }*/
            //}
            //if (!IsWallChanged() && !IsDistanceChanged) return;

        if (_1WallOff)
        {
            WallfOffOrChangeHight(1);
        }
        else if (_2WallsOff)
        {
            WallfOffOrChangeHight(2);
        }
        else if(_3WallsOff)
        {
            WallfOffOrChangeHight(3);
        }
        else if(_4WallsOff)
        {
            WallfOffOrChangeHight(4);
        }
        WallsOn();
        DeselecteAll();
        //if (IsDistanceChanged)
        //{
            ChangeDepth(distance);
        //}
    }

    private void ChangeDepth(float distance)
    {
        for (int i = 0; i < _WallsOff.Length; i++)
        {
            Transform wall = _WallsOff[i];
            if (!IsWall(wall))
            {
                float hight = -(wall.localScale.y / 2 - distance) - _Floor.localPosition.y;
                if (_Floor.localPosition.y != _FloorHeight)
                {
                    Debug.Log(_Floor.localPosition.y - _FloorHeight);
                    hight += _Floor.localPosition.y - _FloorHeight;
                }
                Vector3 wallPosition = new Vector3(wall.localPosition.x, hight, wall.localPosition.z);
                wall.transform.localPosition = wallPosition;
            }
        }
    }

    private void DeselecteAll()
    {
        _1WallOff = false;
        _2WallsOff = false;
        _3WallsOff = false;
        _4WallsOff = false;
    }

    private bool NoneSelected()
    {
        foreach (bool i in _WallsOff)
        {
            if (i) return false;
        }
        return true;
    }

    private void WallsOn()
    {
        if (_1WallOff)
        {
            ChangeMatToWall(_Wall2);
            _Wall2.transform.localPosition = new Vector3(_Wall2.localPosition.x, _WallHight, _Wall2.localPosition.z);
            ChangeMatToWall(_Wall3);
            _Wall3.transform.localPosition = new Vector3(_Wall3.localPosition.x, _WallHight, _Wall3.localPosition.z);
            ChangeMatToWall(_Wall4);
            _Wall4.transform.localPosition = new Vector3(_Wall4.localPosition.x, _WallHight, _Wall4.localPosition.z);
        }
        if (_2WallsOff)
        {
            ChangeMatToWall(_Wall3);
            _Wall3.transform.localPosition = new Vector3(_Wall3.localPosition.x, _WallHight, _Wall3.localPosition.z);
            ChangeMatToWall(_Wall4);
            _Wall4.transform.localPosition = new Vector3(_Wall4.localPosition.x, _WallHight, _Wall4.localPosition.z);
        }
        if (_3WallsOff)
        {
            ChangeMatToWall(_Wall4);
            _Wall4.transform.localPosition = new Vector3(_Wall4.localPosition.x, _WallHight, _Wall4.localPosition.z);
        }
    }

    private bool IsWall (Transform _transf)
    {
        GameObject item = _transf.gameObject;
        Material mat = item.GetComponent<Renderer>().material;
        return mat.shader.name.Equals(_WallMat.shader.name);
    }

    private void ChangeMatToFluid(Transform _transf)
    {
        GameObject item = _transf.gameObject;
        item.GetComponent<Renderer>().material = _FluidSideMat;
    }

    private void ChangeMatToWall(Transform _transf)
    {
        GameObject item = _transf.gameObject;
        item.GetComponent<Renderer>().material = _WallMat;
    }

    private void WallfOffOrChangeHight(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform wall = _WallsOff[i];
            if (IsWall(wall))
            {
                ChangeMatToFluid(wall);
            }
        }
    }

    private bool IsWallChanged()
    {
        if (_WallsOff[0] != _1WallOff) return true;
        if (_WallsOff[1] != _2WallsOff) return true;
        if (_WallsOff[2] != _3WallsOff) return true;
        if (_WallsOff[3] != _4WallsOff) return true;
        return false;
    }
}
