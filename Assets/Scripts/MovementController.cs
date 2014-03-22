using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
	public KeyCode aimUp, moveLeft, moveRight, aimDown, jump, shoot, restart;
	bool canRightMove, canLeftMove, isAimUp, isAimDown;
	int yDirection;
	bool grounded = false;
	
	public int health;
	//public GameObject player;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	
	public float speed = 5f;
	public float jumpForce = 700f;
	public Gun myGun;
 	int facing;
	
	bool canShoot = true;
	
	Animator anim;

	void Start () 
	{
		myGun.identifyOwner(this.gameObject);
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		float move = Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("Speed", move);
		
	}
	
	void CheckKeysDown()
	{
		if(Input.GetKeyDown (restart))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if(Input.GetKeyDown (aimUp))
		{
			isAimUp = true;
			yDirection = 1;
		}
		if(Input.GetKeyDown (aimDown))
		{
			isAimDown = true;
			yDirection = -1;
		}
		if(Input.GetKeyDown (moveRight))
		{
			canRightMove = true;
			facing = 0;
			anim.SetInteger("facing", facing);
		}
		if(Input.GetKeyDown (moveLeft))
		{
			canLeftMove = true;
			facing = 1;
			anim.SetInteger("facing", facing);
		}
		if(Input.GetKeyDown (shoot))
		{			
			Debug.Log ("player shoot");
			if(canShoot)
			{
				canShoot = myGun.shoot(facing, yDirection);
			}
		}
		//Debug.Log(canShoot);
	}
	
	void CheckKeysUp()
	{
		if(Input.GetKeyUp (aimUp))
		{
			isAimUp = false;
		}
		if(Input.GetKeyUp (aimDown))
		{
			isAimDown = false;
		}
		
		if(!isAimUp && !isAimDown)
			yDirection = 0;
			
		if(Input.GetKeyUp (moveRight))
		{
			canRightMove = false;
		}
		if(Input.GetKeyUp (moveLeft))
		{
			canLeftMove = false;
		}	
		if(Input.GetKeyUp (shoot))
		{
			canShoot = true;
		}
		if(!canLeftMove && !canRightMove)
		{
			Vector2 v = new Vector2();
			Vector2 currentVelocity = rigidbody2D.velocity;
			v.x = 0;
			v.y = currentVelocity.y;
			rigidbody2D.velocity = v;
			//state = Idle
		}
	}
	
	void UpdateMovement()
	{
		Vector2 v = new Vector2();
		Vector2 currentSpeed = rigidbody2D.velocity;
		v.y = currentSpeed.y;
		if(canRightMove)
		{
			v.x = speed;
			rigidbody2D.velocity = v;
		}
		if(canLeftMove)
		{
			v.x = speed * -1;
			rigidbody2D.velocity = v;
		}
	}
	
	void Update()
	{
		CheckKeysDown ();
		CheckKeysUp ();
		UpdateMovement ();
		
		if(grounded && Input.GetKeyDown(jump))
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
	}
}
