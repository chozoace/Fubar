using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : Gun
{
	public GameObject bulletPrefab;
	
	public Pistol()
	{
	
	}
	
	public Pistol(GameObject theObject):base(theObject)
	{
		
	}
	
	public override bool shoot(int facing, int yDirection = 0)
	{		
		GameObject bullet = (GameObject)(Instantiate(bulletPrefab, myObject.transform.position, Quaternion.identity));
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
