using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsScript : MonoBehaviour
{

    int numCheckpoints;
    private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        int i = 0;
        foreach (Transform child in transform)
        {
            this.positions.Add(child.position);
            child.GetComponent<Collider>().enabled = false;
            child.GetComponent<Renderer>().enabled = false;
            i++;
        }
        numCheckpoints = i;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 getNextCheckpoint(int currentPosition)
    {
        return this.positions[currentPosition];
    }

    public int getNumCheckpoints()
    {
        return numCheckpoints;
    }
}
