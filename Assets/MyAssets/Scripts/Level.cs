using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int levelNum;
    public int numEnemies;
    public int enemyHealth;
    public int spawnRate;

    public Level(int levelNum)
    {
        this.levelNum = levelNum;
    }

    public Level(int levelNum, int numEnemies, int enemyHealth, int spawnRate)
    {
        this.levelNum = levelNum;
        this.numEnemies = numEnemies;
        this.enemyHealth = enemyHealth;
        this.spawnRate = spawnRate;
    }

    public int getLevelNum()
    {
        return this.levelNum;
    }
    public int getNumEnemies()
    {
        return this.numEnemies;
    }
    public int getEnemyHealth()
    {
        return this.enemyHealth;
    }
    public int getSpawnRate()
    {
        return this.spawnRate;
    }
}
