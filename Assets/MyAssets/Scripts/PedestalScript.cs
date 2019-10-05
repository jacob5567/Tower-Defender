using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public bool filled;
    // Start is called before the first frame update
    void Start()
    {
        filled = false;
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
}
