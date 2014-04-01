using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour 
{
	GUIText scoreText;
	int score;
	
	void Start () 
	{
		score = 0;
	}
	
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		GUI.TextArea(new Rect(10,42,Screen.width / 4, 20), "Score: " + score);
	}
	
	public void adjustScore(int points)
	{
		score += points;
	}
}
