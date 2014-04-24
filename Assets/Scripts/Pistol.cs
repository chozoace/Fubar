using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : Gun
{
	public GameObject bulletPrefab;
	public GameObject flashPrefab;
	
	public Pistol()
	{
	
	}
	
	public Pistol(GameObject theObject):base(theObject)
	{
		
	}
	
	public override bool shoot(int facing, int yDirection = 0)
	{		
		GameObject bullet;
		GameObject flash;
		//create bullet
		if(facing == 0)
		{
			flash = (GameObject)(Instantiate (flashPrefab, new Vector2(myObject.transform.position.x + .4f, myObject.transform.position.y + .06f), Quaternion.identity));
			flash.GetComponent<BulletFlashScript>().Init(myObject, facing);
			bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
		}
		else
		{
			flash = (GameObject)(Instantiate (flashPrefab, new Vector2(myObject.transform.position.x - .4f, myObject.transform.position.y - .06f), Quaternion.identity));
			flash.GetComponent<BulletFlashScript>().Init(myObject, facing);
			bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));
		}
		bullet.GetComponent<Bullet>().speed = bulletSpeed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().directionX = facing;
		bullet.GetComponent<Bullet>().directionY = yDirection;
		bullet.GetComponent<Bullet>().isPlayerBullet = true;
		
		return false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
