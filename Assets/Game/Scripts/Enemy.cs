using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Animator e_animator;

    public int maxHealth = 100;
    public int currentHealth;
    public PrototypeHero hitBoxList;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool isHit = false;
    private Collider2D enemyCollider;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        isHit = true;
        currentHealth -= damage;
        StartCoroutine(WaitSeconds(.5f));
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            Die();
        }

    }

    void Die()
    {
        // Trigger aniamtion
        isDead = true;

        // Turn off collider
        enemyCollider = this.gameObject.GetComponent<Collider2D>();
        enemyCollider.enabled = false; 
        StartCoroutine(DestroyObject(1f));
        this.enabled = false;
    }

    IEnumerator DestroyObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }

    IEnumerator WaitSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isHit = false;
    }
}
