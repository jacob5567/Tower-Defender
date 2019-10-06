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
    int health;
    private int startingHealth;
    private int currentCheckpointNum;
    private Vector3 currentDestination;
    private const float TOWER_ATTACK_DISTANCE = 3.5f;
    private const int ATTACK_CYCLE_LENGTH = 140;
    private const int DAMAGE_TO_TOWER = 10;
    private const int SPEED = 2;
    private int moneyDrop;
    private int attackCycleLocation;
    private int index;
    private bool moneyGiven;

    // Use this for initialization
    void Start()
    {
        moneyGiven = false;
        health = startingHealth;
        currentCheckpointNum = 0;
        attackCycleLocation = -1;
        theAnimator = GetComponent<Animator>(); //get handle to the Animator
        nmAgent = GetComponent<NavMeshAgent>(); //Tell enemy what mesh to use
        theAnimator.SetFloat("Speed", 0);
        theAnimator.SetFloat("Direction", 0.5f);
        this.gameObject.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
        nmAgent.speed = SPEED;
        this.startWalking();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            die();
        }
        if (currentCheckpointNum < checkpoints.GetComponent<CheckpointsScript>().getNumCheckpoints())
        {
            currentDestination = checkpoints.GetComponent<CheckpointsScript>().getNextCheckpoint(currentCheckpointNum);
            nmAgent.SetDestination(currentDestination); // enemy follows next checkpoint
            Vector3 distanceToCheckpoint = currentDestination - transform.position;
            if (distanceToCheckpoint.magnitude < 1.0f)
                currentCheckpointNum++;
        }
        else
        {
            nmAgent.SetDestination(tower.transform.position); // enemy now follows the tower
        }
        Vector3 distanceToTower = tower.transform.position - transform.position;
        if (distanceToTower.magnitude < TOWER_ATTACK_DISTANCE && atTower == false)
        {
            atTower = true;
            nmAgent.speed = 0;
            theAnimator.SetFloat("Speed", 0);
            theAnimator.SetBool("Attacking", true);
            attackCycleLocation = ATTACK_CYCLE_LENGTH;
        }
        else if (distanceToTower.magnitude < TOWER_ATTACK_DISTANCE && atTower == true)
        {
            if (attackCycleLocation <= 0)
            {
                attackCycleLocation = ATTACK_CYCLE_LENGTH;
                tower.GetComponent<TowerScript>().decreaseHealth(DAMAGE_TO_TOWER);
            }
            attackCycleLocation--;
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
        nmAgent.speed = 0;
    }

    public void die()
    {
        if (!moneyGiven)
        {
            player.GetComponent<PlayerScript>().money += moneyDrop;
            moneyGiven = true;
        }
        nmAgent.speed = 0;
        theAnimator.SetBool("Dead", true);
        GameObject.Find("EnemyGroupCenter").GetComponent<EnemyGroupScript>().killAnEnemy(this.index);
        GameObject healthBar = transform.Find("HealthBar").gameObject;
        GameObject healthBarFiller = transform.Find("HealthBar").Find("CurrentHealth").gameObject;
        healthBarFiller.GetComponent<Renderer>().enabled = false;
        healthBar.GetComponent<Renderer>().enabled = false;
    }

    public void hit(int DamageAmount)
    {
        health -= DamageAmount;
        updateHealth();
    }

    public void SetPlayer(GameObject toSet)
    {
        player = toSet;
    }

    public void SetTower(GameObject toSet)
    {
        tower = toSet;
    }

    public void SetCheckpoints(GameObject toSet)
    {
        checkpoints = toSet;
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    private void updateHealth()
    {
        GameObject healthBar = transform.Find("HealthBar").Find("CurrentHealth").gameObject;
        healthBar.transform.localScale = new Vector3((float)health / startingHealth, 0.99f, 0.99f);
        float newPosition = 5 - (((float)health / startingHealth / 2) * 10);
        if (newPosition < 0)
            newPosition = 0f;
        healthBar.transform.localPosition = (new Vector3(newPosition, -0.0001f, 0));
    }

    public void setStartingHealth(int starting)
    {
        startingHealth = starting;
    }

    public void setMoneyDrop(int amount)
    {
        moneyDrop = amount;
    }

}
