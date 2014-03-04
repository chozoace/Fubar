using UnityEngine;
using System.Collections;

public class FleeEnemyAI : MonoBehaviour 
{
	public int health;
	public int speed = 5;
	public float deathtime = 2f;
	
	bool playerInSight, canFlee, running;
	int maxDistance;
	GameObject player;
	
	int facing = 1;
	public float detectionRange = 5f;
	// Use this for initialization
	void Start () 
	{
		canFlee = true;
		running = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if(this.transform.position.x - player.transform.position.x < detectionRange)
		{
			Debug.Log ("detected");
			playerInSight = true;			
		}
		if(canFlee)
		{
			if(playerInSight)
			{
				flee();
				canFlee = false;
				running = true;
			}
		}
		if(running)
			this.rigidbody2D.velocity = new Vector2(speed, 0);
	}
	
	void flee()
	{
		//this.rigidbody2D.velocity = new Vector2(speed, 0);
		Invoke ("destroySelf", deathtime);
	}
	
	public void takeDamage(int theDamage)
	{
		health -= theDamage;
		Debug.Log (health);
		
		if(health <= 0)
			Destroy (gameObject);
	}
	
	void destroySelf()
	{
		GameObject.Destroy(this.gameObject);
	}
}
