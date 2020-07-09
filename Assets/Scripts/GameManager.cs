using System;
using System.Collections;
using Pattern;
using Pattern.Implement;
using UnityEngine;
using Object = UnityEngine.Object;

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
