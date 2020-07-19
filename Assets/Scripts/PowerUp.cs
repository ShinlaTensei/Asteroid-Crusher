using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpInfo info;

    [SerializeField] private GameObject circleBound;
    [SerializeField] private GameObject iconPowerUp;
    private Rigidbody2D body;
    private Vector3 originalPos;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
    }
    private void Update()
    {
        circleBound.transform.Rotate(Time.deltaTime * new Vector3(0,0,50f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            transform.position = originalPos;
        }
    }

    public void SetPwInfo(PowerUpInfo pwInfo)
    {
        info = pwInfo;
        SetSpritePw(info.Icon, info.BoundingCircle);
    }

    public void SetSpritePw(Sprite icon, Sprite boundCircle)
    {
        circleBound.GetComponent<SpriteRenderer>().sprite = boundCircle;
        iconPowerUp.GetComponent<SpriteRenderer>().sprite = icon;
    }
}
