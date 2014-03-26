using UnityEngine;
using System.Collections;

public class PlayerSetUp : MonoBehaviour {

	public int _health;

	private int _currentCheckPoint;

	private Vector2 _checkPointMarker;//1 or 2

	private Animator anim;
	
	private static int _deathRightStateId;
	private static int _deathLeftStateId;

	private bool _isDead;
	[SerializeField] GameObject BlackScreenPrefab;
	GameObject blackScreen;

	void Awake()
	{
		_deathRightStateId = Animator.StringToHash ("Base Layer.playerDeathRight");
		_deathLeftStateId = Animator.StringToHash ("Base Layer.playerDeathLeft");
	}
	

	void Start () 
	{
		/*Color initialAlpha = Color.black;
		initialAlpha.a = 0;
		blackScreen = (GameObject)Instantiate(BlackScreenPrefab, new Vector3(this.transform.position.x, 1.8f, -4), Quaternion.identity);
		blackScreen.GetComponent<SpriteRenderer>().color = initialAlpha;*/
	
		GameObject checkpoint = GameObject.FindGameObjectWithTag("checkpoint");
		_checkPointMarker = checkpoint.transform.position;
		anim = GetComponent<Animator> ();
		_isDead = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.gameObject.transform.position.x >= _checkPointMarker.x)
			_currentCheckPoint = 2;
		else 
			_currentCheckPoint = 1;
		if(this.gameObject.transform.position.y <= -10)
			playerDeath ("Fall");
		
		if(_health <= 0)
		{
			playerDeath("Health");
		}
		
	}
	
	public void playerDeath(string condition)
	{
		if(condition == "Health")
		{
			_isDead = true;
			anim.SetBool("isDead", _isDead);
			if(anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).nameHash == _deathRightStateId)
			{
				anim.SetBool("isDead", false);
				GameController.Instance().RestartLevel(this.transform.position, _currentCheckPoint);
				GameObject.Destroy(this.gameObject);
			}
			if(anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).nameHash == _deathLeftStateId)
			{
				anim.SetBool("isDead", false);
				GameController.Instance().RestartLevel(this.transform.position, _currentCheckPoint);
				GameObject.Destroy(this.gameObject);
			}
		}
		else if(condition == "Fall")
		{
			_isDead = true;
			GameController.Instance().RestartLevel(this.transform.position, _currentCheckPoint);
			GameObject.Destroy(this.gameObject);
		}
	}
	void LateUpdate()
	{
		if(_health <= 0)
		{
			MovementController.Instance().lockControls();
			anim.SetBool("PlayedAnimation", true);
		}
	}
}
