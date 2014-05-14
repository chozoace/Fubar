using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			GameController.Instance().EndGame(this.transform.position.x);
			
		}
	}	
		
	void Update () 
	{
		
	}
}
