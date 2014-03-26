using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject playerPrefab;
	static GameController instance;

	[SerializeField] GameObject _gernadePrefab;
	[SerializeField] Transform _gernadeSpawn;
	[SerializeField] GameObject BlackScreenPrefab;
	GameObject blackScreen;
	
	public bool controlsLocked;
	int currentCheckpoint;//1 or 2
	
	Color fadeColor = Color.black;
	float fadeCounter = .01f;

	private GameObject _gernade;
	
	GameObject player;

	// Use this for initialization
	void Start () 
	{
		instance = this;
		SetupLevel(1);
		blackScreen = (GameObject)Instantiate(BlackScreenPrefab, new Vector3(player.transform.position.x, 1.8f, 0), Quaternion.identity);
		Color initialAlpha = Color.black;
		initialAlpha.a = 0;
		blackScreen.GetComponent<SpriteRenderer>().color = initialAlpha;
		controlsLocked = false;
		
		SpawnGernade ();
	}
	
	public bool isControlLocked()
	{
		return controlsLocked;
	}
	
	void SpawnGernade()
	{
		_gernade = (GameObject)(Instantiate (_gernadePrefab, _gernadeSpawn.position, Quaternion.identity));
	}
	
	public static GameController Instance()
	{
		return instance;
	}
	
	void SetupLevel(int checkpoint)
	{
		Debug.Log("gamemanager start");
		//create Player from player spawnpoint
		GameObject playerSpawnPoint;
		if(checkpoint == 1)
		{	
			Debug.Log("first checkpoint");
			playerSpawnPoint = GameObject.FindGameObjectWithTag("playerSpawn");
		}
		else
		{
			Debug.Log("second checkpoint");
			playerSpawnPoint = GameObject.FindGameObjectWithTag("playerSpawn2");
		}
		Vector2 playerSpawnPos = new Vector2(playerSpawnPoint.transform.position.x, playerSpawnPoint.transform.position.y);
		player = (GameObject)(Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity));
		player.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
	}
	
	public void fade(int checkPointMarker)
	{
		Color currentAlpha = blackScreen.GetComponent<SpriteRenderer>().color;
		currentAlpha.a = Mathf.Clamp(blackScreen.GetComponent<SpriteRenderer>().color.a + fadeCounter,0,1);
		blackScreen.GetComponent<BlackScreenScript>().fade(currentAlpha);
		//Debug.Log (currentAlpha);
		
		if(currentAlpha.a == 0)
		{
			GameObject.Destroy(blackScreen);
			fadeCounter *= -1;
		}
		else if (currentAlpha.a == 1)
		{
			if(checkPointMarker == 1)
				Application.LoadLevel(Application.loadedLevel);
			instance.SetupLevel(checkPointMarker);
			Debug.Log("Restarting level via manager");
			fadeCounter *= -1;
			fade (checkPointMarker);
		}
		else
		{
			fade (checkPointMarker);
		}
	}
	
	public void RestartLevel(Vector2 playerDeathPos, int checkPointMarker)
	{		
		//fade(checkPointMarker);
		controlsLocked = true;
		Color newAlpha = Color.black;
		newAlpha.a = 1;
		blackScreen.GetComponent<SpriteRenderer>().color = newAlpha;
		Vector3 deathLocation = new Vector3(playerDeathPos.x, 1.8f, 0);
		blackScreen.transform.position = deathLocation;
		currentCheckpoint = checkPointMarker;
		Invoke ("RemoveBlackScreen", 1f);
	}
	
	public void EndGame()
	{
		controlsLocked = true;
		Color newAlpha = Color.black;
		newAlpha.a = 1;
		blackScreen.GetComponent<SpriteRenderer>().color = newAlpha;
		player = GameObject.FindGameObjectWithTag("Player");
		Vector3 screenLocation = new Vector3(player.transform.position.x, 1.8f, 0);
		blackScreen.transform.position = screenLocation;
		Invoke("LoadCredits", 1f);
	}
	
	public void LoadCredits()
	{
		Application.LoadLevel("CreditsScene");
	}
	
	void RemoveBlackScreen()
	{
		if(currentCheckpoint == 1)
			Application.LoadLevel(Application.loadedLevel);
		instance.SetupLevel(currentCheckpoint);
		Debug.Log("Restarting level via manager");
		Color initialAlpha = Color.black;
		initialAlpha.a = 0;
		blackScreen.GetComponent<SpriteRenderer>().color = initialAlpha;
		controlsLocked = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
