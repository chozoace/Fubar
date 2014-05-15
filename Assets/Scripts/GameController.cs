using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject playerPrefab;
	static GameController instance;
	float endGamePos;

	public enum LevelState
	{
		GamePlay,
		Restarting,
		FlashBanged,
		EndGame
	}
	
	public LevelState levelState;
	
	[SerializeField] GameObject _gernadePrefab;
	[SerializeField] Transform _gernadeSpawn;
	[SerializeField] GameObject BlackScreenPrefab;
	GameObject blackScreen;
	
	public bool controlsLocked;
	int currentCheckpoint;//1 or 2
	
	Color fadeColor = Color.black;
	float fadeCounter = .01f;

	private GameObject _gernade;
	bool gernadeSpawned;
	
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
		levelState = LevelState.GamePlay;
		gernadeSpawned = false;
		
		//SpawnGernade ();
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
		//Debug.Log("gamemanager start");
		//create Player from player spawnpoint
		GameObject playerSpawnPoint;
		if(checkpoint == 1)
		{	
			//Debug.Log("first checkpoint");
			playerSpawnPoint = GameObject.FindGameObjectWithTag("playerSpawn");
		}
		else
		{
			Debug.Log("second checkpoint");
			playerSpawnPoint = GameObject.FindGameObjectWithTag("playerSpawn");
		}
		Vector2 playerSpawnPos = new Vector2(playerSpawnPoint.transform.position.x, playerSpawnPoint.transform.position.y);
		player = (GameObject)(Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity));
		player.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
	}
	
	public void fade(int checkPointMarker = 1)
	{
		Color currentAlpha = blackScreen.GetComponent<SpriteRenderer>().color;
		currentAlpha.a = Mathf.Clamp(blackScreen.GetComponent<SpriteRenderer>().color.a + fadeCounter,0,1);
		blackScreen.GetComponent<BlackScreenScript>().fade(currentAlpha);
		//Debug.Log (currentAlpha);
		
		if(currentAlpha.a == 0)
		{
			GameObject.Destroy(blackScreen);
			fadeCounter *= -1;
			levelState = LevelState.GamePlay;
			controlsLocked = false;
		}
		else if (currentAlpha.a == 1)
		{
			if(levelState != LevelState.EndGame)
			{
				//if(checkPointMarker == 1)
				Application.LoadLevel(Application.loadedLevel);
				instance.SetupLevel(checkPointMarker);
				Debug.Log("Restarting level via manager");
				//Vector2 startPos = new Vector3(-2.2f, 1.8f, -10f);
				//CameraController.Instance().transform.position = startPos;
				fadeCounter *= -1;
				Invoke("fade", 2f);
				//fade (checkPointMarker);
			}
			else
			{
				Invoke("LoadCredits", 1f);
			}
		}
		else
		{
			//fade (checkPointMarker);
		}
	}	
	
	public void RestartLevel(Vector2 playerDeathPos, int checkPointMarker)
	{	
		controlsLocked = true;
		//fade(checkPointMarker);
		currentCheckpoint = checkPointMarker;
		Vector3 deathLocation = new Vector3(playerDeathPos.x, 1.8f, 0);
		blackScreen.transform.position = deathLocation;
		levelState = LevelState.Restarting;
		
		/*Color newAlpha = Color.black;
		newAlpha.a = 1;
		blackScreen.GetComponent<SpriteRenderer>().color = newAlpha;
		Vector3 deathLocation = new Vector3(playerDeathPos.x, 1.8f, 0);
		blackScreen.transform.position = deathLocation;
		currentCheckpoint = checkPointMarker;
		Invoke ("RemoveBlackScreen", 1f);*/
	}
	
	public void EndGame(float xPos)
	{
		endGamePos = xPos;
		Vector3 deathLocation = new Vector3(xPos, 1.8f, 0);
		blackScreen.transform.position = deathLocation;
		levelState = LevelState.EndGame;
		/*controlsLocked = true;
		Color newAlpha = Color.black;
		newAlpha.a = 1;
		blackScreen.GetComponent<SpriteRenderer>().color = newAlpha;
		player = GameObject.FindGameObjectWithTag("Player");
		Vector3 screenLocation = new Vector3(player.transform.position.x, 1.8f, 0);
		blackScreen.transform.position = screenLocation;
		Invoke("LoadCredits", 1f);*/
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
		if(levelState == LevelState.GamePlay)
		{
			if(player.transform.position.x >= 80 && !gernadeSpawned)
			{
				Debug.Log ("grenade made " + _gernadeSpawn.position.x);
				SpawnGernade();
				controlsLocked = true;
				gernadeSpawned = true;
			}
		}
		else if(levelState == LevelState.Restarting)
		{
			fade (currentCheckpoint);
		}
		else if(levelState == LevelState.FlashBanged)
		{
			Vector2 temp = new Vector2(80, player.transform.position.y);
			player.transform.position = temp;
		}
		else if (levelState == LevelState.EndGame)
		{
			//controlsLocked = true;
			Vector2 temp2 = new Vector2(endGamePos, player.transform.position.y);
			player.transform.position = temp2;
			MovementController.Instance().lockControls();
			fade (currentCheckpoint);
		}
	}
}
