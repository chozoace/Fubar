﻿using UnityEngine;
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
	bool deathCounter;

	void Awake()
	{
		_deathRightStateId = Animator.StringToHash ("Base Layer.playerDeathRight");
		_deathLeftStateId = Animator.StringToHash ("Base Layer.playerDeathLeft");
		deathCounter = false;
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
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			//Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
			if(this.gameObject.transform.position.x >= _checkPointMarker.x)
				_currentCheckPoint = 2;
			else 
				_currentCheckPoint = 1;
			if(this.gameObject.transform.position.y <= -10)
			{
				//GameController.Instance().controlsLocked = true;
				playerDeath ("Fall");
			}
			
			if(_health <= 0)
			{
				//GameController.Instance().controlsLocked = true;
				MovementController.Instance().lockControls();
				//anim.SetBool("PlayedAnimation", true);
				playerDeath("Health");
			}
		}
	}
	
	public void takeDamage(int damage)
	{
		_health -= damage;
		this.GetComponent<PlayerHealth>().AdjustCurrentHealth(damage*-1);
	}
	
	public void playerDeath(string condition)
	{
		if(condition == "Health")
		{
			_isDead = true;
			anim.SetBool("isDead", _isDead);
			if(MovementController.Instance().getFacing() == 0 && !deathCounter)
			{
				anim.Play (_deathRightStateId, 0, 0);
				deathCounter = true;
			}
			
			else if(MovementController.Instance().getFacing() == 1 && !deathCounter)
			{
				anim.Play (_deathLeftStateId, 0, 0);
				deathCounter = true;
			}
			
			if(anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).nameHash == _deathRightStateId)
			{
				GameController.Instance().RestartLevel(this.transform.position, _currentCheckPoint);
				GameObject.Destroy(this.gameObject);
			}
			if(anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).nameHash == _deathLeftStateId)
			{
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
	/*void LateUpdate()
	{
		if(GameController.Instance().levelState == GameController.LevelState.GamePlay)
		{
			if(_health <= 0)
			{
				MovementController.Instance().lockControls();
				anim.SetBool("PlayedAnimation", true);
			}
		}
	}*/
}
