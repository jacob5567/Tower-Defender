using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    Animator theAnimator;
    public GameObject player;
    NavMeshAgent nmAgent;

    // Use this for initialization
    void Start()
    {
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
        nmAgent.SetDestination(player.transform.position); //Tell enemy what to follow
    }

    public void startWalking()
    {
        Debug.Log("here");
        theAnimator.SetFloat("Speed", 1);
    }

    public void stopWalking()
    {
        Debug.Log("here2");
        theAnimator.SetFloat("Speed", 0);
    }
}
