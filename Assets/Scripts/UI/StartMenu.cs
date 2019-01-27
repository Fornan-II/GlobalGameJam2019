using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public GameObject panel;
    public GameObject player;
    public Button startGame;
    public Button quitGame;

    // Use this for initialization
    void Start()
    {
        //player.SetActive(false);
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
    }
	
	public void StartGame()
    {
        //player.SetActive(true);
        panel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
