// Jacob Faulk
// This script represents a single turret pedestal.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public bool filled; // true if the pedestal has a turret in it already, false if not
    Collider col; // the pedestal's collider
    Camera cam; // the main camera
    Plane[] planes; // will hold the planes containing the main camera's view

    // sets the attributes to their default values
    void Start()
    {
        filled = false;
        cam = Camera.main;
        col = GetComponent<Collider>();
    }

    // returns whether the pedestal has a turret in it or not
    public bool isFilled()
    {
        return filled;
    }

    // fills the pedestal with a turret
    public void fill()
    {
        filled = true;
    }

    // checks whether the pedestal is in frame of the main camera. This is used for placing a turret in a pedestal
    public bool isInFrame()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, col.bounds);
    }
}
