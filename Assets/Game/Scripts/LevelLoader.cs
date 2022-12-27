using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;

public class LevelLoader : MonoBehaviour
{

    [HideInInspector]
    public float alpha = 0;

    public Animator transition;
    public Image image;
    public float transitionTime = 1;
    private bool loadLevel;
    private void Start()
    {
        if (image != null)
        {
            image.canvasRenderer.SetAlpha(0);
            image.gameObject.SetActive(false);
        }

        loadLevel = false;
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
        loadLevel = true;
    }

    public IEnumerator FadeToBlack() //Alpha gradually goes to 0
    {
        if (alpha < 1)
        {
            image.gameObject.SetActive(true);
            alpha += .05f;
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
            alpha -= .04f;
            yield return new WaitForSeconds(.75f);
            image.canvasRenderer.SetAlpha(alpha);
        }
    }
}
