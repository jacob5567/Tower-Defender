// Jacob Faulk
// This script controls the first three types of turrets in the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private const float FIRING_SPEED = 1f; // The speed at which the firing animation is played for the first turret.
    public int turretType; // The type of turret the script is bound to. 1 for basic, 3 for flamethrower, 4 for laser.
    Animator theAnimator; // The animator for the first turret.
    public GameObject player; // The FPSController
    public GameObject EnemyGroupCenter; // The GameObject that spawns and controls all the enemies.
    private GameObject currentTarget; // The enemy that the turret is currently focused on.
    private int damage; // The amount of damage that the turret deals.
    private int fireCooldownTime; // The rate at which the turret applies its damage. Damage is applied every fireCooldownTime frames.
    private float range; // The maximum distance an enemy can be for the turret to damage it.
    private int cooldown; // The counter variable that checks if the turret is ready to fire again.
    public GameObject targetingLine; // The line that goes from the turret to the enemy in turret types 2, 3, and 4.
    private AudioSource firingSound; // The sound that the turret makes when firing.
    private bool audioToggle; // Used to make sure that the audio doesn't play every frame.

    // Sets the damage, cooldown and range values based on the type of turret it is. Initializes various other variables.
    void Start()
    {
        switch (turretType)
        {
            case 1:
                damage = 20;
                fireCooldownTime = 30;
                range = 15f;
                break;
            case 3:
                damage = 4;
                fireCooldownTime = 2;
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
        if (turretType == 1) // The Animator is only set if the turret is of type 1.
        {
            theAnimator = GetComponent<Animator>();
            theAnimator.SetFloat("FiringSpeed", 0f);
        }
        this.StopFiring();
    }

    // Detects the nearest enemy, checks if it is in range, and damages it, showing the targeting line if necessary. Rotates the turret accordinly.
    void Update()
    {
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
            targetingLine.GetComponent<LineRenderer>().SetPosition(1, transform.InverseTransformPoint(closestChild.position));
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

    // Begins the firing animation (if applicable) and the firing sound.
    public void StartFiring()
    {
        if (turretType == 1)
        {
            theAnimator.SetFloat("FiringSpeed", FIRING_SPEED);
        }
        if (!audioToggle)
        {
            firingSound.Play(0);
            audioToggle = true;
        }
    }

    // Stops the firing animation (if applicable) and the firing sound.
    public void StopFiring()
    {
        if (turretType == 1)
        {
            theAnimator.SetFloat("FiringSpeed", 0f);
        }
        firingSound.Pause();
        audioToggle = false;
    }

    // sets the player GameObject to the specified GameObject
    public void SetPlayer(GameObject toSet)
    {
        player = toSet;
    }

    // sets the EnemyGroupCenter GameObject to the specified GameObject
    public void SetEnemyGroupCenter(GameObject toSet)
    {
        EnemyGroupCenter = toSet;
    }
}
