using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    UIUnit_Leaderboard prefab_UIUnit;
    [SerializeField]
    RectTransform rectTrans_Leaderboard;
    [SerializeField]
    TMP_InputField inputField_PlayerName;
    [SerializeField]
    string leaderboard_Name = "GameStandardModeScore";
    //For future database versions
    [SerializeField]
    int leaderboard_Version = 0;


    List<UIUnit_Leaderboard> uiUnitList = new List<UIUnit_Leaderboard>();


    // Start is called before the first frame update
    void Start()
    {
        Leaderboard.leaderboardEvent_LoginSuccess += OnLoginSuccess;
        Leaderboard.leaderboardEvent_LoadedOnline += OnLoadedOnline;
        Leaderboard.InitCheck();

        gameManager.event_BackToMenu += RefreshLeaderboard;
    }

	private void OnDestroy()
    {
        Leaderboard.leaderboardEvent_LoginSuccess -= OnLoginSuccess;
        Leaderboard.leaderboardEvent_LoadedOnline -= OnLoadedOnline;

        gameManager.event_BackToMenu -= RefreshLeaderboard;
    }


    void RefreshLeaderboard()
    {
        foreach(UIUnit_Leaderboard uiUnit in uiUnitList)
		{
            Destroy(uiUnit.gameObject);
        }
        uiUnitList.Clear();

        Leaderboard.RefreshLeaderboard(leaderboard_Name, leaderboard_Version);
    }

    void OnLoginSuccess()
    {
        inputField_PlayerName.text = Leaderboard.PLAYER_NAME;
        RefreshLeaderboard();
    }

    void OnLoadedOnline()
    {
        float intervalY = 50;
        //Display leaderboard
        for (int i = 0; i < Leaderboard.leaderboardEntry.Count; i++)
        {
            UIUnit_Leaderboard uiUnit = Instantiate(prefab_UIUnit, rectTrans_Leaderboard);
            uiUnit.Init(Leaderboard.leaderboardEntry[i]);
            uiUnitList.Add(uiUnit);
        }

        rectTrans_Leaderboard.sizeDelta = new Vector2(rectTrans_Leaderboard.sizeDelta.x, intervalY * Leaderboard.leaderboardEntry.Count);
    }

    public void Submit()
	{
        Leaderboard.UploadScore(gameManager.score, inputField_PlayerName.text, leaderboard_Name, leaderboard_Version);
	}

}
