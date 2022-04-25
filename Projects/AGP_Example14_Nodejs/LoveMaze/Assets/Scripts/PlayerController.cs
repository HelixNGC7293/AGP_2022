using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool isMainPlayer = true;
	public Text textUI;

	private Vector3 prePosition_;
	private Quaternion preRotation_;
	
	private GameObject playerBody;
	private GroundCheck playerBodyGroundCheck;
	private Rigidbody playerRigidbody;

	public float speed = 500;

	void Start()
	{
		playerBody = GameObject.Find ("/" + gameObject.name + "/PlayerBody");
		playerBodyGroundCheck = playerBody.GetComponent<GroundCheck>();
		playerRigidbody = playerBody.GetComponent<Rigidbody> ();

		//After loading, create the Maze
		Application.ExternalCall ("socket.emit", "unityLoad");
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		float moveJump = 0.0f;

		//Jump
		if(playerBodyGroundCheck.isGround && Mathf.Abs(playerRigidbody.velocity.y) <= 0.15 && Input.GetKeyDown(KeyCode.Space))
		{
			//isGround_ = false;
			moveJump = 20.0f;
		}

		Vector3 movement = new Vector3 ();

		if (playerRigidbody.velocity.magnitude < 50) 
		{
			movement = new Vector3 (moveHorizontal, moveJump, moveVertical);
		}

		playerRigidbody.AddForce (movement * speed * Time.deltaTime);
	}

	void Update()
	{
		if (!isMainPlayer)
			return;
		EmitPosition ();
		EmitRotation ();
	}

	void LateUpdate()
	{
		if (playerBody.transform.position.y < -15.0f) {
			var pos = playerBody.transform.position;
			pos.y = -5.0f;
			playerBody.transform.position = pos;
		}
	}

	void EmitPosition()
	{
		var pos = playerBody.transform.position;
		if (pos != prePosition_) {
			prePosition_ = pos;
			Application.ExternalCall ("socket.emit", "move", pos.x, pos.y, pos.z);
		}
	}

	void EmitRotation()
	{
		var rot = playerBody.transform.rotation;
		if (rot != preRotation_) {
			preRotation_ = rot;
			Application.ExternalCall ("socket.emit", "rotate", rot.x, rot.y, rot.z, rot.w, Time.frameCount);
		}
	}

	void Talk(string message)
	{
		textUI.text = message;
		Application.ExternalCall ("socket.emit", "talk", message);
	}
}
