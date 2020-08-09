using System;
using System.Collections.Generic;
using Base;
using Pattern.Implement;
using UnityEngine;
using UnityEngine.UI;
using ICommand = Pattern.Interface.ICommand;
using Facebook.Unity;

public class HomePageUI : MonoBehaviour
{
    // ****************************************************************
    // ********************** PUBLIC FIELDS ***************************
    // ****************************************************************
    [SerializeField]
    private GameObject singlePlayButton;
    [SerializeField]
    private GameObject scoresButton;
    [SerializeField]
    private GameObject armoryButton;
    [SerializeField]
    private GameObject groupButtonLeftButton;
    [SerializeField]
    private GameObject connectFbButton;

    public Image testImage;

    private void OnEnable()
    {
        GameManager.Instance.Log("Vào HomePageUI.OnEnable");
        GameManager.Instance.TweenFrom(singlePlayButton, new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             singlePlayButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            singlePlayButton.GetComponent<RectTransform>().anchoredPosition.y));

        GameManager.Instance.TweenFrom(scoresButton,
            new Vector3(
                GetComponent<RectTransform>().rect.width / 2.0f +
                scoresButton.GetComponent<RectTransform>().rect.width / 2.0f, 
                scoresButton.GetComponent<RectTransform>().anchoredPosition.y));
        
        GameManager.Instance.TweenFrom(armoryButton, new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             armoryButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            armoryButton.GetComponent<RectTransform>().anchoredPosition.y));
        
        GameManager.Instance.TweenFrom(groupButtonLeftButton, new Vector3(
                groupButtonLeftButton.GetComponent<RectTransform>().anchoredPosition.x,
                (GetComponent<RectTransform>().rect.height / 2.0f +
                 groupButtonLeftButton.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
        
        GameManager.Instance.TweenFrom(connectFbButton, new Vector3(
            connectFbButton.GetComponent<RectTransform>().anchoredPosition.x,
            (GetComponent<RectTransform>().rect.height / 2.0f +
             connectFbButton.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
    }

    private void Start()
    {
        if (PlayerManager.Instance.UserData.facebookData.isLoginFacebook && !FB.IsLoggedIn)
        {
            ClickConnectFacebook();
        }
    }

    // ****************************************************************
    // ********************** BUTTON EVENTS METHODS *******************
    // ****************************************************************

    public void ClickGoto(GameObject destination)
    {
        GameManager.Instance.Log("Vào HomePageUI.ClickGoto");
        ICommand clickGoto = new ClickGoTo(gameObject);
        clickGoto.Execute(destination);
    }

    public void ClickConnectFacebook()
    {
        GameManager.Instance.ShowLoading(true);
        GameManager.Instance.facebookApi.LoginFacebook();
        connectFbButton.SetActive(false);
    }

    public void ClickLeaderboard()
    {
        GameManager.Instance.playfabController.GetLeaderboard(() =>
        {
            Debug.Log("test");
        });
    }

    // ****************************************************************
    // ********************** OTHER METHODS ***************************
    // ****************************************************************
    
}
