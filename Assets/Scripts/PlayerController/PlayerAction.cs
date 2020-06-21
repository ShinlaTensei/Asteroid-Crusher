using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    #region Elements
    [Header("Set in Inspector")]
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float moveSpeed;

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
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = joystick.Horizontal * moveSpeed;
        float vertical = joystick.Vertical * moveSpeed;
        rigidBody.MovePosition(rigidBody.position + new Vector2(horizontal * Time.deltaTime, vertical * Time.deltaTime));
    }

    public void SetController(Joystick jstick)
    {
        joystick = jstick;
    }

    public void Shoot()
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool("bullet_blaster_small_single", transform.position, Quaternion.identity);
        projectile.GetComponent<BulletController>().Fired(Vector3.up);
    }
}
