using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFacing : MonoBehaviour
{
    public Camera _camera;
    //public bool show = false;
    private Material _WallMat;
    private Material _FluidMat;
    private bool isWall = true;
    public int min = 0;
    public int max = 90;
    void Awake()
    {
        //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        _WallMat = Resources.Load<Material>("WallMat");
        _FluidMat = Resources.Load<Material>("WallMat2");
        // load wall material if needed at this point
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float angle = Vector3.Angle(this.gameObject.transform.TransformDirection(Vector3.up), _camera.transform.TransformDirection(Vector3.forward));
        float angle = Vector3.Angle(transform.up, _camera.transform.forward);
        //float angle = Mathf.Acos(Vector3.Dot(-this.transform.right, _camera.transform.forward) / (this.transform.right.magnitude * _camera.transform.forward.magnitude));
        Debug.Log(angle);
        
        
        if (angle > 0 && angle < 89)
        {
            if (!isWall) return;
            // show, switch shader
            Renderer meshRenderer = GetComponent<Renderer>();
            // Get the current material applied on the GameObject
            meshRenderer.material = _FluidMat;
        } else
        {
            if (isWall) return;
            // show, switch shader
            Renderer meshRenderer = GetComponent<Renderer>();
            // Get the current material applied on the GameObject
            meshRenderer.material = _WallMat;
        }
        isWall = !isWall;
        
    }
}
