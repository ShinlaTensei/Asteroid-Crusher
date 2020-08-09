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
    [SerializeField] private GameObject loadingPrefab;

    public FacebookAPI facebookApi { get; private set; }
    public PlayFabController playfabController { get; private set; }

    private GameObject loadingPanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Log("Vào GameManager.Start");
        if (File.Exists(Application.persistentDataPath + "/log.txt"))
        {
            File.Delete(Application.persistentDataPath + "/log.txt");
        }

        facebookApi = GetComponent<FacebookAPI>();
        playfabController = GetComponent<PlayFabController>();
        if (File.Exists(Application.persistentDataPath + "/" + Constant.Path.playerData)) LoadPlayerData();
        else PlayerManager.Instance.InitData(new PlayerData(3000, 0));
    }

    private void OnEnable()
    {
        Log("Vào GameManager.OnEnable");

    }

    private void OnDisable()
    {
        
    }

    private void OnApplicationQuit()
    {
        Log("Vào GameManager.OnApplicationQuit");
        Debug.Log("Game quited");
        m_ShuttingDown = true;
        SaveLoad.SaveToBinary(PlayerManager.Instance.UserData, Constant.Path.playerData);
    }

    public void ShowMessage(string message)
    {
        Log("Vào GameManager.ShowMessage");
    }

    public void ShowLoading(bool isLoading)
    {
        Log("Vào GameManager.ShowLoading");
        if (isLoading)
        {
            loadingPanel = Instantiate(loadingPrefab);
            var canvas = GameObject.FindObjectOfType<Canvas>();
            loadingPanel.transform.SetParent(canvas.transform, false);
        }
        else
        {
            if(loadingPanel) Destroy(loadingPanel);
        }
    }

    public void TweenFrom(GameObject target, Func<Vector3> startPos)
    {
        Log("Vào GameManager.TweenFrom");
        iTween.MoveFrom(target, iTween.Hash("position", startPos.Invoke(), "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }
    public void TweenFrom(GameObject target, Vector3 startPos)
    {
        Log("Vào GameManager.TweenFrom");
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }

    public bool HasConnection()
    {
        Log("Vào GameManager.HasConnection");
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
            string path = Application.persistentDataPath + "/log.txt";
            using (StreamWriter write = new StreamWriter(path, true))
            {
                write.WriteLine(message);
            }
        }
    }

    private void LoadPlayerData()
    {
        SaveLoad.LoadFromBinary(out PlayerData data, Constant.Path.playerData);
        if (data == null) data = new PlayerData(3000, 0);
        PlayerManager.Instance.InitData(data);
    }
    
}
