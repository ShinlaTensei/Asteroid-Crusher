using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector3 originalPos;
    public float moveSpeed;
    public GameObject explosionPrefab;
    public float damage;

    private void Awake()
    {
        originalPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != originalPos)
        {
            float distance = Vector3.Distance(transform.position, originalPos);
            if (distance >= 200f)
            {
                transform.position = originalPos;
                gameObject.SetActive(false);
                body.velocity = Vector2.zero;
            }
        }
    }

    public void Fired(Vector3 direction)
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        body.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }

    public void Fired(Vector3 direction, Quaternion rotation)
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        transform.rotation = rotation;
        body.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            transform.position = originalPos;
            gameObject.SetActive(false);
            body.velocity = Vector2.zero;
        }
    }
}
