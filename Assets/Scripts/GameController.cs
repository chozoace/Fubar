using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject playerPrefab;
	static GameController instance;

	[SerializeField] GameObject _gernadePrefab;
	[SerializeField] Transform _gernadeSpawn;

	private GameObject _gernade;
	
	GameObject player;

	// Use this for initialization
	void Start () 
	{
		instance = this;
		SetupLevel(1);

		SpawnGernade ();
	}

	void SpawnGernade()
	{
		_gernade = (GameObject)(Instantiate (_gernadePrefab, _gernadeSpawn.position, Quaternion.identity));
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
	
	public static void RestartLevel(Vector2 playerDeathPos, int checkPointMarker)
	{
		if(checkPointMarker == 1)
			Application.LoadLevel(Application.loadedLevel);
		instance.SetupLevel(checkPointMarker);
		Debug.Log("Restarting level via manager");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
