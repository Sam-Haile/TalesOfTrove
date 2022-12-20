using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator e_animator;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        e_animator.SetTrigger("Hurt");

        if (currentHealth<= 0)
        {
            Die();
        }
    }

    void Die()
    {
        e_animator.SetBool("isDead", true);

        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
