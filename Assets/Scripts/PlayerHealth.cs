using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	int maxHealth = 100;
	int currentHealth = 100;
	private GUIStyle currentStyle = null;
	
	public float healthBarLength;
	
	void Start ()
	{
		healthBarLength = Screen.width / 2;
		
	}
	
	void Update () 
	{
		AdjustCurrentHealth(0);
	}
	
	void OnGUI()
	{
		InitStyles();
		GUI.Box(new Rect(10,10,healthBarLength, 20), currentHealth + "/" + maxHealth, currentStyle);
	}
	
	void InitStyles()
	{
		if(currentStyle == null)
		{
			currentStyle = new GUIStyle(GUI.skin.box);
			currentStyle.normal.background = MakeTex(10, 10, new Color(0f, 1f, 0f, .5f));
		}
	}
	
	Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];
		for(int i = 0; i < pix.Length; ++i)
		{
			pix [ i ] = col;
		}
		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}
	
	public void AdjustCurrentHealth(int adj)
	{
		currentHealth += adj;
		
		if(currentHealth < 0)
			currentHealth = 0;
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
	}
}
