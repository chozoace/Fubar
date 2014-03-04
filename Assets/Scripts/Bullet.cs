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
	
	// Use this for initialization
	void Start () 
	{		
		Invoke("destroyBullet", 3f);
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
			rigidbody2D.velocity = new Vector2(speed * -1, (speed - yAdjust) * -1);
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag != "Player" && isPlayerBullet)
		{
			Debug.Log ("hitting");
			if(collision.tag == "Enemy")
			{
				Debug.Log("hitting enemy");
				collision.gameObject.GetComponent<StillEnemyAI>().takeDamage(damage);
			}
			else if(collision.tag == "FleeEnemy")
			{
				Debug.Log("hitting enemy");
				//must fix this
				collision.gameObject.GetComponent<FleeEnemyAI>().takeDamage(damage);
			}
			Destroy(this.gameObject);
		}
		else if(!isPlayerBullet && collision.gameObject.tag != "Enemy")
		{
			if(collision.gameObject.tag == "Player")
			{
				collision.gameObject.GetComponent<PlayerSetUp>().health -= damage;
			}
			Debug.Log ("player hit");
			Destroy(this.gameObject);	
		}
	}
	
	/*void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag != "Player" && isPlayerBullet)
		{
			Debug.Log("collided");
			if(collision.collider.tag == "Enemy")
			{
				Debug.Log("hitting enemy");
				collision.gameObject.GetComponent<StillEnemyAI>().takeDamage(damage);
			}
			else if(collision.collider.tag == "FleeEnemy")
			{
				Debug.Log("hitting enemy");
				//must fix this
				//collision.gameObject.GetCompenent<FleeEnemyAI>().takeDamage(damage);
			}
			Destroy(this.gameObject);
		}
		else if(collision.gameObject.tag == "Player" && !isPlayerBullet)
		{
			collision.gameObject.GetComponent<PlayerSetUp>().health -= damage;
			Destroy (this.gameObject);
		}
	}*/
	
}
