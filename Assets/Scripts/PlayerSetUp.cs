﻿using UnityEngine;
using System.Collections;

public class PlayerSetUp : MonoBehaviour {

	public int _health;

	private int _currentCheckPoint;

	private Vector2 _checkPointMarker;

	private Animator anim;
	
	private static int _deathRightStateId;
	private static int _deathLeftStateId;

	private bool _isDead;

	void Awake()
	{
		_deathRightStateId = Animator.StringToHash ("Base Layer.playerDeathRight");
		_deathLeftStateId = Animator.StringToHash ("Base Layer.playerDeathLeft");
	}

	void Start () 
	{
		GameObject checkpoint = GameObject.FindGameObjectWithTag("checkpoint");
		_checkPointMarker = checkpoint.transform.position;
		anim = GetComponent<Animator> ();
		_isDead = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.gameObject.transform.position.x >= _checkPointMarker.x)
		{
			_currentCheckPoint = 2;
		}
		else 
			_currentCheckPoint = 1;
		
		if(_health <= 0)
		{
			_isDead = true;
			anim.SetBool("isDead", _isDead);
			if(anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).nameHash == _deathRightStateId)
			{
				anim.SetBool("isDead", false);
				GameController.RestartLevel(this.transform.position, _currentCheckPoint);
				GameObject.Destroy(this.gameObject);
			}
			if(anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).nameHash == _deathLeftStateId)
			{
				anim.SetBool("isDead", false);
				GameController.RestartLevel(this.transform.position, _currentCheckPoint);
				GameObject.Destroy(this.gameObject);
			}
			//GameController.RestartLevel(this.transform.position, currentCheckPoint);
			//GameObject.Destroy(this.gameObject);
		}
		
	}

	void LateUpdate()
	{
		if(_health <= 0)
		{
			anim.SetBool("PlayedAnimation", true);
		}
	}
}
