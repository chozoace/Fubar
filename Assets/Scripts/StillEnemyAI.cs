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

	int facing = 1;//1 = left, 0 = right //This guy
	int yDirection;
	public float detectionRange = 5f;
	public Gun myGun;

	private int _deathRightStateID;
	private int _deathLeftStateID;
	bool deathCounter = false;

	private bool _isDead;

	void Awake() {
		_deathRightStateID = Animator.StringToHash("Base Layer.EnemyDeathRight");
		_deathLeftStateID = Animator.StringToHash("Base Layer.EnemyDeathLeft");
	}
	// Use this for initialization
	void Start () 
	{
		facing = 1;
		myGun.identifyOwner(this.gameObject);
		anim = GetComponent<Animator>();
		_isDead = false;
	}
	
	public int getFacing()
	{
		return facing;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			if(_isDead == false) {
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
				{
					yDirection = 1;
					anim.SetBool("AimUp", true);
					anim.SetBool("AimDown", false);
				}
				else if(this.transform.position.y - player.transform.position.y > .3)
				{
					yDirection = -1;
					anim.SetBool("AimUp", false);
					anim.SetBool("AimDown", true);
				}
				else 
				{
					yDirection = 0;
					anim.SetBool("AimUp", false);
					anim.SetBool("AimDown", false);
				}
				//detect enemy
				//begin shooting at enemy, straight shots
				if(playerInSight)
				{
					if(canShoot)
					{
						canShoot = myGun.shoot (facing, yDirection);
						canShoot = false;
						Invoke("reload", fireRate);;
					}
				}
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
		
		if(health <= 0)
		{
			//Destroy (gameObject);
			EnemyDeath();
			player.GetComponent<ScoreUI>().adjustScore(100);
		}
	}

	public void EnemyDeath() {

		_isDead = true;
		anim.SetBool("isDead", _isDead);

		if(facing == 1 && !deathCounter)
		{ //Right
			anim.Play (_deathLeftStateID, 0, 0);
			deathCounter = true;
			//Debug.Log("started playing");
		}
		else if(facing == 0 && !deathCounter) 
		{
			anim.Play (_deathRightStateID, 0, 0);
			deathCounter = true;
		}
		
		/*if(anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).nameHash == _deathRightStateID)
		{
			Debug.Log("destroying");
			GameObject.Destroy(this.gameObject);
		}

		if(anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).nameHash == _deathLeftStateID)
		{
			Debug.Log("destroying");
			GameObject.Destroy(this.gameObject);
		}*/
		Invoke("DestroySelf", .7f); //FUCK IT
	}
	
	void DestroySelf()
	{
		GameObject.Destroy(this.gameObject);
	}
}
