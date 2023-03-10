using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public SpriteRenderer key;

    [SerializeField] private Image interactKey;
    


    // Start is called before the first frame update
    void Start()
    {
        interactKey.enabled = false;
        dialogBox.SetActive(false);
        key.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
        interactKey.enabled = true;
        dialogBox.SetActive(false);
        key.enabled = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false; 
        interactKey.enabled = false;
        dialogBox.SetActive(false);
        key.enabled = false;

    }
}
