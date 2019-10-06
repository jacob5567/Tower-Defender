// Jacob Faulk
// This file controls all the checkpoints for the enemies in the game.
// The checkpoints are what the enemies follow to go down the path and eventually reach the tower.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsScript : MonoBehaviour
{

    int numCheckpoints; // the number of checkpoints on the map
    private List<Vector3> positions; // a list of all the positions of the checkpoints

    // Adds all the checkpoints to the list and updates the number of checkpoints
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

    // returns the location of the next checkpoint based on the index of the current checkpoint
    public Vector3 getNextCheckpoint(int currentPosition)
    {
        return this.positions[currentPosition];
    }

    // returns the total number of checkpoints
    public int getNumCheckpoints()
    {
        return numCheckpoints;
    }
}
