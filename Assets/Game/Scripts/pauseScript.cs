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
    public GameObject pauseBackgroundImage;
    public GameObject pauseBackgroundImage2;
    public Button pauseButton;
    public Button quitButton;
    public Button respawn;

    // Start is called before the first frame update
    void Start()
    {
        pauseBackgroundImage.SetActive(false);
        pauseBackgroundImage2.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        respawn.gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void Unpause()
    {
        pauseState.paused = false;
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    void PauseGame()
    {
        if (pauseState.paused)
        {
            pauseBackgroundImage.SetActive(true);
            pauseBackgroundImage2.SetActive(true);
            pauseButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            respawn.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!pauseState.paused)
        {
            pauseBackgroundImage.SetActive(false);
            pauseBackgroundImage2.SetActive(false);
            pauseButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            respawn.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void moveUI(float distance)
    {
        if (pauseBackgroundImage2.activeSelf)
        {
            iTween.MoveTo(pauseBackgroundImage2,
            iTween.Hash(
            "x", distance,
            "easeType", "easeInQuad",
            "loopType", "none",
            "delay", 0,
            "time", .5f,
            "ignoretimescale", true));
        }
    }
}
