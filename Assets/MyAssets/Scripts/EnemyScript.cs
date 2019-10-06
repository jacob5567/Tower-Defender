// Jacob Faulk
// The script for an individual enemy. Controls movement, navigation, damage, health, attacks, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    Animator theAnimator; // The animator for the walking, idle, and attacking animations
    public GameObject player;
    public GameObject tower;
    NavMeshAgent nmAgent;
    private bool atTower; // True if the enemy has reached the tower
    public GameObject checkpoints; // The GameObjects containing the checkpoints for the enemy to navigate
    int health; // the current health of the enemy
    private int startingHealth; // the maximum health of the enemy
    private int currentCheckpointNum; // the index of the checkpoint that the enemy is navigating towards
    private Vector3 currentDestination; // the point towards which the enemy is navigating, be it a checkpoint or a tower
    private const float TOWER_ATTACK_DISTANCE = 3.5f; // The maximum distance between the enemy and the tower for the enemy to stop moving and start attacking
    private const int ATTACK_CYCLE_LENGTH = 140; // The number of frames in between each attack
    private const int DAMAGE_TO_TOWER = 10; // The amount of damage done to the tower per attack
    private const int SPEED = 2; // The speed of the enemy
    private int moneyDrop; // The amount of money given to the player after the enemy is defeated
    private int attackCycleLocation; // The number of frames until the enemy attacks again.
    private int index; // The index of this enemy in the list of all enemies
    private bool moneyGiven; // true if the player has already been given money for defeating this enemy

    // sets the initial values and initializes the walking animation
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

    // Monitors the health of the enemy, navigates between checkpoints and the tower, begins attack if close enough to tower
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

    // begins the walking animation
    public void startWalking()
    {
        theAnimator.SetFloat("Speed", 1);
    }

    // ends the walking animation
    public void stopWalking()
    {
        theAnimator.SetFloat("Speed", 0);
        nmAgent.speed = 0;
    }

    // gives the player money for defeating this enemy, goes into death animation, destroys this gameobject, removes health bar
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

    // reduces health by (damageAmount) and updates the health bar to match
    public void hit(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log(damageAmount);
        updateHealth();
    }

    // updates the health bar above the enemies head to reflect the current health value of the enemy
    private void updateHealth()
    {
        GameObject healthBar = transform.Find("HealthBar").Find("CurrentHealth").gameObject;
        healthBar.transform.localScale = new Vector3((float)health / startingHealth, 0.99f, 0.99f);
        float newPosition = 5 - (((float)health / startingHealth / 2) * 10);
        if (newPosition < 0)
            newPosition = 0f;
        healthBar.transform.localPosition = (new Vector3(newPosition, -0.0001f, 0));
    }

    /* For the next section, a bunch of setters for the EnemyGroupScript to use */
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

    public void setStartingHealth(int starting)
    {
        startingHealth = starting;
    }

    public void setMoneyDrop(int amount)
    {
        moneyDrop = amount;
    }
}
