using System;
using System.Collections;
using System.Collections.Generic;
using Constant;
using Base.Pattern.Implement;
using PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Pattern.Implement
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
        }

        public void OnScored(int score, int money)
        {
            PlayerManager.Instance.UserData.money += money;
            onScored?.Invoke(score, money);
        }

        public void OnHit(int crrHealth)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Handheld.Vibrate();
            }
            onHit?.Invoke(crrHealth);
        }

        public void InvokeOnGetPowerUp(PowerUpInfo type, object additionalData = null)
        {
            OnGetPowerUp?.Invoke(type, additionalData);
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

