using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotScript : MonoBehaviour
{
    private const int COOLDOWN = 11;
    private const int DAMAGE = 10;
    private int range;
    private const int RANGE_SHORT = 15;
    private const int RANGE_LONG = 25;
    public float targetDistance;
    private AudioSource gunSound;
    private Animation gunShotAnim;
    public GameObject player;
    // private bool aimDownSights;
    public int cooldown;
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        range = RANGE_SHORT;
        // aimDownSights = false;
        layerMask = ~layerMask;
        cooldown = 0;
        gunSound = GetComponent<AudioSource>();
        gunShotAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
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
