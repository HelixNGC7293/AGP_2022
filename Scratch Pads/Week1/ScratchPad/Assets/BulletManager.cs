using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    List<GameObject> bullets = new List<GameObject>();

    public void Spawn(Vector3 positionOfSpawn, GameObject bullet)
	{
        bullet.transform.position = positionOfSpawn;
        bullets.Add(bullet);

    }

    public void UpdateManually(float deltaTime)
    {
        foreach(GameObject bullet in bullets)
		{
            Vector3 speedDirection = new Vector3(0, 0.1f * deltaTime, 0);
            bullet.transform.position += speedDirection;
		}
    }

    public void Destroy()
	{
        
	}
}
