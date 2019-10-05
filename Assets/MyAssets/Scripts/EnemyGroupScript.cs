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
    private const int SPAWN_RATE = 100; // a new enemy spawns every SPAWN_RATE frames
    private int spawnTimer;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private int currentIndex;

    public void generateEnemy()
    {
        spawnTimer = 0;
        GameObject enemy;
        //Create a new EnemyPrefab at enemyPos with the same orientation as Main Camera
        enemy = (GameObject)Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
        enemy.transform.parent = this.transform;
        enemy.GetComponent<EnemyScript>().SetPlayer(player);
        enemy.GetComponent<EnemyScript>().SetTower(tower);
        enemy.GetComponent<EnemyScript>().SetCheckpoints(checkpoints);
        enemy.GetComponent<EnemyScript>().SetIndex(currentIndex);
        currentIndex++;
        totalEnemies++;
        enemyList.Add(enemy); //Add the new enemy to the List
    }

    void Start()
    {
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
            spawnTimer = SPAWN_RATE;
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
