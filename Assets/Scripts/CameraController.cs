using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 

{
	public KeyCode moveLeft, moveRight;
	bool canLeft, canRight;
	GameObject player;
	float speed = 5;
	
	void CheckKeysUp()
	{
		if(Input.GetKeyUp (moveLeft))
		{
			canLeft = false;
		}
		if(Input.GetKeyUp (moveRight))
		{
			canRight = false;
		}
		if(!canRight && !canLeft)
		{
			rigidbody2D.velocity = Vector2.zero;
		}
	}
	
	void CheckKeysDown()
	{
		if(Input.GetKeyDown (moveLeft))
		{
			canLeft = true;
		}
		if(Input.GetKeyDown (moveRight))
		{
			canRight = true;
		}
	}
	
	void UpdateMovement()
	{
		if(canLeft)
		{
			Debug.Log("going Left");
			this.rigidbody2D.velocity = new Vector2(speed * -1, 0);
		}
		if(canRight)
		{
			this.rigidbody2D.velocity = new Vector2(speed, 0);
			Debug.Log("going right");
			Debug.Log (this.rigidbody2D.velocity.x);
		}
		
	}
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		transform.position = new Vector3((float)(player.transform.position.x), 1.8f, -10);
		//CheckKeysUp();
		//CheckKeysDown();
		//UpdateMovement();
	}
}
