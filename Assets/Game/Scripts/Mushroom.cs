using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    public PrototypeHero playerHealth;
    private float distance;
    private Animator m_animator;
    private MushroomState mushroom = MushroomState.Idle;
    private Enemy enemy;
    private Rigidbody2D m_rigidbody;
    private BoxCollider2D boxCollider;
    public enum MushroomState
    {
        Idle,
        Chase,
        Attack,
        Hurt,
        Die
    }


    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {

        MushroomStates();
        MushroomAnims();
        // Calculate distance between player and enemy
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (player == null)
            return;
        if (chase && enemy.isDead == false)
            Chase();
        else
            ReturnToStartingPosition();
        if (playerHealth.health <= 0)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
    }

    private void ReturnToStartingPosition()
    {
        if (enemy.currentHealth > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(startingPoint.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void MushroomStates()
    {
        switch (mushroom)
        {
            case MushroomState.Idle:
                if (chase)
                {
                    mushroom = MushroomState.Chase;
                }
                else if (enemy.currentHealth <= 0)
                {
                    mushroom = MushroomState.Die;
                }
                else if (enemy.isHit == true)
                {
                    mushroom = MushroomState.Hurt;
                }
                else if (distance < .5f)
                {
                    mushroom = MushroomState.Attack;
                }
                break;
            case MushroomState.Chase:
                if (distance < .5f)
                {
                    mushroom = MushroomState.Attack;
                }
                else if (transform.position.x == startingPoint.position.x)
                {
                    mushroom = MushroomState.Idle;
                }
                else if (enemy.currentHealth <= 0)
                {
                    mushroom = MushroomState.Die;
                }
                else if (enemy.isHit == true)
                {
                    mushroom = MushroomState.Hurt;
                }
                break;
            case MushroomState.Attack:
                if (transform.position.x == startingPoint.position.x)
                {
                    mushroom = MushroomState.Idle;
                }
                else if (enemy.currentHealth <= 0)
                {
                    mushroom = MushroomState.Die;
                }
                else if (enemy.isHit == true)
                {
                    mushroom = MushroomState.Hurt;
                }
                else if (distance > 1.5f)
                {
                    mushroom = MushroomState.Chase;
                }
                break;
            case MushroomState.Hurt:
                if (enemy.currentHealth <= 0)
                {
                    mushroom = MushroomState.Die;
                }
                else if (distance < .5f)
                {
                    mushroom = MushroomState.Attack;
                }
                else if (transform.position.x == startingPoint.position.x && !enemy.isHit)
                {
                    mushroom = MushroomState.Idle;
                }
                else if (chase)
                {
                    mushroom = MushroomState.Chase;
                }
                break;
            case MushroomState.Die:
                if (boxCollider != null)
                {
                    boxCollider.enabled = true;
                }
                m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
                break;
        }
    }
    private void MushroomAnims()
    {
        switch (mushroom)
        {
            case MushroomState.Idle:
                m_animator.SetBool("idle", true);
                m_animator.SetBool("chase", false);
                m_animator.SetBool("attack", false);
                break;
            case MushroomState.Chase:
                m_animator.SetBool("idle", false);
                m_animator.SetBool("chase", true);
                m_animator.SetBool("attack", false);
                m_animator.SetBool("isHit", false);
                break;
            case MushroomState.Attack:
                m_animator.SetBool("attack", true);
                m_animator.SetBool("chase", false);
                m_animator.SetBool("idle", false);
                m_animator.SetBool("isHit", false);
                break;
            case MushroomState.Hurt:
                m_animator.SetBool("isHit", true);
                break;
            case MushroomState.Die:
                m_animator.SetTrigger("isDead");
                break;
        }
    }
}
