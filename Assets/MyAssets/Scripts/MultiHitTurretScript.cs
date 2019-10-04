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
    // private int fireCooldownTime;
    private float range;
    private int numBeams;
    private List<int> cooldown;
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
                range = 10f;
                numBeams = 3;
                break;
            default:
                damage = 1;
                // fireCooldownTime = 30;
                range = 15f;
                numBeams = 5;
                break;
        }
        firingSound = GetComponent<AudioSource>();
        audioToggle = false;
        cooldown = new List<int>();
        for (int i = 0; i < numBeams; i++)
        {
            cooldown.Add(0);
        }
        theAnimator = GetComponent<Animator>();
        theAnimator.SetFloat("FiringSpeed", 0f);
        this.StopFiring();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 1, 0);
        for (int i = 0; i < numBeams; i++)
        {
            cooldown[i]--;
        }
        float currentDistance;
        List<Transform> closestChildren;
        List<float> distances;
        closestChildren = new List<Transform>();
        distances = new List<float>();
        for (int i = 0; i < numBeams; i++)
        {
            distances.Add(float.PositiveInfinity);
        }
        foreach (Transform child in EnemyGroupCenter.transform)
        {
            currentDistance = Vector3.Distance(child.transform.position, transform.position);
            distances.Sort();
            for (int i = 0; i < distances.Count; i++)
            {
                if (currentDistance < distances[i])
                {
                    distances.Insert(i, currentDistance);
                    distances.RemoveAt(distances.Count - 1);
                    closestChildren.Add(child);
                    float maximum = 0;
                    Transform farthestChild = null;
                    foreach (Transform c in closestChildren)
                    {
                        if (Vector3.Distance(c.transform.position, transform.position) < maximum)
                        {
                            farthestChild = c;
                        }
                    }
                    if (farthestChild != null)
                    {
                        closestChildren.Remove(farthestChild);
                    }
                }
            }
        }
        bool firing = false;
        foreach (Transform c in closestChildren)
        {
            if (Vector3.Distance(c.transform.position, transform.position) <= range)
            {
                firing = true;
                foreach (Transform line in targetingLines.transform)
                {
                    if (line.GetComponent<LineRenderer>().GetPosition(1).z == 0)
                    {
                        line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-0.0425f, 0, Vector3.Distance(c.transform.position, transform.position)));
                        break;
                    }
                }
                c.gameObject.GetComponent<EnemyScript>().hit(damage);
            }
        }
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
