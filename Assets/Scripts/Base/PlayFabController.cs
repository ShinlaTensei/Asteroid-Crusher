using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using PlayFab.Json;
using LoginResult = PlayFab.ClientModels.LoginResult;


public class PlayFabController : MonoBehaviour
{
    private string titleId = "9C7A3";
    private int severScore;

    public bool IsLogin => PlayFabClientAPI.IsClientLoggedIn();

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = titleId;
        }
        // var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    #region  Login

    private bool isLogin = false;
    public void LoginWithFacebook(string tokenString)
    {
        var request = new LoginWithFacebookRequest
        {
            CreateAccount = true, AccessToken = tokenString
        };
        PlayFabClientAPI.LoginWithFacebook(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerManager.Instance.UserData.isLoginToPlayFab = true;
        isLogin = true;
        string name = GameManager.Instance.facebookApi.UserName;
        var request = new UpdateUserTitleDisplayNameRequest {DisplayName = name};
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, updateResult =>
        {
            Debug.Log(updateResult.DisplayName);
        }, ErrorCallback);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion

    #region Player Statistic
    public void SetStatistic(int value)
    {
        if (!isLogin) return;
        var executeCloudScriptRequest = new ExecuteCloudScriptRequest
        {
            FunctionName = "SetStatistic",
            FunctionParameter = new {Score = value},
            GeneratePlayStreamEvent = false,
        };
        PlayFabClientAPI.ExecuteCloudScript(executeCloudScriptRequest, SetStatisticSuccessCallback, ErrorCallback);
    }

    public void GetStatistics()
    {
        if (!isLogin) return;
        var executeCloudScriptRequest = new ExecuteCloudScriptRequest
        {
            FunctionName = "GetPlayerStatistics",
            FunctionParameter = new {Score = "Score"},
            GeneratePlayStreamEvent = false
        };
        PlayFabClientAPI.ExecuteCloudScript(executeCloudScriptRequest, GetStatisticsSuccessCallback, ErrorCallback);
    }

    private void SetStatisticSuccessCallback(ExecuteCloudScriptResult result)
    {
        
    }
    
    private void GetStatisticsSuccessCallback(ExecuteCloudScriptResult result)
    {
        JsonObject jsonResult = (JsonObject) result.FunctionResult;
        jsonResult.TryGetValue("score", out object messageValue);
        JsonObject resultMessage = messageValue as JsonObject;
        JsonArray statistics = (JsonArray) resultMessage["Statistics"];
        JsonObject scoreValue = statistics[0] as JsonObject;
        if (scoreValue.Contains(new KeyValuePair<string, object>("StatisticsName", "Score")))
        {
            severScore = (int) scoreValue["Value"];
        }
    }

    private void ErrorCallback(PlayFabError error)
    {
        var errorStr = error.GenerateErrorReport();
    }
    
    #endregion Player Statistic

    #region Leaderboard

    private Action<List<PlayerLeaderboardEntry>> callbackLeaderboard;
    private List<PlayerLeaderboardEntry> _leaderboardEntries = new List<PlayerLeaderboardEntry>();
    public void GetLeaderboard(Action<List<PlayerLeaderboardEntry>> callback)
    {
        var request = new GetLeaderboardRequest {StartPosition = 0, StatisticName = "Score", MaxResultsCount = 20};
        PlayFabClientAPI.GetLeaderboard(request, GetLeaderboardResult, ErrorCallback);
        callbackLeaderboard = callback;
    }

    private void GetLeaderboardResult(GetLeaderboardResult result)
    {
        _leaderboardEntries = result.Leaderboard;
        callbackLeaderboard.Invoke(result.Leaderboard);
    }
    

    #endregion
    
}