using System;
using System.Collections;
using System.Collections.Generic;
using Pattern.Implement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pattern.Implement
{
    public class GameRunningState : State
    {
        public event Action<int, int> onScored;
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 1f;
            GameManager.Instance.gameStateMachine.OnEnter();
        }

        public void OnScored(int score, int money)
        {
            PlayerManager.Instance.UserData.money += money;
            onScored?.Invoke(score, money);
        }
    }

    public class GamePauseState : State
    {
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0;
        }
    }
}

