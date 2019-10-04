using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemyClass : IComparable
{
    public int cooldown;
    public Transform enemy;
    public float distance;
    public Transform line;

    public LaserEnemyClass()
    {
        this.distance = float.PositiveInfinity;
    }

    public LaserEnemyClass(Transform enemy, float distance)
    {
        this.enemy = enemy;
        this.distance = distance;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        LaserEnemyClass other = obj as LaserEnemyClass;
        if (other != null)
            return this.distance.CompareTo(other.distance);
        else
            throw new ArgumentException("Object is not a LaserEnemyClass");
    }

    public void assignEnemy(LaserEnemyClass obj)
    {
        this.enemy = obj.enemy;
        this.distance = obj.distance;
    }

    public void removeEnemy()
    {
        this.enemy = null;
        this.distance = float.PositiveInfinity;
    }

    public bool hasEnemy()
    {
        return this.enemy != null;
    }
}
