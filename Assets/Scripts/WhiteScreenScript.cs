using UnityEngine;
using System.Collections;

public class WhiteScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void fade(Color newAlpha)
	{
		this.GetComponent<SpriteRenderer>().color = newAlpha;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
