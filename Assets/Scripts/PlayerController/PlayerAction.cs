using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour
{
    #region Elements
    [Header("Set in Inspector")]
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

    }

    void HandleMovement(float joyHorizontal, float joyVertical)
    {
        if ((joyHorizontal > 0.1f || joyHorizontal < -0.1f) || (joyVertical > 0.1f || joyVertical < -0.1f))
        {
            float horizontal = joyHorizontal * moveSpeed;
            float vertical = joyVertical * moveSpeed;
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
