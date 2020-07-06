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
                    Exit();
                };
            }
            else
            {
                
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}