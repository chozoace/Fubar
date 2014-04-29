using UnityEngine;
using System.Collections;

public class FleeEnemyAI : MonoBehaviour 
{
	public int health;
	public int speed = 3;
	public float deathtime = 2f;
	
	bool playerInSight, canFlee, running;
	int maxDistance;
	GameObject player;
	Animator anim;
	
	int facing = 1;
	public float detectionRange = 5f;
	// Use this for initialization
	void Start () 
	{
		anim = this.GetComponent<Animator>();
		canFlee = true;
		running = false;
	}
	
	// Update is called once per frame
	
	void FixedUpdate()
	{
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			float mySpeed = this.rigidbody2D.velocity.x;
			anim.SetFloat("Speed", mySpeed);
		}
	}
	void Update () 
	{
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			if(this.transform.position.x - player.transform.position.x < detectionRange)
			{
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
			float yVel = this.rigidbody2D.velocity.y;
			if(running)
			{
				if(yVel == 0)
				{
					//Debug.Log("here running");
					this.rigidbody2D.velocity = new Vector2(speed, this.rigidbody2D.velocity.y);
					//Debug.Log(this.rigidbody2D.velocity.x);	
				}			
			}
		}
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
		{
			Destroy (gameObject);
			player.GetComponent<ScoreUI>().adjustScore(-50);
		}
	}
	
	void destroySelf()
	{
		GameObject.Destroy(this.gameObject);
	}
}
