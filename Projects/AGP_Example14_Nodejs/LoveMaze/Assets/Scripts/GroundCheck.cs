using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	
	public GameObject isGround;

	void OnCollisionEnter(Collision other)
	{
		//if(other.gameObject.tag == "Floor")
		isGround = other.gameObject;
	}
	
	void OnCollisionExit(Collision other)
	{
		if(isGround == other.gameObject)
			isGround = null;
	}
}
