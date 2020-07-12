using System;
using System.Collections;
using System.Net;
using Pattern;
using Pattern.Implement;
using UnityEngine;
using System.Threading;
using Base;
using Unity.Collections;
using Unity.Jobs;

public class GameManager : Singleton<GameManager>
{
    [NonSerialized]public readonly StateMachine gameStateMachine = new StateMachine();

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

    private void OnApplicationQuit()
    {
        Debug.Log("Game quited");
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

    public bool HasConnection()
    {
        NativeArray<bool> result = new NativeArray<bool>(1, Allocator.TempJob);
        CheckConnectionJob networkJob = new CheckConnectionJob();
        networkJob.result = result;
        networkJob.timeOut = 5000;

        JobHandle handle = networkJob.Schedule();
        handle.Complete();
        bool hasConnection = result[0];
        result.Dispose();
        return hasConnection;
    }
}
