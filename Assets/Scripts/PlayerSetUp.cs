using UnityEngine;
using System.Collections;

public class PlayerSetUp : MonoBehaviour {
	public int health;
	Vector2 checkPointMarker;
	int currentCheckPoint;
	// Use this for initialization
	void Start () 
	{
		GameObject checkpoint = GameObject.FindGameObjectWithTag("checkpoint");
		checkPointMarker = checkpoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.gameObject.transform.position.x >= checkPointMarker.x)
		{
			currentCheckPoint = 2;
		}
		else 
			currentCheckPoint = 1;
		
		if(health <= 0)
		{
			GameController.RestartLevel(this.transform.position, currentCheckPoint);
			GameObject.Destroy(this.gameObject);
		}
		
	}
}
