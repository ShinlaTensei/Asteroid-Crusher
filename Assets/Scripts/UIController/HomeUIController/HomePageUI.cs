using System;
using UnityEngine;

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
        TweenFrom(singlePlayButton, new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             singlePlayButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            singlePlayButton.GetComponent<RectTransform>().anchoredPosition.y));

        TweenFrom(scoresButton,
            new Vector3(
                GetComponent<RectTransform>().rect.width / 2.0f +
                scoresButton.GetComponent<RectTransform>().rect.width / 2.0f, 
                scoresButton.GetComponent<RectTransform>().anchoredPosition.y));
        
        TweenFrom(armoryButton, new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             armoryButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            armoryButton.GetComponent<RectTransform>().anchoredPosition.y));
        
        TweenFrom(groupButtonLeftButton, new Vector3(
                groupButtonLeftButton.GetComponent<RectTransform>().anchoredPosition.x,
                (GetComponent<RectTransform>().rect.height / 2.0f +
                 groupButtonLeftButton.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
        
        TweenFrom(connectFbButton, new Vector3(
            connectFbButton.GetComponent<RectTransform>().anchoredPosition.x,
            (GetComponent<RectTransform>().rect.height / 2.0f +
             connectFbButton.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
    }

    // ****************************************************************
    // ********************** BUTTON EVENTS METHODS *******************
    // ****************************************************************

    public void OnClickSetting(GameObject settingPage)
    {
        settingPage.SetActive(true);
        gameObject.SetActive(false);
    }
    
    // ****************************************************************
    // ********************** OTHER METHODS ***************************
    // ****************************************************************

    private void TweenFrom(GameObject target, Func<Vector3> startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos.Invoke(), "easeType", iTween.EaseType.easeInBack, 
            "time", .75));
    }
    private void TweenFrom(GameObject target, Vector3 startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .75f));
    }
}
