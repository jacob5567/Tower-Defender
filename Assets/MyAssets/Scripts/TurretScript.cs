using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private const float FIRING_SPEED = 1f;
    Animator theAnimator;
    private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        theAnimator = GetComponent<Animator>();
        theAnimator.SetFloat("FiringSpeed", 0f);
        this.StartFiring();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFiring()
    {
        isFiring = true;
        theAnimator.SetFloat("FiringSpeed", FIRING_SPEED);
        Debug.Log("start");
    }

    public void StopFiring()
    {
        isFiring = false;
        theAnimator.SetFloat("FiringSpeed", 0f);
        Debug.Log("stop");
    }
}
