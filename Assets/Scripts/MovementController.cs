using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
	public KeyCode aimUp, moveLeft, moveRight, aimDown, jump, shoot, restart;
	bool canRightMove, canLeftMove, isAimUp, isAimDown;
	int yDirection;
	bool grounded = false;
	static MovementController instance;
	
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
	bool controlsLocked;
	
	Animator anim;

	private bool isFacingRight;
	bool dead;

	void Start () 
	{
		instance = this;
		myGun.identifyOwner(this.gameObject);
		anim = GetComponent<Animator>();
		isFacingRight = true;
		dead = false;
	}
	
	public static MovementController Instance()
	{
		return instance;
	}
	
	public int getFacing()
	{
		return facing;
	}
	
	void FixedUpdate () 
	{
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool("Ground", grounded);
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			
			float move = Input.GetAxis ("Horizontal");
			
			anim.SetFloat ("Speed", move);
		}
	}
	
	public void death()
	{
		dead = true;
	}
	
	public void lockControls()
	{
		GameController.Instance().controlsLocked = true;
		
		
	}
	
	public void unlockControls()
	{
		GameController.Instance().controlsLocked = false;
	}
	
	void CheckKeysDown()
	{
		if(Input.GetKeyDown (restart))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		else if(Input.GetKey (aimUp) && this.rigidbody2D.velocity.x == 0 && this.rigidbody2D.velocity.y == 0)
		{
			isAimUp = true;
			yDirection = 1;
			anim.SetBool("AimUp", isAimUp);
		}
		if(Input.GetKey (aimDown) && this.rigidbody2D.velocity.x == 0 && this.rigidbody2D.velocity.y == 0)
		{
			isAimDown = true;
			yDirection = -1;
			anim.SetBool("AimDown", isAimDown);
		}
		if(Input.GetKeyDown (moveRight))
		{
			canRightMove = true;
			Debug.Log ("Pressing down right");
			if(!canLeftMove)
			{
				facing = 0;
				isFacingRight = true;
				anim.SetBool("isFacingRight", isFacingRight);
			}
		}
		if(Input.GetKeyDown (moveLeft))
		{
			canLeftMove = true;
			if(!canRightMove)
			{
				facing = 1;
				isFacingRight = false;
				anim.SetBool("isFacingRight", isFacingRight);
			}
		}
		
		if(Input.GetKeyDown (shoot))
		{			
			Debug.Log ("player shoot");
			if(canShoot)
			{
				canShoot = myGun.shoot(facing, yDirection);
			}
		}
		//Debug.Log(facing);
	}
	
	void CheckKeysUp()
	{
		if(Input.GetKeyUp (aimUp))
		{
			isAimUp = false;
			anim.SetBool("AimUp", isAimUp);
		}
		if(Input.GetKeyUp (aimDown))
		{
			isAimDown = false;
			anim.SetBool("AimDown", isAimDown);
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
		if(canRightMove && canLeftMove)
		{
			v.x = 0;
			rigidbody2D.velocity = v;
		}
		else if(canRightMove)
		{
			facing = 0;		
			isFacingRight = true;
			anim.SetBool("isFacingRight", isFacingRight);
			v.x = speed;
			rigidbody2D.velocity = v;
		}
		else if(canLeftMove)
		{
			facing = 1;
			isFacingRight = false;
			anim.SetBool("isFacingRight", isFacingRight);
			v.x = speed * -1;
			rigidbody2D.velocity = v;
		}
	}
	
	public void StopAnim()
	{
		
	}
	
	void Update()
	{
		controlsLocked = GameController.Instance().isControlLocked();
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			if(!controlsLocked)
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
			else if (controlsLocked)
			{
				Vector2 v = new Vector2(0, rigidbody2D.velocity.y);
				rigidbody2D.velocity = v;
				canLeftMove = false;
				canRightMove = false;
			}
			if(this.rigidbody2D.velocity.x != 0 || this.rigidbody2D.velocity.y != 0)
			{
				isAimUp = false;
				isAimDown = false;
			}
			else
			{

			}
		}
	}
}
