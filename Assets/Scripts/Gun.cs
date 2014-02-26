using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	protected GameObject myObject;
	public int fireRate;
	public int bulletSpeed;
	public int damage;
	public List<Bullet> bulletList = new List<Bullet>();
	
	//public static int getSpeed { get { return bulletSpeed;}}
	//public static int getDamage{ get { return damage;}}
	
	public Gun()
	{
		
	}
	
	public Gun(GameObject theObject)
	{
		myObject = theObject;
	}
	
	public virtual void identifyOwner(GameObject theObject)
	{
		myObject = theObject;
	}
	
	public virtual bool shoot(int facing, int yDirection = 0)
	{
		Debug.Log ("shooting");

		return false;
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
