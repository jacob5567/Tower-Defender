using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public bool filled;
    Collider col;
    Camera cam;
    Plane[] planes;

    // Start is called before the first frame update
    void Start()
    {
        filled = false;
        cam = Camera.main;
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isFilled()
    {
        return filled;
    }

    public void fill()
    {
        filled = true;
    }

    public bool isInFrame()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }
}
