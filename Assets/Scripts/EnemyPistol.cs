using UnityEngine;
using System.Collections;

public class EnemyPistol : Gun
{
	public GameObject bulletPrefab;
	
	public EnemyPistol()
	{
		
	}
	
	public EnemyPistol(GameObject theObject):base(theObject)
	{
		
	}
	
	public override bool shoot(int facing, int yDirection = 0)
	{		
		Debug.Log("in enemy shoot " + yDirection);
		GameObject bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x - .05f, myObject.transform.position.y), Quaternion.identity));
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
