using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float speed;
	public int damage;
	public int yAdjust=5;
	//bool canUpdate = true;
	bool vsEnemy;
	public bool exists;
	public bool isPlayerBullet;
	public int directionX = 0; //0 = right, 1 = left
	public int directionY = 0;
	float ysizeDeath = .0001f;
	float ycenterDeath = -.3f;
	bool isVisible;
	
	// Use this for initialization
	void Start () 
	{		
		Invoke("destroyBullet", 7f);
		this.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
	}
	
	void destroyBullet()
	{
		GameObject.Destroy(this.gameObject);
	}
	
	public void Initialize(float theSpeed)
	{
		speed = theSpeed;
	}
	
	void Update () 
	{
		if(isPlayerBullet == false)
			yAdjust = 1;
		if(directionX == 0 && directionY == 0)
			rigidbody2D.velocity = new Vector2(speed, 0);
		else if(directionX == 1 && directionY == 0)
			rigidbody2D.velocity = new Vector2(speed * -1, 0);
		else if(directionX == 0 && directionY == 1)
			rigidbody2D.velocity = new Vector2(speed, speed - yAdjust);
		else if(directionX == 1 && directionY == 1)
			rigidbody2D.velocity = new Vector2(speed * -1, speed - yAdjust);
		else if(directionX == 0 && directionY == -1)
			rigidbody2D.velocity = new Vector2(speed, (speed - yAdjust) * -1);
		else if(directionX == 1 && directionY == -1)
		{
			rigidbody2D.velocity = new Vector2(speed * -1, (speed - yAdjust) * -1);
			//Debug.Log("shot at angle");
		}
		isVisible = renderer.isVisible;
		if(!isVisible)
		{
			GameObject.Destroy(this.gameObject);
		}
		//if(isPlayerBullet == true)
			//Debug.Log("bullet update: " + directionY + " " + directionX);
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag != "Player" && isPlayerBullet && collision.GetComponent<BoxCollider2D>().enabled)
		{
			//Debug.Log ("hitting");
			if(collision.tag == "Enemy")
			{
				collision.gameObject.GetComponent<StillEnemyAI>().takeDamage(damage);
				Vector2 newSize = new Vector2(collision.GetComponent<BoxCollider2D>().size.x, .0001f);
				collision.GetComponent<BoxCollider2D>().size = newSize;
				Vector2 newCenter = new Vector2(collision.GetComponent<BoxCollider2D>().size.x, -.25f);
				collision.GetComponent<BoxCollider2D>().center = newCenter;
			}
			else if(collision.tag == "FleeEnemy")
			{
				Debug.Log("hitting enemy");
				//must fix this
				collision.gameObject.GetComponent<FleeEnemyAI>().takeDamage(damage);
				Vector2 newSize = new Vector2(collision.GetComponent<BoxCollider2D>().size.x, .0001f);
				collision.GetComponent<BoxCollider2D>().size = newSize;
				Vector2 newCenter = new Vector2(collision.GetComponent<BoxCollider2D>().size.x, -.25f);
				collision.GetComponent<BoxCollider2D>().center = newCenter;
			}
			Destroy(this.gameObject);
		}
		else if(!isPlayerBullet && collision.gameObject.tag != "Enemy")
		{
			if(collision.gameObject.tag == "Player")
			{
				collision.gameObject.GetComponent<PlayerSetUp>().takeDamage(damage);
				//Debug.Log ("player hit");
			}
			Destroy(this.gameObject);	
		}
	}
	
}
