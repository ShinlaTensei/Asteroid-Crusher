using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isGameOver = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerManager.Instance.InitData(new PlayerData(0, 0));
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }


    #region ************************** Calling the event **************************************
    public void MainGameStart()
    {
        isGameOver = false;
        StartCoroutine(SpawnAsteroid());
    }

    #endregion

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

    private void OnGameOver()
    {
        isGameOver = true;
        StopCoroutine(SpawnAsteroid());
    }

    public void ShowMessage(string message)
    {
        
    }
}
