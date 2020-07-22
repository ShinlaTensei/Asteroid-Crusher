using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Pattern;
using Pattern.Implement;
using UnityEngine;
using Base;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public ShipInfo shipInfo = new ShipInfo();
    public List<Sprite> shipImage = new List<Sprite>();
    
    #region ************************** Private fields *******************************
    private Rigidbody2D rigidBody;
    private int health = 5;
    private string projectileName = Projectile.CLUSTER_BOMB;

    public SpriteRenderer ShipSprite { get; private set; }
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject collectHealthParticle;
    #endregion
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ShipSprite = GetComponent<SpriteRenderer>();
    }
    
    public void HandleMovement(float joyHorizontal, float joyVertical)
    {
        GameManager.Instance.Log("Vào Ship.HandleMovement");
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

    public void Shoot(bool canShootOtherWeapon)
    {
        GameManager.Instance.Log("Vào Ship.Shoot");
        if (!canShootOtherWeapon)
        {
            projectileName = Projectile.BLASTER_SMALL;
        }
        for (int i = 0; i < shipInfo.numberOfCannon; ++i)
        {
            Transform gunsTransform = transform.GetChild(1).GetChild(i);
            if (shipInfo.numberOfCannon == 2)
            {
                gunsTransform = transform.GetChild(1).GetChild(i + 1);
            }
            Vector3 shootPos = gunsTransform.position;
            GameObject projectile = ObjectPooler.Instance.SpawnFromPool(projectileName, shootPos, Quaternion.identity);
            projectile.GetComponent<BulletController>().Fired(Vector3.up, gameObject.tag);
        }
    }

    private void GetPowerUp(PowerUp pw, GameRunningState state)
    {
        GameManager.Instance.Log("Vào Ship.GetPowerUp");
        if (pw.info.type == Powerup.HEALTH)
        {
            health = Mathf.Clamp(health + 1, 0, 5);
            state.InvokeOnGetPowerUp(pw.info, health);
            GameObject ps = Instantiate(collectHealthParticle, transform.position, Quaternion.identity, transform);
        }
        else if (pw.info.type == Powerup.SHIELD)
        {
            ActiveShield(pw.info.timeActive);
        }
        else if (pw.info.type == Powerup.SPEED)
        {
            IEnumerator BoostUpSpeed(float time)
            {
                float lastSpeed = shipInfo.speed;
                shipInfo.speed += Const.SPEED_BOOST;
                yield return new WaitForSeconds(time);
                shipInfo.speed = lastSpeed;
            }
            StartCoroutine(BoostUpSpeed(pw.info.timeActive));
        } 
        else if (pw.info.type == Powerup.CLUSTER)
        {
            state.InvokeOnGetPowerUp(pw.info);
        }
    }

    private void ActiveShield(float time, bool isActive = true)
    {
        GameManager.Instance.Log("Vào Ship.ActiveShield");
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        
        IEnumerator DeactiveShield(GameObject _shield)
        {
            yield return new WaitForSeconds(time);
            Destroy(_shield);
        }

        StartCoroutine(DeactiveShield(shield));
    }

    #region ***************************** LOGIC COLLISION *****************************

    private void OnTriggerEnter2D(Collider2D collide)
    {
        GameManager.Instance.Log("Vào Ship.OnTriggerEnter2D");
        State state = GameManager.Instance.gameStateMachine.currentState;
        if (state is GameRunningState runningState)
        {
            if (collide.gameObject.CompareTag("Asteroid") 
                || (collide.gameObject.CompareTag("Projectile") 
                    && !gameObject.CompareTag(collide.gameObject.GetComponent<BulletController>().TagShotFrom)))
            {
                health = Mathf.Clamp(health - 1, 0, 5);
                runningState.OnHit(health);
            }
            else if (collide.gameObject.CompareTag("Powerups"))
            {
                GetPowerUp(collide.gameObject.GetComponent<PowerUp>(), runningState);
            }
        }
    }
    

    #endregion
}

[Serializable]
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
