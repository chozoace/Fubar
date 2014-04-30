using UnityEngine;
using System.Collections;

public class BulletFlashScript : MonoBehaviour
{
	Animator anim;
	public int facing;
	int prevFacing;
	bool playerReferenced;
	public GameObject player;
	Vector3 currentScale;
	int myYdirection;
	
	void Awake()
	{
		player = null;
		Invoke ("DestroySelf", .2f);
		anim = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
		facing = 3;
		playerReferenced = false;
	}
	
	void Start ()
	{
		//Debug.Log ("in start");
		/*player = null;
		Invoke ("DestroySelf", .2f);
		anim = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
		facing = 3;
		playerReferenced = false;*/
	}
	
	public void Init(GameObject playerRef, int facingRef, int yDirection)
	{
		//Debug.Log ("at player ref function");
		myYdirection = yDirection;
		player = playerRef;
		facing = facingRef;
		if(facing == -1)
		{
			this.transform.position = new Vector2(player.transform.position.x - .4f, player.transform.position.y + .06f);
			currentScale = transform.localScale;
			currentScale.x *= -1;
			transform.localScale = currentScale;
		}
		playerReferenced = true;
	}
	
	void DestroySelf()
	{
		GameObject.Destroy(this.gameObject);
	}
	
	void Update () 
	{
		//find a way to fix the weird flip before bullet flash appears. Bullet flash gets called from pistol or enemy
		//pistol
		if(playerReferenced == true)
		{
			//Debug.Log ("Current Scale:" + currentScale.x);
			//Debug.Log ("facing: " + facing);
			//Debug.Log ("PrevFacing: " + facing);
			//player = GameObject.FindGameObjectWithTag("Player");
			prevFacing = facing;
			//Debug.Log (player.tag);
			if(player.tag == "Player")
				facing = player.GetComponent<MovementController>().getFacing();
			else if (player.tag == "Enemy")
				facing = player.GetComponent<StillEnemyAI>().getFacing();
		
			
			if(player != null)
			{
				if(facing == 0)
				{
					if(myYdirection == 0)
					{
						//Debug.Log("in right shoot");
						if(player.tag == "Player")
							this.transform.position = new Vector2(player.transform.position.x + .4f, player.transform.position.y + .06f);
						else if(player.tag == "Enemy")
							this.transform.position = new Vector2(player.transform.position.x + .2f, player.transform.position.y + .06f);
					}
					else if(myYdirection == 1)
					{
						//up
						if(player.tag == "Player")
						{
							this.transform.position = new Vector2(player.transform.position.x + .23f, player.transform.position.y + .37f);
						}
						else if(player.tag == "Enemy")
						{
							this.transform.position = new Vector2(player.transform.position.x + .23f, player.transform.position.y + .37f);
						}
					}
					else if(myYdirection == -1)
					{
						//down
						if(player.tag == "Player")
						{
							this.transform.position = new Vector2(player.transform.position.x + .23f, player.transform.position.y - .3f);
						}
						else if(player.tag == "Enemy")
						{
							this.transform.position = new Vector2(player.transform.position.x + .23f, player.transform.position.y - .25f);
							
						}
					}
				}
				else if(facing == 1)
				{
					if(myYdirection == 0)
					{
						//Debug.Log ("In left shoot");
						this.transform.position = new Vector2(player.transform.position.x - .4f, player.transform.position.y + .06f);
						if(currentScale.x != -1)
						{
							currentScale = transform.localScale;
							currentScale.x *= -1;
							transform.localScale = currentScale;
						}
					}
					else if(myYdirection == 1)
					{
						//up
						if(player.tag == "Player")
						{
							this.transform.position = new Vector2(player.transform.position.x - .23f, player.transform.position.y + .37f);
							
						}
						else if(player.tag == "Enemy")
						{
							this.transform.position = new Vector2(player.transform.position.x - .23f, player.transform.position.y + .37f);
							
						}
						if(currentScale.x != -1)
						{
							currentScale = transform.localScale;
							currentScale.x *= -1;
							transform.localScale = currentScale;
						}
					}
					else if(myYdirection == -1)
					{
						//down
						if(player.tag == "Player")
						{
							this.transform.position = new Vector2(player.transform.position.x - .23f, player.transform.position.y - .3f);
							
						}
						else if(player.tag == "Enemy")
						{
							this.transform.position = new Vector2(player.transform.position.x - .23f, player.transform.position.y - .25f);
							
						}
						if(currentScale.x != -1)
						{
							currentScale = transform.localScale;
							currentScale.x *= -1;
							transform.localScale = currentScale;
						}
					}
				}
			}
		}
	}
}
