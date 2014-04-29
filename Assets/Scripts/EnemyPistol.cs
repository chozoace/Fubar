using UnityEngine;
using System.Collections;

public class EnemyPistol : Gun
{
	public GameObject bulletPrefab;
	[SerializeField] GameObject flashPrefab;
	[SerializeField] GameObject upFlashPrefab;
	[SerializeField] GameObject downFlashPrefab;
	
	public EnemyPistol()
	{
		
	}
	
	public EnemyPistol(GameObject theObject):base(theObject)
	{
		
	}
	
	public override bool shoot(int facing, int yDirection = 0)
	{		
		GameObject bullet;
		GameObject flash;
		//Debug.Log("in enemy shoot " + yDirection);
		if (facing == 0)
		{	
			//.2, .06f
			if(yDirection == 0)
			{
				flash = (GameObject)(Instantiate (flashPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y + .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
			}
			else if(yDirection == 1)
			{
				flash = (GameObject)(Instantiate (upFlashPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y + .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
			}
			else if(yDirection == -1)
			{
				flash = (GameObject)(Instantiate (downFlashPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y + .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
			}
			bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
		}
		else
		{
			if(yDirection == 0)
			{
				flash = (GameObject)(Instantiate (flashPrefab, new Vector2(myObject.transform.position.x - .4f, myObject.transform.position.y - .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));;
			}
			else if(yDirection == 1)
			{
				flash = (GameObject)(Instantiate (upFlashPrefab, new Vector2(myObject.transform.position.x - .4f, myObject.transform.position.y - .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));;
			}
			else if(yDirection == -1)
			{
				flash = (GameObject)(Instantiate (downFlashPrefab, new Vector2(myObject.transform.position.x - .4f, myObject.transform.position.y - .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				//bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));;
			}
			bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));;
		}
		bullet.GetComponent<Bullet>().speed = bulletSpeed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().directionX = facing;
		bullet.GetComponent<Bullet>().directionY = yDirection;
		bullet.GetComponent<Bullet>().isPlayerBullet = false;
		
		return false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
