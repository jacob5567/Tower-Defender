// Jacob Faulk
// The script that pauses the game, shows the pause menu, and controls all the buttons on both the pause menu and game over menus

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

#pragma warning disable 0168
public class PauseMenuScript : MonoBehaviour
{
    public static bool paused = false; // whether the game is paused or not
    public GameObject pauseMenuUI; // The UI for the pause menu
    public GameObject gameOverMenuUI; // The UI for the game over menu
    public GameObject player;
    public GameObject enemyGroupCenter; // The GameObject that spawns all the enemies
    public GameObject turrets; // The GameObject that contains all the turrets in the game
    public GameObject gun; // The player's gun
    public GameObject gameAudio; // the main backgound audio object
    public GameObject tower;
    public GameObject healthBar; // the tower's health bar

    // pauses and unpauses if the escape key is presses
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

    // resumes the game timer, re-enables all the disabled scripts and objects, deactivates the pause menu
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

    // pauses the game timer, disables all the relevant scripts and objects, shows the pause menu
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

    // loads the main menu
    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // quits the game
    public void quitGame()
    {
        Application.Quit();
    }

    // disables all the relevant scripts, disables the audio, stops time, and shows the game over screen
    public void gameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;

        gameAudio.GetComponent<AudioSource>().enabled = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<PlayerScript>().enabled = false;
        enemyGroupCenter.GetComponent<EnemyGroupScript>().enabled = false;
        gun.GetComponent<GunshotScript>().enabled = false;
        tower.GetComponent<TowerScript>().enabled = false;
        healthBar.SetActive(false);
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
}
