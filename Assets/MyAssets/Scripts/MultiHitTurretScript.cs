using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiHitTurretScript : MonoBehaviour
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
    private int numBeams;
    // private List<int> cooldown;
    private List<LaserEnemyClass> objects;
    public GameObject targetingLines;
    private AudioSource firingSound;
    private bool audioToggle;
    // private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        // isFiring = false;
        switch (towerType)
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
        // theAnimator = GetComponent<Animator>();
        // theAnimator.SetFloat("FiringSpeed", 0f);
        this.StopFiring();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 1, 0);
        foreach (LaserEnemyClass o in objects)
        {
            o.cooldown--;
        }
        float currentDistance;
        List<LaserEnemyClass> allEnemies = new List<LaserEnemyClass>();
        foreach (Transform child in EnemyGroupCenter.transform)
        {
            currentDistance = Vector3.Distance(child.transform.position, transform.position);
            allEnemies.Add(new LaserEnemyClass(child, currentDistance));
        }
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
        // for (int i = 0; i < distances.Count; i++)
        // {
        //     if (currentDistance < distances[i])
        //     {
        //         distances.Insert(i, currentDistance);
        //         distances.RemoveAt(distances.Count - 1);
        //         closestChildren.Add(child);
        //         float maximum = 0;
        //         Transform farthestChild = null;
        //         foreach (Transform c in closestChildren)
        //         {
        //             if (Vector3.Distance(c.transform.position, transform.position) < maximum)
        //             {
        //                 farthestChild = c;
        //             }
        //         }
        //         if (farthestChild != null)
        //         {
        //             closestChildren.Remove(farthestChild);
        //         }
        //     }
        // }
        bool firing = false;
        foreach (LaserEnemyClass obj in objects)
        {
            if (obj.hasEnemy())
            {
                firing = true;
                obj.line.GetComponent<LineRenderer>().SetPosition(1, transform.InverseTransformPoint(new Vector3(obj.enemy.transform.position.x, obj.enemy.transform.position.y - 0.5f, obj.enemy.transform.position.z)));// new Vector3(obj.enemy.transform.position.x, 0, Vector3.Distance(obj.enemy.transform.position, transform.position)));
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
        // foreach (Transform c in closestChildren)
        // {
        //     if (Vector3.Distance(c.transform.position, transform.position) <= range)
        //     {
        //         firing = true;
        //         foreach (Transform line in targetingLines.transform)
        //         {
        //             if (line.GetComponent<LineRenderer>().GetPosition(1).z == 0)
        //             {
        //                 line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, Vector3.Distance(c.transform.position, transform.position)));
        //                 break;
        //             }
        //         }
        //         c.gameObject.GetComponent<EnemyScript>().hit(damage);
        //     }
        // }
        if (firing)
        {
            this.StartFiring();
        }
        else
        {
            this.StopFiring();
        }
        // if (closestChild != null && minimumDistance <= range)
        // {
        //     Vector3 targetPosition = new Vector3(closestChild.position.x, this.transform.position.y, closestChild.position.z);
        //     targetingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, minimumDistance));
        //     transform.LookAt(targetPosition);
        //     if (cooldown < 0)
        //     {
        //         closestChild.gameObject.GetComponent<EnemyScript>().hit(damage);
        //         cooldown = fireCooldownTime;
        //     }
        //     this.StartFiring();
        // }
        // else
        // {
        //     targetingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, 0));
        //     this.StopFiring();
        // }
    }

    public void StartFiring()
    {
        // isFiring = true;
        // theAnimator.SetFloat("FiringSpeed", FIRING_SPEED);
        if (!audioToggle)
        {
            firingSound.Play(0);
            audioToggle = true;
        }
    }

    public void StopFiring()
    {
        // isFiring = false;
        // theAnimator.SetFloat("FiringSpeed", 0f);
        firingSound.Pause();
        audioToggle = false;
    }
}
