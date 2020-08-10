using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Networking;

namespace Base
{
    public class FacebookAPI : MonoBehaviour
    {
        public AccessToken aToken { get; private set; }

        private Dictionary<string, object> graphApiResult;
        private Dictionary<string, object> detailAvatar;
        private IJob currentJob;

        private string userName;
        public string UserName => userName;
        private int userId;
        public int UserId => userId;
        private Sprite userAvatar;

        public Sprite UserAvatar => userAvatar;

        private Action<bool> loginCallback;

        void Awake()
        {
            InitFacebook();
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnApplicationQuit()
        {
            FB.LogOut();
        }

        void InitFacebook()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(() =>
                {
                    if (FB.IsInitialized)
                    {
                        FB.ActivateApp();
                    }
                }, isGameShown =>
                {
                    if (isGameShown)
                    {
                        Time.timeScale = 1f;
                    }
                    else
                    {
                        Time.timeScale = 0f;
                    }
                });
            }
            else
            {
                FB.ActivateApp();
            }
        }
    
        public void LoginFacebook(Action<bool> callback)
        {
            if (!FB.IsInitialized) InitFacebook();
            if (FB.IsLoggedIn) return;
            loginCallback = callback;
            if (GameManager.Instance.HasConnection() == false)
            {
                GameManager.Instance.ShowMessage(Constant.Message.NetworkError);
                return;
            }
            
            var permission = new List<string>() {"public_profile"/*, "email", "user_friends", "user_photos", "user_birthday"*/};
            FB.LogInWithReadPermissions(permission, LoginFBCallback);
        }
    
        private void LoginFBCallback(ILoginResult result)
        {
            loginCallback.Invoke(FB.IsLoggedIn);
            if (FB.IsLoggedIn)
            {
                aToken = AccessToken.CurrentAccessToken;

                GetApi();
                GameManager.Instance.playfabController.LoginWithFacebook(aToken.TokenString);
                PlayerManager.Instance.UserData.facebookData.isLoginFacebook = true;
            }
            else
            {
                GameManager.Instance.ShowMessage(Constant.Message.LoginFbFailed);
                GameManager.Instance.ShowLoading(false);
                PlayerManager.Instance.UserData.facebookData.isLoginFacebook = false;
                // Handle login failed
            }
        }

        public void GetApi()
        {
            FB.API("me?fields=id,name,picture", HttpMethod.GET, GraphResultCallback);
        }

        private void GraphResultCallback(IGraphResult result)
        {
            if (result.Error == null)
            {
                graphApiResult = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
                graphApiResult.TryGetValue("name", out userName);
                graphApiResult.TryGetValue("id", out userId);
                Dictionary<string, object> data = graphApiResult["picture"] as Dictionary<string, object>;
                Dictionary<string, object> detail;
                data.TryGetValue("data", out detail);
                detailAvatar = detail;
                string url = detail["url"] as string;
                StartCoroutine(GetRequest(url));
            }
            else
            {
                GameManager.Instance.ShowLoading(false);
            }
        }

        IEnumerator GetRequest(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (!request.isNetworkError || !request.isHttpError)
            {
                var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                var width = Convert.ToSingle(detailAvatar["width"]);
                var height = Convert.ToSingle(detailAvatar["height"]);
                var rect = new Rect(0,0, width, height);
                userAvatar = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                userAvatar.name = "Avatar";
            }
            GameManager.Instance.ShowLoading(false);
        }
    }
}

