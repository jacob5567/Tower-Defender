// Jacob Faulk
// The script that controls the gun. Repositions it, controls the range, and damages the enemies it shoots.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotScript : MonoBehaviour
{
    private const int COOLDOWN = 11; // The minimum amount of frames in between each shot of the gun
    private const int DAMAGE = 15; // the damage each shot does to an enemy
    private int range; // the maximum range of the gun
    private const int RANGE_SHORT = 15; // the range of the gun when hipfiring
    private const int RANGE_LONG = 25; // the range of the gun when aiming down the sights of the gun
    public float targetDistance; // the distance between the gun and the targer
    private AudioSource gunSound; // the sound the gun makes when fired
    private Animation gunShotAnim; // the kickback animation of the gun
    public GameObject player;
    public int cooldown; // the number of frames until the player is able to fire again
    int layerMask = 1 << 8; // specifies the layers that the gun is able to hit

    // sets various variables and initializes the audio and the animation
    void Start()
    {
        range = RANGE_SHORT;
        layerMask = ~layerMask;
        cooldown = 0;
        gunSound = GetComponent<AudioSource>();
        gunShotAnim = GetComponent<Animation>();
    }

    // aims if the player holds right click, fires if the player presses left click, damages an enemy if it is hit
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.Translate(new Vector3(-0.003f, 0.039f, -0.331f));
            range = RANGE_LONG;
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.Translate(new Vector3(0.003f, -0.039f, 0.331f));
            range = RANGE_SHORT;
        }

        cooldown--;
        if (Input.GetButtonDown("Fire1") && cooldown <= 0)
        {
            // damage stuff
            RaycastHit shot;
            if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out shot, range))
            {
                targetDistance = shot.distance;
                Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * shot.distance, Color.yellow);
                if (targetDistance < range)
                {
                    shot.transform.SendMessage("DeductHealth", DAMAGE, SendMessageOptions.DontRequireReceiver);
                }
            }

            // sound and animation stuff
            gunSound.Play();
            gunShotAnim.Play("Gunshot");
            cooldown = COOLDOWN;
        }
    }
}
