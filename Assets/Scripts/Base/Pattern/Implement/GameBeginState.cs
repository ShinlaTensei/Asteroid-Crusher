using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
namespace Base.Pattern.Implement
{
    public class GameBeginState : State
    {
        public override void Enter()
        {
            Scene currScene = SceneManager.GetActiveScene();
            if (currScene != SceneManager.GetSceneByBuildIndex(Constant.IndexScene.MainScene))
            {
                GameManager.Instance.ShowLoading(true);
                AsyncOperation operation =
                    SceneManager.LoadSceneAsync(Constant.IndexScene.MainScene, LoadSceneMode.Single);
                operation.completed += (asyncOperation) =>
                {
                    GameManager.Instance.ShowLoading(false);
                    base.Enter();
                };
            }
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
                };
            }
        }
    }
    
    public class GameOverState : State
    {
        public void SaveScore(int score)
        {
            PlayerManager.Instance.UserData.score = score;
        }
    }
}