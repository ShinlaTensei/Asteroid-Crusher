﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Vector3 originalPos;
    protected string tagShootFrom = String.Empty;
    public string TagShotFrom => tagShootFrom;

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
            if (distance >= 100f)
            {
                transform.position = originalPos;
                gameObject.SetActive(false);
                body.velocity = Vector2.zero;
            }
        }
    }

    public virtual void Fired(Vector3 direction, string tag)
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        body.AddForce(direction * moveSpeed, ForceMode2D.Force);
        tagShootFrom = tag;
    }

    public virtual void Fired(Vector3 direction, Quaternion rotation, string tag)
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        tagShootFrom = tag;
        transform.rotation = rotation;
        body.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Shield"))
        {
            DestroyBullet();
        }
    }

    protected void DestroyBullet()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        transform.position = originalPos;
        gameObject.SetActive(false);
        body.velocity = Vector2.zero;
    }
}
