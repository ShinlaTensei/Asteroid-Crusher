using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    private bool isGameOver = false;

    private Ship shipHandler;

    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        MainGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCommand(joystick.Horizontal, joystick.Vertical);
    }
    
    public void MainGameStart()
    {
        isGameOver = false;
        ObjectPooler.Instance.InitPool();
        InitPlayerShip();
        StartCoroutine(SpawnAsteroid());
    }

    IEnumerator SpawnAsteroid()
    {
        while (!isGameOver)
        {
            if (Camera.main != null)
            {
                float xPos =
                    Mathf.Clamp(
                        UnityEngine.Random.Range((-1f * Camera.main.orthographicSize * 2f) + 20f,
                            Camera.main.orthographicSize * 2f - 20f), -1f * Camera.main.orthographicSize * 2f,
                        Camera.main.orthographicSize * 2f);
                GameObject asteroid = ObjectPooler.Instance.SpawnFromPool("asteroid01",
                    new Vector3(xPos, Camera.main.orthographicSize + 20f, 0f), Quaternion.identity);
                asteroid.SetActive(true);
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    private void InitPlayerShip()
    {
        if (!shipHandler)
        {
            GameObject playerShip = Instantiate(PlayerManager.Instance.choosingShip);
            shipHandler = playerShip.GetComponent<Ship>();
        }
        Vector3 pos = new Vector3(0, -1f * Camera.main.orthographicSize - 10f, 0);
        shipHandler.transform.position = pos;
        iTween.MoveTo(shipHandler.gameObject, iTween.Hash("position", Vector3.zero, "easing", iTween.EaseType.easeInBack,
            "time", 2f));
    }

    public void ShootCommand()
    {
        shipHandler.Shoot();
    }

    private void MoveCommand(float horizontal, float vertical)
    {
        shipHandler.HandleMovement(horizontal, vertical);
    }
}
