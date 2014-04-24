using UnityEngine;
using System.Collections;

public class BlackScreenScript : MonoBehaviour {

	Color fadeColor = Color.black;
	int fadeCounter = 5;
	//static BlackScreenScript instance;
	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().sortingLayerName = "BlackScreen";
		//instance = this;
	}
	
	public void fade(Color newAlpha)
	{
		//Debug.Log ("fade called Before:");
		//Debug.Log (this.GetComponent<SpriteRenderer>().color.a);
		//Debug.Log ("After");
		//Color sample = new Color();
		//sample.a = 0.5f;
		this.GetComponent<SpriteRenderer>().color = newAlpha; //newAlpha;
		//Debug.Log (this.GetComponent<SpriteRenderer>().color.a);
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(this.GetComponent<SpriteRenderer>().color.a);
	}
}
