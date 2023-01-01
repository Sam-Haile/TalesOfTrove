using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomChaseController : MonoBehaviour
{
    public Mushroom[] enemyArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Mushroom bat in enemyArray)
            {
                bat.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Mushroom bat in enemyArray)
            {
                bat.chase = false;
            }
        }
    }
}
