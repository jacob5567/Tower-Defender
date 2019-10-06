// Jacob Faulk
// This script controls the fourth type of turret because it has multiple targeting beams.
// I felt that this turret functions differently enough from the others to justify giving it its own script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiHitTurretScript : MonoBehaviour
{
    public int turretType; // The type of turret the script is bound to. This should always be 6.
    public GameObject player; // The FPSController
    public GameObject EnemyGroupCenter; // The GameObject that spawns and controls all the enemies.
    private int damage; // The amount of damage that the turret deals per beam.
    private int fireCooldownTime; // The rate at which the turret applies its damage. Damage is applied every fireCooldownTime frames.
    private float range; // The maximum distance an enemy can be for the turret to damage it.
    private int numBeams; // The number of separate damaging beams the turret has.
    private List<LaserEnemyClass> objects; // A list of the three damaging beams and their targeted enemies.
    public GameObject targetingLines; // The GameObject that holds all the targeting lines.
    private AudioSource firingSound; // The sound that the turret makes when firing.
    private bool audioToggle; // Used to make sure that the audio doesn't play every frame.

    // Sets the damage, cooldown and range values based on the type of turret it is. Initializes various other variables.
    // Begins to set up the laser-enemy connections.
    void Start()
    {
        switch (turretType)
        {
            case 6:
                damage = 1;
                fireCooldownTime = 2;
                range = 10f;
                numBeams = 3;
                break;
            default:
                damage = 1;
                fireCooldownTime = 30;
                range = 15f;
                numBeams = 5;
                break;
        }
        firingSound = GetComponent<AudioSource>();
        audioToggle = false;
        objects = new List<LaserEnemyClass>();
        for (int j = 0; j < numBeams; j++)
        {
            objects.Add(new LaserEnemyClass());
            objects[j].cooldown = 0;
        }
        int i = 0;
        foreach (Transform line in targetingLines.transform)
        {
            objects[i].line = line;
            i++;
        }
        this.StopFiring();
    }

    // Detects the nearest three enemies and fires on them if they are in range.
    // Calculates how to show each line connecting the tower and the enemy.
    void Update()
    {
        // decrease the cooldown for each laser individually
        foreach (LaserEnemyClass o in objects)
        {
            o.cooldown--;
        }

        // get references to all enemies and their distances from the turret
        float currentDistance;
        List<LaserEnemyClass> allEnemies = new List<LaserEnemyClass>();
        foreach (Transform child in EnemyGroupCenter.transform)
        {
            currentDistance = Vector3.Distance(child.transform.position, transform.position);
            allEnemies.Add(new LaserEnemyClass(child, currentDistance));
        }

        // sort the enemies by proximity to tower and assigns lasers to the closest (numBeams) in range
        allEnemies.Sort();
        for (int i = 0; i < numBeams; i++)
        {
            if (i < allEnemies.Count)
            {
                if (allEnemies[i].distance < range)
                {
                    objects[i].assignEnemy(allEnemies[i]);
                }
                else
                {
                    objects[i].removeEnemy();
                }
            }
        }

        // sets the correct position for each laser visually and damages each enemy
        bool firing = false;
        foreach (LaserEnemyClass obj in objects)
        {
            if (obj.hasEnemy())
            {
                firing = true;
                obj.line.GetComponent<LineRenderer>().SetPosition(1, transform.InverseTransformPoint(new Vector3(obj.enemy.transform.position.x, obj.enemy.transform.position.y - 0.5f, obj.enemy.transform.position.z)));
                if (obj.cooldown <= 0)
                {
                    obj.enemy.gameObject.GetComponent<EnemyScript>().hit(damage);
                    obj.cooldown = fireCooldownTime;
                }
            }
            else
            {
                obj.line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
            }
        }

        // plays the firing sound if at least one of the beams is currently damaging an enemy
        if (firing)
        {
            this.StartFiring();
        }
        else
        {
            this.StopFiring();
        }

    }

    // plays the firing sound
    public void StartFiring()
    {
        if (!audioToggle)
        {
            firingSound.Play(0);
            audioToggle = true;
        }
    }

    // stops the firing sound
    public void StopFiring()
    {
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
