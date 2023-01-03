using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static iTween;
using static Move;

public class pauseScript : MonoBehaviour
{

    public PrototypeHero pauseState;
    public GameObject pauseMenu;
    public GameObject controlMenu;
    private bool controls = false;

    // Start is called before the first frame update
    void Start()
    {
        controlMenu.SetActive(false);
        TurnOff();
    }


    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    // If ESC is pressed again when the game is paused
    public void Unpause()
    {
        pauseState.paused = false;
    }

    // Kill player and return to spawn
    public void Respawn()
    {
        pauseState.health = 0;
        pauseState.paused = false;

        Debug.Log("Respawn");
    }

    // Return to title screen
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    // Handle pausing and control menu
    void PauseGame()
    {
        if (pauseState.paused)
        {
            TurnOn();
            if (controls)
            {
                controlMenu.SetActive(true);
            }
            else if (!controls)
            {
                controlMenu.SetActive(false);
                TurnOn();
            }
            Time.timeScale = 0;
            //if controls button is pressed
        }
        else if (!pauseState.paused)
        {
            TurnOff();

            Time.timeScale = 1;
        }
    }


    // Turn screen elements on
    private void TurnOn()
    {
        pauseMenu.SetActive(true);
    }

    // Turn screen elements off
    private void TurnOff()
    {
        pauseMenu.SetActive(false);
    }

    // turn controls screen on
    public void Controls()
    {
        Debug.Log("sc");
        controls = true;
    }

    // return to pause screen
    public void Back()
    {
        controls = false;
    }
}
