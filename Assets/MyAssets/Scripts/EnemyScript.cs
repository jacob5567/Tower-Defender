using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    Animator theAnimator;
    public GameObject player;
    public GameObject tower;
    NavMeshAgent nmAgent;
    private bool atTower;
    public GameObject checkpoints;

    private int currentCheckpointNum;
    private Vector3 currentDestination;

    private const float TOWER_ATTACK_DISTANCE = 3.5f;

    // Use this for initialization
    void Start()
    {
        currentCheckpointNum = 0;
        theAnimator = GetComponent<Animator>(); //get handle to the Animator
        nmAgent = GetComponent<NavMeshAgent>(); //Tell enemy what mesh to use
        theAnimator.SetFloat("Speed", 0);
        theAnimator.SetFloat("Direction", 0.5f);
        nmAgent.speed = 2;
        this.startWalking();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentCheckpointNum);
        if (currentCheckpointNum < checkpoints.GetComponent<CheckpointsScript>().getNumCheckpoints())
        {
            currentDestination = checkpoints.GetComponent<CheckpointsScript>().getNextCheckpoint(currentCheckpointNum);
            nmAgent.SetDestination(currentDestination); //Tell enemy what to follow
            Vector3 distanceToCheckpoint = currentDestination - transform.position;
            if (distanceToCheckpoint.magnitude < 1.0f)
                currentCheckpointNum++;
        }
        else
        {
            nmAgent.SetDestination(tower.transform.position); //Tell enemy what to follow
        }
        Vector3 distanceToTower = tower.transform.position - transform.position;
        if (distanceToTower.magnitude < TOWER_ATTACK_DISTANCE && atTower == false)
        {
            atTower = true;
            nmAgent.speed = 0;
            theAnimator.SetFloat("Speed", 0);
            theAnimator.SetBool("Attacking", true);
        }
        if (distanceToTower.magnitude > TOWER_ATTACK_DISTANCE)
        {
            atTower = false;
            theAnimator.SetBool("Attacking", false);
        }
    }

    public void startWalking()
    {
        theAnimator.SetFloat("Speed", 1);
    }

    public void stopWalking()
    {
        theAnimator.SetFloat("Speed", 0);
    }
}
