using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public Animator animator;
    private AudioManager_PrototypeHero audioManager;
    public PrototypeHero player;
    public SpriteRenderer keySprite;
    private Rigidbody2D rb;
    public int knockbackForce =50;

    public List<GameObject> collectedGems;
    public int scoreAmount = 0;
    public float damageAmount;
    public bool opened = false;
    private bool chestInRange;
    private Enemy currentEnemy;



    IEnumerator Invincible(float waitTime, GameObject enemy)
    {
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        enemyCollider.enabled = false;
        yield return new WaitForSeconds(waitTime);
        enemyCollider.enabled = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (currentEnemy.isDead)
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
            audioManager.PlaySound("coinPickup");
            Destroy(gameObject);
        }
        if (collectedGems != null)
        {

            if (collision.tag == "Player" && this.tag == "blueGem")
            {
                audioManager.PlaySound("gemPickup");
                this.gameObject.SetActive(false);
                collectedGems[0].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "greenGem")
            {
                this.gameObject.SetActive(false);
                collectedGems[1].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "redGem")
            {
                this.gameObject.SetActive(false);
                collectedGems[2].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "whiteGem")
            {
                this.gameObject.SetActive(false);
                collectedGems[3].SetActive(true);
            }

        }
        // Player x Enemy Collisions
        if (collision.gameObject.CompareTag("Player") && this.tag == "enemy")
        {
            player.TakeDamage(damageAmount);
            // Makes player invunerable for a certain amount of time
            StartCoroutine(Invincible(1f, this.gameObject));
        }
        if (collision.tag == "Ground" && this.tag == "enemy")
        {
        }
        // Spikes and other enviornmental traps
        if (collision.gameObject.CompareTag("Player") && this.tag == "death")
        {
            player.TakeDamage(damageAmount);
        }
        // Player damaging enemies
        if (collision.gameObject.CompareTag("hitbox") && this.tag == "enemy")
        {
            currentEnemy = this.gameObject.GetComponent<Enemy>();
            audioManager.PlaySound("Dodge");
            // collision object here is the colliders
            Debug.Log("Hit");
            currentEnemy.TakeDamage(10);
        }
        /*
        if (collision.gameObject.CompareTag("leftHitBox") && this.tag == "enemy")
        {
            currentEnemy = this.gameObject.GetComponent<Enemy>();
            audioManager.PlaySound("Dodge");
            // collision object here is the colliders
            rb.AddForce(new Vector2(-20,0), ForceMode2D.Impulse);
            currentEnemy.TakeDamage(10);
        }
        else if (collision.gameObject.CompareTag("rightHitBox") && this.tag == "enemy")
        {
            currentEnemy = this.gameObject.GetComponent<Enemy>();
            audioManager.PlaySound("Dodge");
            // collision object here is the colliders
            rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
            currentEnemy.TakeDamage(10);
        }*/

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
