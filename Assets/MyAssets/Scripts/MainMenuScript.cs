using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene(1);
    }

    public void quitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
