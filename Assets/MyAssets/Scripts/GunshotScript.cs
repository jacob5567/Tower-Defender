using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotScript : MonoBehaviour
{
    private const int COOLDOWN = 10;
    private const int DAMAGE = 20;
    private const int RANGE = 15;
    public float targetDistance;
    private AudioSource gunSound;
    private Animation gunShotAnim;
    public GameObject player;
    public int cooldown;
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~layerMask;
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
            // damage stuff
            RaycastHit shot;
            if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out shot, RANGE))
            {
                targetDistance = shot.distance;
                Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * shot.distance, Color.yellow);
                if (targetDistance < RANGE)
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
