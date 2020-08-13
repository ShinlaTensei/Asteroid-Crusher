using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject homePage;

    [SerializeField] private GameObject selectShipPage;

    [SerializeField] private GameObject leaderboardPage;

    [SerializeField] private GameObject settingPage;

    private void OnEnable()
    {
        homePage.SetActive(true);
        selectShipPage.SetActive(false);
        leaderboardPage.SetActive(false);
        settingPage.SetActive(false);
    }
}
