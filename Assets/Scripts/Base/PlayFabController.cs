using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using PlayFab.Json;
using LoginResult = PlayFab.ClientModels.LoginResult;


public class PlayFabController : MonoBehaviour
{
    private string titleId = "9C7A3";
    public void Start()
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
    }

    private void ErrorCallback(PlayFabError error)
    {
        error.GenerateErrorReport();
    }
    
    #endregion
    
}