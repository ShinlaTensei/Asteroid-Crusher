using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    public Text scoreTextThousand;
    public Text scoreTextBigThousand;
    
    [SerializeField] private GameObject[] arrayStarsBg = new GameObject[2];

    private Vector3[] originPos;

    private void Awake()
    {
        GameManager.Instance.gameStateMachine.OnStateEnter += OnStateEnter;
        GameManager.Instance.gameStateMachine.OnStateExit += OnStateExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        originPos = new Vector3[arrayStarsBg.Length];
        originPos[0] = arrayStarsBg[0].transform.position;
        originPos[1] = arrayStarsBg[1].transform.position;
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
            GameManager.Instance.gameStateMachine.OnStateExit -= OnStateExit;
            
            shipHandler.OnHit -= ShipHandlerOnOnHit;
        }
    }
    
    private void MainGameStart()
    {
        isGameOver = false;
        ObjectPooler.Instance.InitPool();
        InitPlayerShip();
        //StartCoroutine(SpawnAsteroid());
        InvokeRepeating("LaunchAsteroid", 3.0f, 1.5f);
        shipHandler.OnHit += ShipHandlerOnOnHit;
    }

    IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(3.0f);
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

    private void LaunchAsteroid()
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

    private void MovingBackground()
    {
        for (int i = 0; i < arrayStarsBg.Length; ++i)
        {
            string tag = $"star0{i + 1}";
            GameObject starBg = ObjectPooler.Instance.SpawnFromPool(tag, originPos[i], Quaternion.identity);
            
        }
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
            List<char> score = scoreTextBigThousand.text.Concat(scoreTextThousand.text.ToCharArray()).ToList();
            
            overState.SaveScore(int.Parse(score.ToString()));
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
        List<char> scoreText = scoreTextBigThousand.text.Concat(scoreTextThousand.text.ToCharArray()).ToList();
        int crrScore = int.Parse(String.Concat(scoreText));
        crrScore += score;
        List<string> text;
        FormatScore(crrScore, out text);
        scoreTextBigThousand.text = (text[0] + text[1]);
        scoreTextThousand.text = (text[2] + text[3] + text[4]);
    }

    private void FormatScore(int score, out List<string> scoreText)
    {
         scoreText = new List<string>();
        
        for (int i = 4; i >= 0; --i)
        {
            int res = (int)(score / Mathf.Pow(10f, i));
            if (res != 0)
            {
                scoreText.Add(res.ToString());
                score = (int) (score % Mathf.Pow(10f, i));
            }
            else
            {
                scoreText.Add("0");
            }
        }
    }
}
