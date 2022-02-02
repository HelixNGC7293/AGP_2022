using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
	public static void Initlization()
	{
		EnemySystemInGame = new EnemySystem();
		BulletManagerInGame = new BulletManager();
	}

	public static GameController GameControllerInSystem;
	public static AudioController AudioInGame;
	public static EnemySystem EnemySystemInGame;
	public static BulletManager BulletManagerInGame;

	private void Update()
	{
		BulletManagerInGame.UpdateManually(Time.deltaTime);
	}
}

public class EnemySystem
{
	//Some features here
	public int maxEnemies = 10;
	public List<GameObject> enemiesGameObjects = new List<GameObject>();
	public void Update()
	{

	}

	public void Destroy()
	{

	}
}