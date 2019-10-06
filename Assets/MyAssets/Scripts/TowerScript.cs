using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    private const int TOWER_HEALTH = 500;
    public int health;
    public Image healthBar;

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
            GameObject.Find("Canvas").GetComponent<PauseMenuScript>().gameOver();
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void decreaseHealth(int points)
    {
        health -= points;
        updateHealth();
    }

    private void updateHealth()
    {
        healthBar.transform.localScale = new Vector3((float)health / TOWER_HEALTH, 1f, 1f);
    }
}
