using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadedListener : MonoBehaviour
{
	private void Start()
	{
		SceneManager.sceneLoaded += HandleSceneLoaded;
	}

	private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		string logMessage = string.Format("Scene {0} loaded in mode {1}", arg0, arg1);
		Debug.Log(logMessage);
	}
}
