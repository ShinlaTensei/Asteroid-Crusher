using System;
using System.Collections;
using Pattern;
using Pattern.Implement;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : Singleton<GameManager>
{
    private bool isGameOver = false;
    public StateMachine stateMachine = new StateMachine();

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
        PlayerManager.Instance.InitData(new PlayerData(3000, 0));
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
        ObjectPooler.Instance.InitPool();
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

    public void ShowLoading(bool isLoading)
    {
        
    }

    public void TweenFrom(GameObject target, Func<Vector3> startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos.Invoke(), "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }
    public void TweenFrom(GameObject target, Vector3 startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }
}
