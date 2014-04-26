using UnityEngine;
using System.Collections;

public class Gernade : MonoBehaviour {
	
	[SerializeField] PhysicsMaterial2D _bounceMaterial;
	[SerializeField] float _bounceReduction;
	[SerializeField] float _leftForce;
	[SerializeField] GameObject WhiteScreenPrefab;
	GameObject whiteScreen;
	
	bool madeContact = false;
	bool startedFade = false;
	Vector2 newGernadePos = new Vector2(0, -20);
	
	float fadeCounter = -.01f;

	private LensFlare _lensFlare;

	void LeftForce()
	{
		rigidbody2D.AddForce (new Vector2(_leftForce, 0));
	}

	/// <summary>
	/// Feel Like I'm Cheating Using Invoke
	/// </summary>
	void TurnOffFlare()
	{
		_lensFlare.enabled = false;
		DestroyObject (this.gameObject);
		GameController.Instance().controlsLocked = false;
		
	}
	
	void fadeOut()
	{
		Color currentAlpha = whiteScreen.GetComponent<SpriteRenderer>().color;
		currentAlpha.a = Mathf.Clamp(whiteScreen.GetComponent<SpriteRenderer>().color.a + fadeCounter,0,1);
		whiteScreen.GetComponent<WhiteScreenScript>().fade(currentAlpha);
		startedFade = true;
		//Debug.Log (currentAlpha);
		
		if(currentAlpha.a == 0)
		{
			GameController.Instance().levelState = GameController.LevelState.GamePlay;
			GameController.Instance().controlsLocked = false;
			DestroyObject(this.gameObject);
		}
	}

	void Awake() {
		_bounceMaterial.bounciness = 1;
	}

	// Use this for initialization
	void Start () {
		_lensFlare = GetComponent<LensFlare> ();
		LeftForce ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameController.Instance().levelState == GameController.LevelState.FlashBanged && startedFade)
		{
			fadeOut();
		}
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "wall") {
			_bounceMaterial.bounciness -= _bounceReduction;
		}

		if (other.gameObject.tag == "Player") {
			if(!madeContact)
			{
				whiteScreen = (GameObject)Instantiate(WhiteScreenPrefab, new Vector3(other.transform.position.x, 1.8f, 0), Quaternion.identity);
				GameController.Instance().levelState = GameController.LevelState.FlashBanged;
				GetComponent<SpriteRenderer>().enabled = false;
				Invoke("fadeOut", 2f);
				madeContact = true;
				//_bounceMaterial.bounciness = 0;
			}
			//_lensFlare.enabled = true;
			//Invoke("TurnOffFlare", 2);
		}

	}
}
