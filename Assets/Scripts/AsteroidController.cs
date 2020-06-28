using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private Vector3 originalPos;
    private int countHit = 0;
    private Rigidbody2D body;
    private float angle;
    private bool isAllowMoving = false;
    private float moveSpeed = 1200f;

    public GameObject explosionPrefab;
    public List<Sprite> spriteList = new List<Sprite>();

    private void Awake()
    {
        originalPos = transform.position;
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = spriteList[0];
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        countHit = 0;
        spriteRender.sprite = spriteList[0];
        isAllowMoving = true;
        Moving(Vector3.down);
    }

    private void OnDisable()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowMoving)
        {
            angle += Time.deltaTime * -50;
            body.MoveRotation(angle);
            if(transform.position.y < -1f * (Camera.main.orthographicSize + 50f))
            {
                ResetDefault();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if (!spriteRender)
            {
                spriteRender = GetComponent<SpriteRenderer>();
            }
            if (countHit < spriteList.Count - 1)
            {
                spriteRender.sprite = spriteList[++countHit];
            }
            else
            {
                Debug.Log(gameObject.name + "is destroyed");
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                transform.position = originalPos;
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + "is destroyed");
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            transform.position = originalPos;
        }
    }

    private void Moving(Vector3 direction)
    {
        body.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }

    private void ResetDefault()
    {
        transform.position = originalPos;
        isAllowMoving = false;
        body.rotation = 0;
        angle = 0;
        gameObject.SetActive(false);
    }
}
