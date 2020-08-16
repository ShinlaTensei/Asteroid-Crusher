using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class LeaderboardPage : MonoBehaviour
{
    [SerializeField] private GameObject itemRank;

    [SerializeField] private List<Sprite> rankSprite = new List<Sprite>(4);

    [SerializeField] private GameObject content;
    public void Active()
    {
        content.transform.DetachChildren();
        if (GameManager.Instance.playfabController.IsLogin)
        {
            GameManager.Instance.playfabController.GetLeaderboard((leaderboard) =>
            {
                int length = leaderboard.Count;
                for (int i = 0; i < length; ++i)
                {
                    var rank = Instantiate(itemRank);
                    Sprite sprite1 = (i < 3) ? rankSprite[i] : rankSprite[3];
                    rank.GetComponent<ItemRank>().Init(sprite1, (i + 1).ToString(), leaderboard[i].DisplayName, 
                        leaderboard[i].StatValue.ToString());
                    rank.transform.SetParent(content.transform, false);
                }
            });
        }
        else
        {
            InitLeaderboardOffline();
        }
    }

    private void InitLeaderboardOffline()
    {
        var rank = Instantiate(itemRank);
        string score = PlayerManager.Instance.UserData.score.ToString();
        rank.GetComponent<ItemRank>().Init(rankSprite[0], "1", "You", 
            score);
        rank.transform.SetParent(content.transform, false);
    }

    public void Close(GameObject toOpen)
    {
        gameObject.SetActive(false);
        toOpen.SetActive(true);
    }
}
