using System;
using Pattern;
using UnityEngine;
using Base;
using Unity.Collections;
using Unity.Jobs;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    [NonSerialized] public readonly StateMachine gameStateMachine = new StateMachine();
    [SerializeField] private bool isDebug = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance.Log("Vào GameManager.Start");
        if (File.Exists(Application.dataPath + "/log.txt"))
        {
            File.Delete(Application.dataPath + "/log.txt");
        }
    }

    private void OnEnable()
    {
        Instance.Log("Vào GameManager.OnEnable");
        PlayerManager.Instance.InitData(new PlayerData(3000, 0));
    }

    private void OnDisable()
    {
        
    }

    private void OnApplicationQuit()
    {
        Instance.Log("Vào GameManager.OnApplicationQuit");
        Debug.Log("Game quited");
    }

    public void ShowMessage(string message)
    {
        Instance.Log("Vào GameManager.ShowMessage");
    }

    public void ShowLoading(bool isLoading)
    {
        Instance.Log("Vào GameManager.ShowLoading");
    }

    public void TweenFrom(GameObject target, Func<Vector3> startPos)
    {
        Instance.Log("Vào GameManager.TweenFrom");
        iTween.MoveFrom(target, iTween.Hash("position", startPos.Invoke(), "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }
    public void TweenFrom(GameObject target, Vector3 startPos)
    {
        Instance.Log("Vào GameManager.TweenFrom");
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }

    public bool HasConnection()
    {
        Instance.Log("Vào GameManager.HasConnection");
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

    public void Log(string message)
    {
        if (isDebug)
        {
            string path = Application.dataPath + "/log.txt";
            using (StreamWriter write = new StreamWriter(path, true))
            {
                write.WriteLine(message);
            }
        }
    }
}
