using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    

    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "enemy")
        { 
            Destroy(other.gameObject);
            
        }
    }




}
