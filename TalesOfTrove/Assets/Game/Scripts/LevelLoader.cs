using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [HideInInspector]
    public float alpha = 0;

    public Image image;
    public Animator transition;
    public float transitionTime = 1;
    private bool loadLevel;

    private void Start()
    {
        image.canvasRenderer.SetAlpha(0);
        loadLevel = false;
        image.gameObject.SetActive(false);
    }

    // When the start button is clicked, begin coroutine
    private void Update()
    {
        if (loadLevel)
        {
            StartCoroutine("FadeToBlack");
        }
    }

    public void LoadNextLevel()
    {
        loadLevel= true;
    }

    public IEnumerator FadeToBlack() //Alpha gradually goes to 0
    {
        if (alpha < 1)
        {
            image.gameObject.SetActive(true);
            alpha += .0005f;
            image.canvasRenderer.SetAlpha(alpha);
        }
        else if (alpha >= 1)
        {
            yield return new WaitForSeconds(.75f);
            SceneManager.LoadScene(1);

        }
    }

    public IEnumerator FadeToWhite() //Alpha slowly goes to 1
    {
        yield return new WaitForSeconds(1f);
        if (alpha > 0)
        {
            alpha -= .0004f;
            yield return new WaitForSeconds(.75f);
            image.canvasRenderer.SetAlpha(alpha);
        }
    }
}
