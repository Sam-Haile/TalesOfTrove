using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool chestInRange;
    public SpriteRenderer key;
    public Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        key.enabled = false;
        key.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && chestInRange)
        {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        chestInRange = true;
        key.enabled = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        chestInRange = false;
        key.enabled = false;

    }
}
