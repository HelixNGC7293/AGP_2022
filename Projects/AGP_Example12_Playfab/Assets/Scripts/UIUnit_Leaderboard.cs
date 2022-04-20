using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;

public class UIUnit_Leaderboard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tX_PlayerName;
    [SerializeField]
    TextMeshProUGUI tX_Score;
    // Start is called before the first frame update
    public void Init(PlayerLeaderboardEntry leaderboardEntry)
    {
        tX_PlayerName.text = leaderboardEntry.DisplayName;
        tX_Score.text = leaderboardEntry.StatValue.ToString();
    }
}
