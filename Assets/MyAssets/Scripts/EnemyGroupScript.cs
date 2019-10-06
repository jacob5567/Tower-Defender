using UnityEngine;
using System.Collections;
using System.Collections.Generic; //use this for Lists
public class EnemyGroupScript : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int totalEnemies;
    public GameObject player;
    public GameObject tower;
    public GameObject checkpoints;
    private List<GameObject> enemyList;
    private int spawnRate; // a new enemy spawns every spawnRate frames
    private int currentStartingHealth;
    // private int spawnRateChangeRate = 2; // the spawn rate increases every SPAWN_RATE_CHANGE_RATE enemies
    private int levelChangeTimer;
    private int spawnTimer;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private LevelController levelController;
    private int currentIndex;
    private int moneyDrop;

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
        //Create a new EnemyPrefab at enemyPos with the same orientation as Main Camera
        enemy = (GameObject)Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
        enemy.transform.parent = this.transform;
        enemy.GetComponent<EnemyScript>().SetPlayer(player);
        enemy.GetComponent<EnemyScript>().SetTower(tower);
        enemy.GetComponent<EnemyScript>().SetCheckpoints(checkpoints);
        enemy.GetComponent<EnemyScript>().SetIndex(currentIndex);
        enemy.GetComponent<EnemyScript>().setStartingHealth(currentStartingHealth);
        enemy.GetComponent<EnemyScript>().setMoneyDrop(moneyDrop);
        currentIndex++;
        totalEnemies++;
        enemyList.Add(enemy); //Add the new enemy to the List
    }

    void Start()
    {
        moneyDrop = 50;
        levelController = new LevelController();
        spawnRate = levelController.getLevel(1).getSpawnRate();
        currentStartingHealth = levelController.getLevel(1).getEnemyHealth();
        levelChangeTimer = levelController.getLevel(1).getNumEnemies();
        spawnTimer = 0;

        totalEnemies = 0;
        currentIndex = 0;
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            generateEnemy();
            spawnTimer = spawnRate;
        }
    }

    public void stopAllEnemies()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            enemyList[i].GetComponent<EnemyScript>().stopWalking();
        }
    }
    public void makeAllEnemiesRun()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            enemyList[i].GetComponent<EnemyScript>().startWalking();
        }
    }

    public void killAnEnemy(int i)
    {
        GameObject deadEnemy = enemyList[i];
        Destroy(deadEnemy, 0.5f);
    }
}
