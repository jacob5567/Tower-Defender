// Jacob Faulk
// Represents a single level in the game. Specifies various attributes about the enemy spawns in that level.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int levelNum;
    public int numEnemies; // The number of enemies in this level
    public int enemyHealth; // The maximum health of each enemy
    public int spawnRate; // An enemy spawns every (spawnRate) frames

    // constructs a new level
    public Level(int levelNum)
    {
        this.levelNum = levelNum;
    }

    // constructs a new level
    public Level(int levelNum, int numEnemies, int enemyHealth, int spawnRate)
    {
        this.levelNum = levelNum;
        this.numEnemies = numEnemies;
        this.enemyHealth = enemyHealth;
        this.spawnRate = spawnRate;
    }

    /* GETTERS */
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
