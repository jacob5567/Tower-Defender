// Jacob Faulk
// This class represents a relationship between a laser of the multihit turret and an enemy on the board.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemyClass : IComparable
{
    public int cooldown; // The individual cooldown for a laser firing on an enemy.
    public Transform enemy; // The enemy being fired upon
    public float distance; // The distance between the enemy and the turret.
    public Transform line; // The laser/firing line

    // Constructs a new LaserEnemyClass instance
    // sets the distance to positive infinity for sorting purposes
    public LaserEnemyClass()
    {
        this.distance = float.PositiveInfinity;
    }

    // Constructs a new LaserEnemyClass instance
    // sets the enemy and the distance to the tower
    public LaserEnemyClass(Transform enemy, float distance)
    {
        this.enemy = enemy;
        this.distance = distance;
    }

    // used for sorting with other instances based on distance between tower and enemy
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        LaserEnemyClass other = obj as LaserEnemyClass;
        if (other != null)
            return this.distance.CompareTo(other.distance);
        else
            throw new ArgumentException("Object is not a LaserEnemyClass");
    }

    // assigns an enemy from a previously constructed instance to this one
    public void assignEnemy(LaserEnemyClass obj)
    {
        this.enemy = obj.enemy;
        this.distance = obj.distance;
    }

    // removes the enemy and resets the distance
    public void removeEnemy()
    {
        this.enemy = null;
        this.distance = float.PositiveInfinity;
    }

    // checks if this instance has an enemy assigned
    public bool hasEnemy()
    {
        return this.enemy != null;
    }
}
