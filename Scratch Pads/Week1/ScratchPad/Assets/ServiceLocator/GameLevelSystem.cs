using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelSystem : MonoBehaviour
{
    [SerializeField]
    GameObject prefab_bullet;

    float timer = 0;
    [SerializeField]
    float timerTotal = 2;
    // Start is called before the first frame update
    void Start()
    {
        //Begining of the game
        Service.Initlization();
        Debug.Log("EnemySystem - max enemy number: " + Service.EnemySystemInGame.maxEnemies);
        Debug.Log("GameLevel: " + Service.GameControllerInSystem.gameLevel);
        Debug.Log("PitchRange: x: " + Service.AudioInGame.pitchRange.x + " y: " + Service.AudioInGame.pitchRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timerTotal)
		{
            timer = 0;
            GameObject bullet = Instantiate(prefab_bullet, transform);
            Service.BulletManagerInGame.Spawn(transform.position, bullet);
        }
        else
		{
            timer += Time.deltaTime;
		}
    }
}
