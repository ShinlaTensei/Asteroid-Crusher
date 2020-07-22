using System;
using System.Collections.Generic;
using Base;
using Pattern.Implement;
using UnityEngine;
using ICommand = Pattern.Interface.ICommand;

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

    // ****************************************************************
    // ********************** BUTTON EVENTS METHODS *******************
    // ****************************************************************

    public void ClickGoto(GameObject destination)
    {
        GameManager.Instance.Log("Vào HomePageUI.ClickGoto");
        ICommand clickGoto = new ClickGoTo(gameObject);
        clickGoto.Execute(destination);
    }

    // ****************************************************************
    // ********************** OTHER METHODS ***************************
    // ****************************************************************
    
}
