using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private const int TOWER_HEALTH = 1000;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = TOWER_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("You Lose!");
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void decreaseHealth(int points)
    {
        health -= points;
    }
}
