using System;
using System.Security.Permissions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShip : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Transform player;
    
    [Header("Stats")]
    public float speed = 0;
    public float stoppingDistance = 0;
    public float retreatDistance = 0;


    [Header("Reference")] 
    public GameObject explosionPrefab;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            rigidbody2D.position = Vector2.MoveTowards(rigidbody2D.position, player.position, 
                speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance
                 && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            rigidbody2D.position = this.rigidbody2D.position;
        }
    }
}