using UnityEngine;
using System.Collections;

public class StillEnemyAI : MonoBehaviour 
{
	public int health;
	public int speed;
	public float fireRate = 2f;
	
	bool playerInSight, canShoot = true;
	int maxDistance;
	GameObject player;
	Animator anim;

	int facing = 1;//1 = left, 0 = right
	int yDirection;
	public float detectionRange = 5f;
	public Gun myGun;
	// Use this for initialization
	void Start () 
	{
		myGun.identifyOwner(this.gameObject);
		anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		//for facing
		if(this.transform.position.x - player.transform.position.x < 0)
		{
			facing = 0;
			anim.SetInteger("facing", facing);
		}
		else
		{
			facing = 1;
			anim.SetInteger("facing", facing);
		}
		//for distance check
		if(this.transform.position.x - player.transform.position.x < detectionRange && facing == 1)
		{
			playerInSight = true;			
		}
		else if(player.transform.position.x - this.transform.position.x < detectionRange && facing == 0)
		{
			playerInSight = true;
		}
		else 
			playerInSight = false;
		//for y angled shooting
		if(this.transform.position.y - player.transform.position.y < -.3)
			yDirection = 1;
		else if(this.transform.position.y - player.transform.position.y > .3)
			yDirection = -1;
		else 
			yDirection = 0;
		//detect enemy
		//begin shooting at enemy, straight shots
		if(playerInSight)
		{
			if(canShoot)
			{
				canShoot = myGun.shoot (facing, yDirection);
				canShoot = false;
				Invoke("reload", fireRate);
			}
		}
	}
	
	void reload()
	{
		canShoot = true;
	}
	
	public void takeDamage(int theDamage)
	{
		health -= theDamage;
		Debug.Log (health);
		
		if(health <= 0)
			Destroy (gameObject);
	}
}
