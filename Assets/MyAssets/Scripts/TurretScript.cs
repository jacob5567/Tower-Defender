using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private const float FIRING_SPEED = 1f;
    public int towerType;
    Animator theAnimator;
    public GameObject player;
    public GameObject EnemyGroupCenter;
    private GameObject currentTarget;
    private int damage;
    private int fireCooldownTime;
    private float range;
    private int cooldown;
    public GameObject targetingLine;
    private AudioSource firingSound;
    private bool audioToggle;
    // private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        // isFiring = false;
        switch (towerType)
        {
            case 1:
                damage = 10;
                fireCooldownTime = 30;
                range = 15f;
                break;
            case 3:
                damage = 2;
                fireCooldownTime = 3;
                range = 10f;
                break;
            case 4:
                damage = 3;
                fireCooldownTime = 3;
                range = 20f;
                break;
            default:
                damage = 10;
                fireCooldownTime = 30;
                range = 15f;
                break;
        }
        firingSound = GetComponent<AudioSource>();
        audioToggle = false;
        cooldown = 0;
        theAnimator = GetComponent<Animator>();
        theAnimator.SetFloat("FiringSpeed", 0f);
        this.StopFiring();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 1, 0);
        cooldown--;
        float currentDistance;
        float minimumDistance = float.PositiveInfinity;
        Transform closestChild = null;
        foreach (Transform child in EnemyGroupCenter.transform)
        {
            currentDistance = Vector3.Distance(child.transform.position, transform.position);
            if (currentDistance < minimumDistance)
            {
                minimumDistance = currentDistance;
                closestChild = child;
            }
        }
        if (closestChild != null && minimumDistance <= range)
        {
            Vector3 targetPosition = new Vector3(closestChild.position.x, this.transform.position.y, closestChild.position.z);
            targetingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, minimumDistance));
            transform.LookAt(targetPosition);
            if (cooldown < 0)
            {
                closestChild.gameObject.GetComponent<EnemyScript>().hit(damage);
                cooldown = fireCooldownTime;
            }
            this.StartFiring();
        }
        else
        {
            targetingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, 0));
            this.StopFiring();
        }
    }

    public void StartFiring()
    {
        // isFiring = true;
        theAnimator.SetFloat("FiringSpeed", FIRING_SPEED);
        if (!audioToggle)
        {
            firingSound.Play(0);
            audioToggle = true;
        }
    }

    public void StopFiring()
    {
        // isFiring = false;
        theAnimator.SetFloat("FiringSpeed", 0f);
        firingSound.Pause();
        audioToggle = false;
    }
}
