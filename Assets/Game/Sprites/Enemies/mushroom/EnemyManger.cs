using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{

    public Enemy enemy;
    private GameObject player;
    public Transform startingPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + .5f, player.transform.position.y + .5f), speed * Time.deltaTime);
    }

    public void ReturnToStartingPosition()
    {
        if (enemy.currentHealth > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
