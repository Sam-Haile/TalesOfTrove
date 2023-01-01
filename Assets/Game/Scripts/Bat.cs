using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    private float distance;
    private Animator b_animator;
    private BatState bat = BatState.Idle;
    private Enemy enemy;
    private Rigidbody2D b_rigidbody;
    private BoxCollider2D boxCollider;
    public enum BatState
    {
        Idle,
        Chase,
        Attack,
        Hurt,
        Die
    }


    void Start()
    {
        b_rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        b_animator = GetComponent<Animator>();
    }

    void Update()
    {
        BatStates();
        BatAnims();
        // Calculate distance between player and enemy
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (player == null)
            return;
        if (chase && enemy.isDead == false)
            Chase();
        else
            ReturnToStartingPosition();

        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + .5f, player.transform.position.y + .5f), speed * Time.deltaTime);
    }

    private void ReturnToStartingPosition()
    {
        if (enemy.currentHealth > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void BatStates()
    {
        switch (bat)
        {
            case BatState.Idle:
                if (chase)
                {
                    bat = BatState.Chase;
                }
                else if (enemy.currentHealth <= 0)
                {
                    bat = BatState.Die;
                }
                else if (enemy.isHit == true)
                {
                    bat = BatState.Hurt;
                }
                else if (distance < 1.5f)
                {
                    bat = BatState.Attack;
                }
                break;
            case BatState.Chase:
                if (distance < 1.5f)
                {
                    bat = BatState.Attack;
                }
                else if (!chase)
                {
                    bat = BatState.Idle;
                }
                else if (enemy.currentHealth <= 0)
                {
                    bat = BatState.Die;
                }
                else if (enemy.isHit == true)
                {
                    bat = BatState.Hurt;
                }
                break;
            case BatState.Attack:
                if (!chase)
                {
                    bat = BatState.Idle;
                }
                else if (enemy.currentHealth <= 0)
                {
                    bat = BatState.Die;
                }
                else if (enemy.isHit == true)
                {
                    bat = BatState.Hurt;
                }
                else if (distance > 1.5f)
                {
                    bat = BatState.Chase;
                }
                break;
            case BatState.Hurt:
                if (enemy.currentHealth <= 0)
                {
                    bat = BatState.Die;
                }
                else if (distance < 1.5f)
                {
                    bat = BatState.Attack;
                }
                else if (!chase && !enemy.isHit)
                {
                    bat = BatState.Idle;
                }
                else if (chase && !enemy.isHit)
                {
                    bat = BatState.Chase;
                }
                break;
            case BatState.Die:
                boxCollider.enabled = true;
                b_rigidbody.bodyType = RigidbodyType2D.Dynamic;
                break;
        }
    }
    private void BatAnims()
    {
        switch (bat)
        {
            case BatState.Idle:
                b_animator.SetBool("attack", false);
                b_animator.SetBool("idle", true);
                break;
            case BatState.Chase:
                b_animator.SetBool("attack", false);
                break;
            case BatState.Attack:
                b_animator.SetBool("attack", true);
                b_animator.SetBool("idle", false);
                b_animator.SetBool("isHit", false);
                break;
            case BatState.Hurt:
                b_animator.SetBool("isHit", true);
                b_animator.SetBool("idle", false);
                break;
            case BatState.Die:
                b_animator.SetTrigger("isDead");
                break;
        }
    }
}
