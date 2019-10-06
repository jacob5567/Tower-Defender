// Jacob Faulk
// The script that spawns all the enemies with the specified attributes.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroupScript : MonoBehaviour
{
    public GameObject EnemyPrefab; // The prefab from which each enemy is instantiated
    public GameObject player;
    public GameObject tower;
    public GameObject checkpoints; // The GameObject containing each of the checkpoints that the enemy navigates to
    private List<GameObject> enemyList; // The list of all enemies
    private int spawnRate; // A new enemy spawns every spawnRate frames
    private int currentStartingHealth; // The max health the next enemy will spawn with
    private int levelChangeTimer; // The number of enemies left to spawn until the level progresses to the next
    private int spawnTimer; // The number of frames left until the next enemy spawns
    private Vector3 spawnPosition; // The position at which the enemy spawns
    private Quaternion spawnRotation; // The rotation at which the enemy spawns
    private LevelController levelController; // The class that controls the settings for each level (spawn rate, health, number of enemies, etc.)
    private int currentIndex; // The index of the current enemy in the list
    private int moneyDrop; // The amount of money given to the player upon defeat of the enemy

    // spawns a new enemy based on the level specifications and sets its attributes
    public void generateEnemy()
    {
        levelChangeTimer--;
        if (levelChangeTimer <= 0 && player.GetComponent<PlayerScript>().level < 20)
        {
            player.GetComponent<PlayerScript>().level++;
            spawnRate = levelController.getLevel(player.GetComponent<PlayerScript>().level).getSpawnRate();
            currentStartingHealth = levelController.getLevel(player.GetComponent<PlayerScript>().level).getEnemyHealth();
            levelChangeTimer = levelController.getLevel(player.GetComponent<PlayerScript>().level).getNumEnemies();
        }
        else if (levelChangeTimer <= 0 && player.GetComponent<PlayerScript>().level >= 20)
        {
            player.GetComponent<PlayerScript>().level++;
            spawnRate = 100;
            currentStartingHealth += 50;
            moneyDrop += 10;
        }


        GameObject enemy;
        //Create a new EnemyPrefab at enemyPos
        enemy = (GameObject)Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
        enemy.transform.parent = this.transform;
        enemy.GetComponent<EnemyScript>().SetPlayer(player);
        enemy.GetComponent<EnemyScript>().SetTower(tower);
        enemy.GetComponent<EnemyScript>().SetCheckpoints(checkpoints);
        enemy.GetComponent<EnemyScript>().SetIndex(currentIndex);
        enemy.GetComponent<EnemyScript>().setStartingHealth(currentStartingHealth);
        enemy.GetComponent<EnemyScript>().setMoneyDrop(moneyDrop);
        currentIndex++;
        enemyList.Add(enemy); //Add the new enemy to the List
    }

    // initializes the level settings for level 1 and sets the position and rotation for enemy spawns
    void Start()
    {
        moneyDrop = 50;
        levelController = new LevelController();
        spawnRate = levelController.getLevel(1).getSpawnRate();
        currentStartingHealth = levelController.getLevel(1).getEnemyHealth();
        levelChangeTimer = levelController.getLevel(1).getNumEnemies();
        spawnTimer = 0;

        currentIndex = 0;
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        enemyList = new List<GameObject>();
    }

    // spawns a new enemy every (spawnRate) frames
    void Update()
    {
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            generateEnemy();
            spawnTimer = spawnRate;
        }
    }

    // destroys the enemy at index i
    public void killAnEnemy(int i)
    {
        GameObject deadEnemy = enemyList[i];
        Destroy(deadEnemy, 0.5f);
    }
}
