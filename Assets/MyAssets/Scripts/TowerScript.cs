// Jacob Faulk
// This script controls the functions of the main tower, mainly decreases the health.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    private const int TOWER_HEALTH = 500; // maximum health of the tower
    public int health; // current health of the tower
    public Image healthBar; // the visual representation of the tower's health in the UI

    // sets the current health to the maximum health
    void Start()
    {
        health = TOWER_HEALTH;
    }

    // triggers the game over state if the tower's health is less than zero
    void Update()
    {
        if (health <= 0)
        {
            GameObject.Find("Canvas").GetComponent<PauseMenuScript>().gameOver();
        }
    }

    // returns the current health value
    public int getHealth()
    {
        return health;
    }

    // decreases the current health by (points)
    public void decreaseHealth(int points)
    {
        health -= points;
        updateHealth();
    }

    // updates the visual indicator of the tower's health
    private void updateHealth()
    {
        healthBar.transform.localScale = new Vector3((float)health / TOWER_HEALTH, 1f, 1f);
    }
}
