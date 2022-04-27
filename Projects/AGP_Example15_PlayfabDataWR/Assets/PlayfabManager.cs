using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
	[SerializeField]
	InputField iF_Data;
	[SerializeField]
	Text tX_Data;
    // Start is called before the first frame update
    void Start()
    {
		InitCheck();
	}
	public static void InitCheck()
	{
		if (!PlayFabClientAPI.IsClientLoggedIn())
		{
			string titleID = "";
			if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
			{
				PlayFabSettings.TitleId = titleID;
			}
			var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
			PlayFabClientAPI.LoginWithCustomID(request, result => { Debug.Log("Login Succeed"); }, result => { Debug.Log("Login Failed"); });
		}
	}

	// Update is called once per frame
	public void UploadData()
	{
		//Build up score unit
		UpdateUserDataRequest requestUpdate = new UpdateUserDataRequest();
		//Get target database name
		requestUpdate.Data = new Dictionary<string, string>();
		requestUpdate.Data.Add("AGPPlayerInputData", iF_Data.text);
		requestUpdate.Data.Add("GameTime", System.DateTime.UtcNow.ToString());

		requestUpdate.Permission = UserDataPermission.Public;

		PlayFabClientAPI.UpdateUserData(requestUpdate, result => { Debug.Log("Upload Succeed"); }, result => { Debug.Log("Upload Failed"); });
	}
	// Update is called once per frame
	public void DownloadData()
	{
		//Build up score unit
		GetUserDataRequest requestGet = new GetUserDataRequest();
		//Get target database name
		requestGet.Keys = new List<string>() { "AGPPlayerInputData", "GameTime" };

		PlayFabClientAPI.GetUserData(
			requestGet, 
			result => { 
				Debug.Log("Get Succeed");
				tX_Data.text = result.Data["GameTime"].Value + "s \n";
				tX_Data.text += "Saved Content: " + result.Data["AGPPlayerInputData"].Value;
			},
			result => {
				Debug.Log("Get Failed"); 
			}
		);
	}
}
