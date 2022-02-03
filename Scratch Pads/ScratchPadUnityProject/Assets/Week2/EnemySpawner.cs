using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    BasicEventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        eventManager.OnGameStart += EnemyStartSpawn;
    }

	private void OnDestroy()
    {
        eventManager.OnGameStart -= EnemyStartSpawn;
    }

	// Update is called once per frame
	void EnemyStartSpawn()
    {
        Debug.Log("Enemy Start Spawning");
    }
}
