using System;
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
        TweenFrom(cancelButton,
            new Vector3(
                (GetComponent<RectTransform>().rect.width / 2.0f +
                 cancelButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
                cancelButton.GetComponent<RectTransform>().anchoredPosition.y));
        TweenFrom(acceptButton,
            new Vector3(
                (GetComponent<RectTransform>().rect.width / 2.0f +
                 acceptButton.GetComponent<RectTransform>().rect.width / 2.0f),
                acceptButton.GetComponent<RectTransform>().anchoredPosition.y));
        TweenFrom(settingPopup,
            new Vector3(settingPopup.GetComponent<RectTransform>().anchoredPosition.x,
                (GetComponent<RectTransform>().rect.height / 2.0f +
                 settingPopup.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
    }

    // ****************************************************************
    // ********************** BUTTON EVENTS METHODS *******************
    // ****************************************************************

    public void OnClickCancel(GameObject returnPage)
    {
        returnPage.SetActive(true);
        gameObject.SetActive(false);
    }
    
    // ****************************************************************
    // ********************** OTHER METHODS ***************************
    // ****************************************************************

    private void TweenFrom(GameObject target, Func<Vector3> startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos.Invoke(), "easeType", iTween.EaseType.easeInBack, 
            "time", .5f));
    }
    private void TweenFrom(GameObject target, Vector3 startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .5f));
    }
}
