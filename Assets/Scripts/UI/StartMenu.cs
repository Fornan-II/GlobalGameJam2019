using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public GameObject panel;
    public Movement2D moveScript;
    public Button startGame;
    public Button quitGame;

    public bool started;

    // Use this for initialization
    void Start()
    {
        moveScript.enabled = false;
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        started = false;
    }
	
	public void StartGame()
    {
        moveScript.enabled = true;
        panel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        started = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
