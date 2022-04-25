using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int dimensions = 49;
	public GameObject wallBlock;
	public GameObject playerBody;
	public float wallSize = 15;

	public GameObject[][] mazeWalls;

	public int spawning = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//2 frames later, set the position of player;
		if (spawning == 0) {
			spawning = 1;
		}
		else if (spawning == 1) {
			spawning = 2;
			var passageNum = (dimensions - 2) * 0.5f;
			int spawnPoints = Mathf.FloorToInt(Mathf.Round(passageNum) - 1);
			playerBody.transform.position = new Vector3 (wallSize * (1 + 2 * Random.Range (0, spawnPoints)), 0, wallSize * (1 + 2 * Random.Range (0, spawnPoints)));
		}
	}

	public void CreateMaze(bool[][] maze)
	{
		mazeWalls = new GameObject[maze.Length][];
		for (var i = 0; i < maze.Length; i++) 
		{
			mazeWalls[i] = new GameObject[maze[i].Length];
			for (var j = 0; j < maze[i].Length; j++) 
			{
				if(maze[i][j])
				{
					var wall = Instantiate (wallBlock) as GameObject;
					wall.transform.position = new Vector3(wallSize * i, -10.0f, wallSize * j);

					mazeWalls[i][j] = wall;
				}
			}
		}
	}
}
