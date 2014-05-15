using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : Gun
{
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] GameObject flashPrefab;
	[SerializeField] GameObject upFlashPrefab;
	[SerializeField] GameObject downFlashPrefab;
	
	[SerializeField] GameObject upLeftFlashPrefab;
	[SerializeField] GameObject downLeftFlashPrefab;
	[SerializeField] GameObject leftFlashPrefab;
	
	public Pistol()
	{
	
	}
	
	public Pistol(GameObject theObject):base(theObject)
	{
		
	}
	
	public override bool shoot(int facing, int yDirection = 0)
	{		
		SoundEffectHelper.Instance.MakeSoundEffect(SoundEffectHelper.SoundEffects.MachineGun);

		GameObject bullet;
		GameObject flash;
		//create bullet
		if(facing == 0)
		{
			if(yDirection == 0)
			{
				flash = (GameObject)(Instantiate (flashPrefab, new Vector2(myObject.transform.position.x + .3f, myObject.transform.position.y + .06f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing,yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
			}
			else if(yDirection == 1)
			{
				flash = (GameObject)(Instantiate (upFlashPrefab, new Vector2(myObject.transform.position.x + .32f, myObject.transform.position.y + .32f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing,yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y + .2f),Quaternion.identity));
			}
			else //ydirection = -1
			{
				flash = (GameObject)(Instantiate (downFlashPrefab, new Vector2(myObject.transform.position.x + .32f, myObject.transform.position.y - .33f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing,yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .48f),Quaternion.identity));
			}
			//bullet = (GameObject)(Instantiate(bulletPrefab, new Vector2(myObject.transform.position.x + .2f, myObject.transform.position.y - .1f),Quaternion.identity));
		}
		else
		{
			if(yDirection == 0)
			{
				flash = (GameObject)(Instantiate (leftFlashPrefab, new Vector2(myObject.transform.position.x - .3f, myObject.transform.position.y - 0f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));
			}
			else if(yDirection == 1)
			{
				flash = (GameObject)(Instantiate (upLeftFlashPrefab, new Vector2(myObject.transform.position.x - .32f, myObject.transform.position.y + .32f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y + .2f) ,Quaternion.identity));
			}
			else //ydirection = -1
			{
				flash = (GameObject)(Instantiate (downLeftFlashPrefab, new Vector2(myObject.transform.position.x - .33f, myObject.transform.position.y - .33f), Quaternion.identity));
				flash.GetComponent<BulletFlashScript>().Init(myObject, facing, yDirection);
				bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y -.48f) ,Quaternion.identity));
			}
			//bullet = (GameObject)(Instantiate(bulletPrefab,new Vector2(myObject.transform.position.x - .2f, myObject.transform.position.y - .1f) ,Quaternion.identity));
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
