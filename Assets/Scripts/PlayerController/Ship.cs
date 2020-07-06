using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public ShipInfo shipInfo = new ShipInfo();

    #region ************************** Private fields *******************************
    private Rigidbody2D rigidBody;
    #endregion
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    public void HandleMovement(float joyHorizontal, float joyVertical)
    {
        if ((joyHorizontal > 0.1f || joyHorizontal < -0.1f) || (joyVertical > 0.1f || joyVertical < -0.1f))
        {
            float horizontal = joyHorizontal * shipInfo.speed;
            float vertical = joyVertical * shipInfo.speed;
            float xPos = Mathf.Clamp(rigidBody.transform.position.x + horizontal * Time.deltaTime,
                                        Camera.main.orthographicSize * -2f, Camera.main.orthographicSize * 2f);
            float yPos = Mathf.Clamp(rigidBody.position.y + vertical * Time.deltaTime,
                                        Camera.main.orthographicSize * -1f, Camera.main.orthographicSize);
            rigidBody.MovePosition(new Vector2(xPos, yPos));
        }
    }

    public void Shoot()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool("bullet_blaster_small_single", transform.position, Quaternion.identity);
        projectile.GetComponent<BulletController>().Fired(Vector3.up);
    }
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
    public List<Sprite> shipImage;
    public float originSpeed;
    public int originFuelConsumption;
    public int originEndurance;
    public bool isOwn = false;

    [SerializeField] private GameObject thisShip;
    public GameObject ThisShip => thisShip;
}
