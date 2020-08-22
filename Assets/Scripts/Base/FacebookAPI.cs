using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using Newtonsoft.Json;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Base
{
    public class FacebookAPI : MonoBehaviour
    {
        public AccessToken aToken { get; private set; }

        private GraphPictureResult graphApiResult;
        private IJob currentJob;

        private string userName;
        public string UserName => userName;
        private long userId;
        public long UserId => userId;
        private Sprite userAvatar;

        public Sprite UserAvatar => userAvatar;
        
        private string avatarUrl = String.Empty;

        public string AvatarUrl => avatarUrl;

        private Action<bool> loginCallback;

        internal class PictureResult
        {
            internal class Data
            {
                [JsonProperty("height")]
                public int height;
                [JsonProperty("is_silhouette")]
                public bool is_silhouette;
                [JsonProperty("url")]
                public string url;
                [JsonProperty("width")]
                public int width;
            }
            [JsonProperty("data")]
            public Data data;
        }

        internal class GraphPictureResult
        {
            [JsonProperty("id")]
            public string id;
            [JsonProperty("name")]
            public string name;
            [JsonProperty("picture")]
            public PictureResult picture;
        }

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
                ShowMessage(Constant.Message.NetworkError, () =>
                {
                    ShowLoading(false);
                }, () =>
                {
                    ShowLoading(false);
                });
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
                ShowMessage(Constant.Message.LoginFbFailed);
                ShowLoading(false);
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
                graphApiResult = JsonConvert.DeserializeObject<GraphPictureResult>(result.RawResult);
                if (graphApiResult.picture != null)
                {
                    userName = graphApiResult.name;
                    userId = long.Parse(graphApiResult.id);
                    avatarUrl = graphApiResult.picture.data.url;
                    StartCoroutine(GetRequest(avatarUrl));
                }
                else
                {
                    ShowLoading(false);
                }
            }
            else
            {
                ShowLoading(false);
            }
        }

        IEnumerator GetRequest(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (!request.isNetworkError || !request.isHttpError)
            {
                var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                var width = graphApiResult.picture.data.width;
                var height = graphApiResult.picture.data.height;
                var rect = new Rect(0,0, width, height);
                userAvatar = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                userAvatar.name = "Avatar";
            }
            ShowLoading(false);
        }
        
        private void ShowLoading(bool isShow)
        {
            GameManager.Instance.ShowLoading(isShow);
        }

        private void ShowMessage(string message, UnityAction acceptPress = null, UnityAction cancelPress = null)
        {
            GameManager.Instance.ShowMessage(message, acceptPress, cancelPress);
        }
    }
}

