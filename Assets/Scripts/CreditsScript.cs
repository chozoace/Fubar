using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour 
{
	[SerializeField] KeyCode closeGame;
	
	void Start ()
	{
	
	}
	
	void Update () 
	{
		if(Input.GetKeyDown (closeGame))
		{
			Debug.Log ("pressed");
			Application.Quit();
		}
	}
}
