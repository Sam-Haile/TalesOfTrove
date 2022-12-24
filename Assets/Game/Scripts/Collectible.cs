using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static Collectible instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player") && this.tag == "enemy")
        {
            collision.gameObject.GetComponent<HealthHeartBar>().TakeDamage(1);
        }
    }
}
