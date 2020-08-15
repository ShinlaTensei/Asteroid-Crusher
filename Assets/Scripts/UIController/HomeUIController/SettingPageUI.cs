using System;
using Base.Pattern.Interface;
using Base.Pattern.Implement;
using UnityEngine;

public class SettingPageUI : MonoBehaviour
{
    // ****************************************************************
    // ********************** PUBLIC FIELDS ***************************
    // ****************************************************************
    [SerializeField]
    private GameObject cancelButton;
    [SerializeField]
    private GameObject acceptButton;
    [SerializeField]
    private GameObject settingPopup;

    private void OnEnable()
    {
        GameManager.Instance.TweenFrom(cancelButton,
            new Vector3(
                (GetComponent<RectTransform>().rect.width / 2.0f +
                 cancelButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
                cancelButton.GetComponent<RectTransform>().anchoredPosition.y));
        GameManager.Instance.TweenFrom(acceptButton,
            new Vector3(
                (GetComponent<RectTransform>().rect.width / 2.0f +
                 acceptButton.GetComponent<RectTransform>().rect.width / 2.0f),
                acceptButton.GetComponent<RectTransform>().anchoredPosition.y));
        GameManager.Instance.TweenFrom(settingPopup,
            new Vector3(settingPopup.GetComponent<RectTransform>().anchoredPosition.x,
                (GetComponent<RectTransform>().rect.height / 2.0f +
                 settingPopup.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
    }

    // ****************************************************************
    // ********************** BUTTON EVENTS METHODS *******************
    // ****************************************************************

    public void ClickGoto(GameObject returnPage)
    {
        ICommand clickGoto = new ClickGoTo(gameObject);
        clickGoto.Execute(returnPage);
    }
    
    // ****************************************************************
    // ********************** OTHER METHODS ***************************
    // ****************************************************************
    
}
