using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotScript : MonoBehaviour
{
    private const int COOLDOWN = 10;
    private AudioSource gunSound;
    private Animation gunShotAnim;
    private int cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        gunSound = GetComponent<AudioSource>();
        gunShotAnim = GetComponent<Animation>();


    }

    // Update is called once per frame
    void Update()
    {
        cooldown--;
        if (Input.GetButtonDown("Fire1") && cooldown <= 0)
        {
            gunSound.Play();
            gunShotAnim.Play("Gunshot");
            cooldown = COOLDOWN;
        }
    }
}
