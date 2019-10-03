using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeductHealth(int DamageAmount)
    {
        transform.parent.gameObject.GetComponent<EnemyScript>().hit(DamageAmount);
    }
}
