using UnityEngine;
using System.Collections;

public class RandomSoundEffects : MonoBehaviour {

	private bool _isInBeginning;
	private bool _isPlaySoundEffect;

	public bool IsInBeginning
	{
		set { _isInBeginning = value; } 
	}

	// Use this for initialization
	void Start () {
		_isPlaySoundEffect = false;
		_isInBeginning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(_isPlaySoundEffect == false) {

			_isPlaySoundEffect = true;

			PickRandomSoundEffect();
		}
	}

	private void PickRandomSoundEffect() {

		float nextPick;

		if(_isInBeginning)
			nextPick = NextPick();
		else 
			nextPick = NextPickF();


		switch((int)nextPick) {
			case 0:
				SoundEffectHelper.Instance.MakeSoundEffect(SoundEffectHelper.SoundEffects.Alarm);
				break;
			case 1:
				SoundEffectHelper.Instance.MakeSoundEffect(SoundEffectHelper.SoundEffects.Boom);
				break;
			case 2:
				SoundEffectHelper.Instance.MakeSoundEffect(SoundEffectHelper.SoundEffects.Heli);
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
		}

		Invoke("PickRandomSoundEffect", 3);
	}

	private float NextPick() {
		return Random.Range(0, 2);
	}

	private float NextPickF() {
		return Random.Range(3, 5);
	}

	private float TimerInterval() {
		return Random.Range(1,3);
	}
}
