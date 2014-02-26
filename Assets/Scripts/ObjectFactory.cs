using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour {
	protected static ObjectFactory instance;
	
	public GameObject bulletPrefab;
	public GameObject enemyPrefab;
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () 
	{
		//instance = this;
	}
	
	public static void CreatePlayer(float xPos, float yPos)
	{
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
