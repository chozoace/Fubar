#pragma strict

var first_music : GameObject;

function Start()
{
}

function OnTriggerEnter2D(other : Collider2D)
{
	if(other.gameObject.tag == "Player")
	{
		Debug.Log("hit");
		first_music = GameObject.FindGameObjectWithTag("background_music");
		first_music.audio.mute = true;
		//first_music.audio.mute = true;
		this.audio.Play();
		
	}
}

function Update () {

}