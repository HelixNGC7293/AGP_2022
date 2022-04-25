using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkPlayerController : MonoBehaviour 
{
	public Text textUi;

	private GameObject playerBody;

	void Start()
	{
		playerBody = GameObject.Find ("/" + gameObject.name + "/PlayerBody");
	}

	public void Move(Vector3 position) 
	{
		if(playerBody)
		{
			playerBody.transform.position = position;
		}
	}
	
	public void Rotate(Quaternion rotation) 
	{
		if (playerBody) 
		{
			playerBody.transform.rotation = rotation;
		}
	}

	public void Talk(string message)
	{
		if(playerBody)
		{
			textUi.text = message;
		}
	}
}
