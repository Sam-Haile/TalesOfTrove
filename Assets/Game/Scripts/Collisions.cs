using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public Animator animator;
    private AudioManager_PrototypeHero audioManager;
    public PrototypeHero player;
    public SpriteRenderer keySprite;
    public Enemy enemy;

    public int scoreAmount = 0;
    public float damageAmount;
    public bool opened = false;
    private bool chestInRange;
    private Enemy currentEnemy;
    private void Start()
    {
        audioManager = AudioManager_PrototypeHero.instance;
        animator = GetComponent<Animator>();
        if (keySprite != null)
        {
            keySprite.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && chestInRange && !opened)
        {
            opened = true;
            animator.SetTrigger("Open");
            if (opened)
            {
                Score.Instance.AddPoints(scoreAmount);
            }
        }

        if (currentEnemy != null)
        {
            if (currentEnemy.IsDead)
            {
                animator.SetTrigger("IsDead");

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.tag == "Chest")
        {
            chestInRange = true;
            keySprite.enabled = true;
        }
        if (collision.tag == "Player" && this.tag == "Collectible")
        {
            Score.Instance.AddPoints(scoreAmount);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && this.tag == "enemy")
        {
            player.TakeDamage(damageAmount);
        }
        if (collision.gameObject.CompareTag("Player") && this.tag == "death")
        {
            player.TakeDamage(damageAmount);
        }
        if (collision.gameObject.CompareTag("hitbox") && this.tag == "enemy")
        {
            currentEnemy = this.gameObject.GetComponent<Enemy>();
            audioManager.PlaySound("Hurt");
            currentEnemy.TakeDamage(10);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        chestInRange = false;
        if (keySprite != null)
        {
            keySprite.enabled = false;
        }
    }
}
