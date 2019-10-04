using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private const float FIRING_SPEED = 1f;
    Animator theAnimator;
    public GameObject player;
    public GameObject EnemyGroupCenter;
    private GameObject currentTarget;
    private const int DAMAGE = 10;
    private const int FIRE_COOLDOWN_TIME = 30;
    private int cooldown;
    // private bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        // isFiring = false;
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
        if (closestChild != null)
        {
            Vector3 targetPosition = new Vector3(closestChild.position.x, this.transform.position.y, closestChild.position.z);
            transform.LookAt(targetPosition);
            if (cooldown < 0)
            {
                closestChild.gameObject.GetComponent<EnemyScript>().hit(DAMAGE);
                cooldown = FIRE_COOLDOWN_TIME;
            }
            this.StartFiring();
        }
        else
            this.StopFiring();
    }

    public void StartFiring()
    {
        // isFiring = true;
        theAnimator.SetFloat("FiringSpeed", FIRING_SPEED);
    }

    public void StopFiring()
    {
        // isFiring = false;
        theAnimator.SetFloat("FiringSpeed", 0f);
    }
}
