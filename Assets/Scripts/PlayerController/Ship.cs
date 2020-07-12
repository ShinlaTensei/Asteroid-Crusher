using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Pattern;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public ShipInfo shipInfo = new ShipInfo();
    public List<Sprite> shipImage = new List<Sprite>();

    #region ************************** EVENT ***********************************

    public event Action<int> OnHit;

    #endregion
    #region ************************** Private fields *******************************
    private Rigidbody2D rigidBody;

    public SpriteRenderer ShipSprite { get; private set; }
    #endregion
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ShipSprite = GetComponent<SpriteRenderer>();
    }

    public void HandleMovement(float joyHorizontal, float joyVertical)
    {
        if ((joyHorizontal > 0.1f || joyHorizontal < -0.1f) || (joyVertical > 0.1f || joyVertical < -0.1f))
        {
            float orthoSize = Camera.main.orthographicSize;
            float horizontal = joyHorizontal * shipInfo.speed;
            float vertical = joyVertical * shipInfo.speed;
            float xPos = Mathf.Clamp(rigidBody.transform.position.x + horizontal * Time.deltaTime,
                orthoSize * -2f, orthoSize * 2f);
            float yPos = Mathf.Clamp(rigidBody.position.y + vertical * Time.deltaTime,
                orthoSize * -1f, orthoSize);
            rigidBody.MovePosition(new Vector2(xPos, yPos));
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < shipInfo.numberOfCannon; ++i)
        {
            Transform gunsTransform = transform.GetChild(1).GetChild(i);
            if (shipInfo.numberOfCannon == 2)
            {
                gunsTransform = transform.GetChild(1).GetChild(i + 1);
            }
            Vector3 shootPos = gunsTransform.position;
            GameObject projectile = ObjectPooler.Instance.SpawnFromPool("bullet_blaster_small_single", shootPos, Quaternion.identity);
            projectile.GetComponent<BulletController>().Fired(Vector3.up, gameObject.tag);
        }
    }

    #region ***************************** LOGIC COLLISION *****************************

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.CompareTag("Asteroid") 
            || (collide.gameObject.CompareTag("Projectile") 
                && !gameObject.CompareTag(collide.gameObject.GetComponent<BulletController>().TagShotFrom)))
        {
            OnHit?.Invoke(5);
        }
    }
    

    #endregion
}

[System.Serializable]
public class ShipInfo
{
    public string name;
    public int numberOfCannon;
    public float speed;
    public int fuelConsumption;
    public int endurance;
    public int price;
    public float originSpeed;
    public int originFuelConsumption;
    public int originEndurance;
    public bool isOwn = false;
}
