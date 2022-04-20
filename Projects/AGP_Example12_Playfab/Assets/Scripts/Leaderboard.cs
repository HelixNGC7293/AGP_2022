using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Leaderboard {
	
	public static string PLAYER_NAME = "Player Unknown";
	//public static int[] leaderboard_Score = new int[20];
	//public static string[] leaderboard_Name = new string[20];

	//static string[] leaderboard_NameDefault = {"DGSpitzer", "Funky Zombie", "IDDQD", "Player1", "Neighbour Wang", "Saber", "Knuckles", "Waifu", "Happy Tree", "Here Lays", "Tycoonpunk", "GG", "Bot2018", "IMBA", "Buggy", "Serious Jack", "Gu", "John Derek", "unnamed", "Null" };

	public static List<PlayerLeaderboardEntry> leaderboardEntry;


	public delegate void LeaderboardEvent();
	public static LeaderboardEvent leaderboardEvent_LoginSuccess;
	public static LeaderboardEvent leaderboardEvent_LoadedOnline;
	public static LeaderboardEvent leaderboardEvent_UploadedScore;

	// Use this for initialization
	public static void InitCheck ()
	{
		if (!PlayFabClientAPI.IsClientLoggedIn())
		{
			string titleID = "FF5BF";
			if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
			{
				PlayFabSettings.TitleId = titleID;
			}
			var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
			PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

			//Find the default name for this player

			//Save new player name
			PLAYER_NAME = "Player Unknown";
		}
	}
	static void OnFailure(PlayFabError error)
	{
		Debug.LogError("This is a leaderboard error!");
		Debug.LogError(error.GenerateErrorReport());
	}

	static void OnLoginFailure(PlayFabError error)
	{
		Debug.LogError("Can't login");
		Debug.LogError(error.GenerateErrorReport());
	}

	// Update is called once per frame
	public static void OnLoginSuccess(LoginResult result)
	{
		Debug.Log("Login Succeed!");
		var request = new GetAccountInfoRequest();

		//Load new player name
		PLAYER_NAME = PlayerPrefs.GetString("PLAYER_NAME", "Player Unknown");
		//PlayFabClientAPI.GetAccountInfo(request, resultLeaderboard => { OnGetAccountInfo(resultLeaderboard); }, OnFailure);

		leaderboardEvent_LoginSuccess?.Invoke();
	}

	//static void OnGetAccountInfo(GetAccountInfoResult result)
	//{
	//	PLAYER_NAME = result.AccountInfo.TitleInfo.DisplayName;
	//	Debug.Log(result.AccountInfo.TitleInfo.DisplayName);
	//}

	public static void RefreshLeaderboard(string statisticName, int version = -1)
	{
		if (statisticName != "")
		{
			GetLeaderboardRequest request = new GetLeaderboardRequest();
			request.StatisticName = statisticName;
			request.MaxResultsCount = 100;
			if (version != -1)
			{
				request.Version = version;
			}
			PlayFabClientAPI.GetLeaderboard(request, resultLeaderboard => { OnLeaderboadLoadedOnline(resultLeaderboard, statisticName); }, OnFailure);
		}
	}

	static void OnLeaderboadLoadedOnline(GetLeaderboardResult result, string statisticName)
	{
		Debug.Log("Got leaderboard data! Database name: \"" + statisticName + "\"");
		//You can save leaderboards data from here
		leaderboardEntry = result.Leaderboard;
		leaderboardEvent_LoadedOnline?.Invoke();
	}

	
	public static void UploadScore (int score, string currentPlayerName, string statisticName, int version = -1) {

		//Build up score unit
		List<StatisticUpdate> scoreUnits = new List<StatisticUpdate>();
		StatisticUpdate scoreUnit = new StatisticUpdate();
		//Get target database name
		scoreUnit.StatisticName = statisticName;
		//scoreUnit.Version = 0;
		scoreUnit.Value = score;
		//Upload to this leaderboard version
		if (version != -1)
		{
			scoreUnit.Version = (uint)version;
		}
		scoreUnits.Add(scoreUnit);
		UpdatePlayerStatisticsRequest requestUpdate = new UpdatePlayerStatisticsRequest();
		requestUpdate.Statistics = scoreUnits;



		if (currentPlayerName.Length < 3)
		{
			currentPlayerName += "   ";
		}
		UpdateUserTitleDisplayNameRequest request_NewDisplayName = new UpdateUserTitleDisplayNameRequest();
		request_NewDisplayName.DisplayName = currentPlayerName;
		//Save new player name
		PlayFabClientAPI.UpdateUserTitleDisplayName(request_NewDisplayName, result => {

			//Save new player name
			PLAYER_NAME = currentPlayerName;
			PlayerPrefs.SetString("PLAYER_NAME", result.DisplayName);

			PlayFabClientAPI.UpdatePlayerStatistics(requestUpdate, result => {
				Debug.Log("Score Uploaded");
				leaderboardEvent_UploadedScore?.Invoke();
			}, error => { 
				Debug.Log(error.ErrorMessage);
			});

		}, OnFailure);

		//PlayFabClientAPI.UpdateUserTitleDisplayName(request_NewDisplayName, OnDisplayNameChanged, OnFailure);


		

	}

	//static void OnDisplayNameChanged(UpdateUserTitleDisplayNameResult result)
	//{
	//	//Save new player name
	//	PlayerPrefs.SetString("PLAYER_NAME", result.DisplayName); 
	//}
}
