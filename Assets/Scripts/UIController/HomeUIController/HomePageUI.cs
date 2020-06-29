using System;
using UnityEngine;

public class HomePageUI : MonoBehaviour
{
    // ****************************************************************
    // ********************** PUBLIC FIELDS ***************************
    // ****************************************************************

    public GameObject singlePlayButton;
    public GameObject scoresButton;
    public GameObject armoryButton;
    public GameObject groupButtonLeftButton;
    public GameObject connectFbButton;

    private void OnEnable()
    {
        TweenFrom(singlePlayButton, () => new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             singlePlayButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            singlePlayButton.transform.position.y));
        TweenFrom(scoresButton, new Vector3(5000f, scoresButton.transform.position.y));
        TweenFrom(armoryButton, () => new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             armoryButton.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            armoryButton.transform.position.y));
        TweenFrom(groupButtonLeftButton, () => new Vector3(
                groupButtonLeftButton.transform.position.x,
                (GetComponent<RectTransform>().rect.height / 2.0f +
                 groupButtonLeftButton.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
        TweenFrom(connectFbButton, () => new Vector3(
            connectFbButton.transform.position.x,
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
            "time", .5f));
    }
    private void TweenFrom(GameObject target, Vector3 startPos)
    {
        iTween.MoveFrom(target, iTween.Hash("position", startPos, "easeType", iTween.EaseType.easeInBack, 
            "time", .5f));
    }
}
