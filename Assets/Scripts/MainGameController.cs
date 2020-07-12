using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Pattern;
using Pattern.Implement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainGameController : MonoBehaviour
{
    private bool isGameOver = false;

    private Ship shipHandler;

    public Joystick joystick;

    public Text scoreText;

    private void Awake()
    {
        GameManager.Instance.gameStateMachine.OnStateEnter += OnStateEnter;
        GameManager.Instance.gameStateMachine.OnStateExit += OnStateExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            MoveCommand(joystick.Horizontal, joystick.Vertical);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gameStateMachine.OnStateEnter -= OnStateEnter;
            
            shipHandler.OnHit -= ShipHandlerOnOnHit;
        }
    }
    
    public void MainGameStart()
    {
        isGameOver = false;
        ObjectPooler.Instance.InitPool();
        InitPlayerShip();
        StartCoroutine(SpawnAsteroid());
        shipHandler.OnHit += ShipHandlerOnOnHit;
    }

    IEnumerator SpawnAsteroid()
    {
        while (!isGameOver)
        {
            if (Camera.main != null)
            {
                float othoSize = Camera.main.orthographicSize;
                float xPos =
                    Mathf.Clamp(
                        Random.Range((-1f * othoSize * 2f) + 20f,
                            othoSize * 2f - 20f), -1f * othoSize * 2f,
                        othoSize * 2f);
                int x = Random.Range(1, 4);
                GameObject asteroid = ObjectPooler.Instance.SpawnFromPool($"asteroid0{x}",
                    new Vector3(xPos, othoSize + 20f, 0f), Quaternion.identity);
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
        shipHandler.ShipSprite.sprite = shipHandler.shipImage[shipHandler.shipInfo.numberOfCannon - 1];
        iTween.MoveTo(shipHandler.gameObject, iTween.Hash("position", new Vector3(-10f, 0, 0), "easing", iTween.EaseType.easeInBack,
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

    public void ClickExitGame()
    {
        GameManager.Instance.gameStateMachine.ChangeState(new GameOverState());
        GameManager.Instance.gameStateMachine.ChangeState(new GameExitState());
    }

    void OnStateEnter(State state)
    {
        if (state is GameBeginState)
        {
            MainGameStart();
            GameManager.Instance.gameStateMachine.ChangeState(new GameRunningState());
        }
        else if (state is GameOverState overState)
        {
            isGameOver = true;
            StopCoroutine(SpawnAsteroid());
            overState.SaveScore(int.Parse(scoreText.text));
        }
        else if (state is GameRunningState runningState)
        {
            runningState.onScored += OnScored;
        }
    }

    private void OnStateExit(State state)
    {
        if (state is GameRunningState runningState)
        {
            runningState.onScored -= OnScored;
        }
    }
    
    private void ShipHandlerOnOnHit(int obj)
    {
        
    }

    private void OnScored(int score, int money)
    {
        int crrScore = int.Parse(scoreText.text);
        crrScore += score;
        scoreText.text = crrScore.ToString();
    }
}
