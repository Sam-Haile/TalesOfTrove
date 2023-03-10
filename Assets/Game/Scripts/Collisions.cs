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
    public List<GameObject> collectedGems;
    public int scoreAmount = 0;
    public float damageAmount;
    private bool opened = false;
    private bool chestInRange;
    private Enemy currentEnemy;
    private CircleCollider2D enemyCollider;


    IEnumerator TakeDamage(float waitTime)
    {
        player.TakeDamage(damageAmount);
        enemyCollider.enabled = false;
        yield return new WaitForSeconds(waitTime);
        enemyCollider.enabled = true;
    }
    IEnumerator Invincible(float waitTime)
    {
        enemyCollider.enabled = false;
        yield return new WaitForSeconds(waitTime);
    }
    private void Start()
    {
        enemyCollider = this.gameObject.GetComponent<CircleCollider2D>();
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
                audioManager.PlaySound("chestOpen");
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
        if (collision.tag == "Player" && this.tag == "nextLevel")
        {
        }
        if (collectedGems != null)
        {

            if (collision.tag == "Player" && this.tag == "blueGem")
            {
                audioManager.PlaySound("gemPickup");
                Score.Instance.AddPoints(scoreAmount);
                this.gameObject.SetActive(false);
                collectedGems[0].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "greenGem")
            {
                audioManager.PlaySound("gemPickup");
                Score.Instance.AddPoints(scoreAmount);
                this.gameObject.SetActive(false);
                collectedGems[1].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "redGem")
            {
                audioManager.PlaySound("gemPickup");
                Score.Instance.AddPoints(scoreAmount);
                this.gameObject.SetActive(false);
                collectedGems[2].SetActive(true);
            }
            else if (collision.tag == "Player" && this.tag == "whiteGem")
            {
                audioManager.PlaySound("gemPickup");
                Score.Instance.AddPoints(scoreAmount);
                this.gameObject.SetActive(false);
                collectedGems[3].SetActive(true);
            }

        }
        // Player x Enemy Collisions
        if (collision.gameObject.CompareTag("Player") && this.tag == "enemy")
        {
            StartCoroutine(TakeDamage(1f));
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        chestInRange = false;
        if (keySprite != null)
        {
            keySprite.enabled = false;
        }
        if (collision.gameObject.CompareTag("Player") && this.tag == "enemy")
        {
            // Makes player invunerable for a certain amount of time
            StartCoroutine(Invincible(.5f));
        }
    }
}
