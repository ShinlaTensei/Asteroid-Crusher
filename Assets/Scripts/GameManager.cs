using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isGameOver = false;
    private Dictionary<GameConstant.EventType, Action> listHandlerAction;
    #region ************************ Event *******************************
    public event Action<float, float> joystickMove;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        listHandlerAction = new Dictionary<GameConstant.EventType, Action>();
    }

    private void OnEnable()
    {
        AddListener(GameConstant.EventType.GAME_OVER, OnGameOver);
    }

    private void OnDisable()
    {
        RemoveListener(GameConstant.EventType.GAME_OVER, OnGameOver);
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
        InvokeEvent(GameConstant.EventType.GAME_BEGIN);
    }

    public void JoystickMove(float horizontal, float vertical)
    {
        joystickMove.Invoke(horizontal, vertical);
    }
    #endregion

    public static void AddListener(GameConstant.EventType type, Action callBack)
    {
        Action cb = null;
        if (Instance.listHandlerAction == null)
        {
            Instance.listHandlerAction = new Dictionary<GameConstant.EventType, Action>();
        }
        if (Instance.listHandlerAction.TryGetValue(type, out cb))
        {
            cb += callBack;
        }
        else
        {
            cb = new Action(callBack);
            Instance.listHandlerAction.Add(type, cb);
        }
    }

    public static void RemoveListener(GameConstant.EventType type, Action callBack)
    {
        Action cb;
        if (Instance.listHandlerAction.TryGetValue(type, out cb))
        {
            cb -= callBack;
        }
    }

    public static void InvokeEvent(GameConstant.EventType type)
    {
        Action thisEvent;
        if (Instance.listHandlerAction.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while (!isGameOver)
        {
            float xPos = Mathf.Clamp(UnityEngine.Random.Range((-1f * Camera.main.orthographicSize * 2f) + 20f, Camera.main.orthographicSize *2f - 20f), -1f * Camera.main.orthographicSize * 2f, Camera.main.orthographicSize * 2f);
            GameObject asteroid = ObjectPooler.Instance.SpawnFromPool("asteroid01", new Vector3(xPos, Camera.main.orthographicSize + 20f, 0f), Quaternion.identity);
            asteroid.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void OnGameOver()
    {
        isGameOver = true;
        StopCoroutine(SpawnAsteroid());
    }
}
