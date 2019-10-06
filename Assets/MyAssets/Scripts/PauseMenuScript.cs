using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenuScript : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenuUI;
    public GameObject player;
    public GameObject enemyGroupCenter;
    public GameObject turrets;
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

#pragma warning disable 0168
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<PlayerScript>().enabled = true;
        enemyGroupCenter.GetComponent<EnemyGroupScript>().enabled = true;
        gun.GetComponent<GunshotScript>().enabled = true;
        foreach (Transform turret in turrets.transform)
        {
            try
            {
                turret.GetComponent<TurretScript>().enabled = true;
            }
            catch (NullReferenceException e)
            {
                turret.GetComponent<MultiHitTurretScript>().enabled = true;
            }
            turret.GetComponent<AudioSource>().enabled = true;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        paused = false;
    }

#pragma warning disable 0168
    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<PlayerScript>().enabled = false;
        enemyGroupCenter.GetComponent<EnemyGroupScript>().enabled = false;
        gun.GetComponent<GunshotScript>().enabled = false;
        foreach (Transform turret in turrets.transform)
        {
            try
            {
                turret.GetComponent<TurretScript>().enabled = false;
            }
            catch (NullReferenceException e)
            {
                turret.GetComponent<MultiHitTurretScript>().enabled = false;
            }
            turret.GetComponent<AudioSource>().enabled = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        paused = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
