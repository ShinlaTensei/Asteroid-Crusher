using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private Vector3 originalPos;
    private int countHit = 0;
    
    public GameObject explosionPrefab;
    public List<Sprite> spriteList = new List<Sprite>();

    private void Awake()
    {
        originalPos = transform.position;
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = spriteList[0];
    }

    private void OnEnable()
    {
        countHit = 0;
        spriteRender.sprite = spriteList[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
