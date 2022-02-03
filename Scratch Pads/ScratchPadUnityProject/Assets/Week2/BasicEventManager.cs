using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class BasicEventManager : MonoBehaviour
{
    public delegate void GameStart();
    public GameStart OnGameStart;

	private void OnDestroy()
	{
		if(OnGameStart != null)
		{
			OnGameStart = null;
		}
	}
}
