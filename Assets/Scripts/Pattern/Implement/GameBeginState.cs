using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
namespace Pattern.Implement
{
    public class GameBeginState : State
    {
        public override void Enter()
        {
            base.Enter();
            Scene currScene = SceneManager.GetActiveScene();
            if (currScene != SceneManager.GetSceneByBuildIndex(Constant.IndexScene.MainScene))
            {
                GameManager.Instance.ShowLoading(true);
                AsyncOperation operation =
                    SceneManager.LoadSceneAsync(Constant.IndexScene.MainScene, LoadSceneMode.Single);
                operation.completed += (asyncOperation) =>
                {
                    GameManager.Instance.ShowLoading(false);
                    GameManager.Instance.gameStateMachine.OnEnter();
                };
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class GameExitState : State
    {
        public override void Enter()
        {
            base.Enter();
            Scene currScene = SceneManager.GetActiveScene();
            if (currScene == SceneManager.GetSceneByBuildIndex(Constant.IndexScene.MainScene))
            {
                GameManager.Instance.ShowLoading(true);
                AsyncOperation loadScene =
                    SceneManager.LoadSceneAsync(Constant.IndexScene.HomeScene, LoadSceneMode.Single);
                loadScene.completed += (operation) =>
                {
                    GameManager.Instance.ShowLoading(false);
                    GameManager.Instance.gameStateMachine.OnEnter();
                };
            }
        }
    }
    
    public class GameOverState : State
    {
        public override void Enter()
        {
            GameManager.Instance.gameStateMachine.OnEnter();
        }

        public void SaveScore(int score)
        {
            PlayerManager.Instance.UserData.score = score;
        }
    }
}