using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDB: ScriptableObject
{
	[Header("***Prefab List***")]
	[SerializeField]
	GameObject[] prefabs;

	[SerializeField]
	int gameSettingValue1;

	[SerializeField]
	bool godModeEnabled;
}
