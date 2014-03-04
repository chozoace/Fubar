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

	int facing = 1;
	public float detectionRange = 5f;
	public Gun myGun;
	// Use this for initialization
	void Start () 
	{
		myGun.identifyOwner(this.gameObject);	
	}
	
	// Update is called once per frame
	void Update () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if(this.transform.position.x - player.transform.position.x < detectionRange)
		{
			playerInSight = true;			
		}
		//detect enemy
		//begin shooting at enemy, straight shots
		if(playerInSight)
		{
			if(canShoot)
			{
				canShoot = myGun.shoot (facing);
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
