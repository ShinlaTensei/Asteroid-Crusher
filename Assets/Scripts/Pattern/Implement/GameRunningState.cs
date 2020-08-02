using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Pattern.Implement;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pattern.Implement
{
    public class GameRunningState : State
    {
        public event Action<int, int> onScored;
        public event Action<int> onHit;
        public event Action<PowerUpInfo, object> OnGetPowerUp;
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 1f;
            GameManager.Instance.Log("Vào GameRunningState.Enter");
        }

        public void OnScored(int score, int money)
        {
            PlayerManager.Instance.UserData.money += money;
            onScored?.Invoke(score, money);
            GameManager.Instance.Log("Vào GameRunningState.OnScored");
        }

        public void OnHit(int crrHealth)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Handheld.Vibrate();
            }
            onHit?.Invoke(crrHealth);
            GameManager.Instance.Log("Vào GameRunningState.OnHit");
        }

        public void InvokeOnGetPowerUp(PowerUpInfo type, object additionalData = null)
        {
            OnGetPowerUp?.Invoke(type, additionalData);
            GameManager.Instance.Log("Vào GameRunningState.InvokeOnGetPowerUp");
        }
    }

    public class GamePauseState : State
    {
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0;
            GameManager.Instance.Log("Vào GamePauseState.Enter");
        }
    }
}

