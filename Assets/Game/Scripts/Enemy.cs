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
    public bool IsDead;
    private Collider2D enemyCollider;
    void Start()
    {
        currentHealth = maxHealth;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
 
    }

    private void Update()
    {
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger aniamtion
        IsDead = true;

        // Turn off collider
        enemyCollider = this.gameObject.GetComponent<Collider2D>();
        enemyCollider.enabled = false; 
        StartCoroutine(WaitTime(1f));
        this.enabled = false;
    }

    IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);

    }
}
