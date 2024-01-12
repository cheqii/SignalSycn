using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectByCamera : MonoBehaviour
{
    private Camera cam;
    private Plane[] cameraFrustum;
    private Collider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InCameraViewDetect();
    }

    public GameObject InCameraViewDetect()
    {
        var bounds = col.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            Debug.Log(gameObject.name + " in camera");
            return gameObject;
        }
        else
        {
            Debug.Log(gameObject.name + " not in camera");
            return null;
        }
    }
}
