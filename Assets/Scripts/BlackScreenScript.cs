using UnityEngine;
using System.Collections;

public class BlackScreenScript : MonoBehaviour {

	Color fadeColor = Color.black;
	int fadeCounter = 5;
	//static BlackScreenScript instance;
	// Use this for initialization
	void Start () {
		//instance = this;
	}
	
	public void fade(Color newAlpha)
	{
		//Debug.Log ("fade called Before:");
		//Debug.Log (this.GetComponent<SpriteRenderer>().color.a);
		//Debug.Log ("After");
		this.GetComponent<SpriteRenderer>().color = newAlpha;
		//Debug.Log (this.GetComponent<SpriteRenderer>().color.a);
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(this.GetComponent<SpriteRenderer>().color.a);
	}
}
