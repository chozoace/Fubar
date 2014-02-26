#pragma strict

function OnGUI()
{
	if(GUI.Button (Rect (Screen.width / 2 + 100, Screen.height / 2, 80, 20), "Start Game"))
	{
		Application.LoadLevel("Scene1");
	}
}