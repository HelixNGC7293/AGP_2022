using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevelType {Menu, InGame, EndScreen}

[CreateAssetMenu(menuName = "Asset/Example Scriptable Object")]
public class SimpleScriptableObject : ScriptableObject
{
	public string playerName;
	public float playerHealth;
	public GameObject[] levelMonsters;
	public GameObject[] levelItems;
	public GameLevelType gameLevelType;

}
