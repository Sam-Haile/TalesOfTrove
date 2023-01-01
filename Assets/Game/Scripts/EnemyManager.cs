using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{

    protected Enemy enemy;
    public float distance;
    protected GameObject player;
    public Transform startingPoint;
    public bool chase = false;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance between player and enemy
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        Flip();
    }

    protected abstract void EnemyState();

    protected abstract void EnemyAnims();

    protected void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + .5f, player.transform.position.y + .5f), speed * Time.deltaTime);
    }

    protected void ReturnToStartingPosition()
    {
        if (enemy.currentHealth > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        }
    }

    protected void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    protected void KnockBack()
    {
        Vector2 position = transform.position;
        //b_rigidbody.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);

    }
}
