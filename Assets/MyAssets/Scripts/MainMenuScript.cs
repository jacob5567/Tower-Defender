// Jacob Faulk
// Contains various methods corresponding to the various buttons on the main menu.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Starts the game
    public void playButton()
    {
        SceneManager.LoadScene(1);
    }

    // Quits the game
    public void quitButton()
    {
        Application.Quit();
    }

    // Sets the cursor visible and mobile when this scene is loaded
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
