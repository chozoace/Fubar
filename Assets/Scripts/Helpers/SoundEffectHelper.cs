using UnityEngine;
using System.Collections;

public class SoundEffectHelper : MonoBehaviour {

	[SerializeField] AudioClip _alarm;
	[SerializeField] AudioClip _boom;
	[SerializeField] AudioClip _flashBang;
	[SerializeField] AudioClip _boomstickReload;
	[SerializeField] AudioClip _heli;
	[SerializeField] AudioClip _machineGun;
	[SerializeField] AudioClip _shotgunReload;
	[SerializeField] AudioClip _reload;
	[SerializeField] AudioClip _reloadOrGunSwap;

	private static SoundEffectHelper _instance;

	public static SoundEffectHelper Instance {
		get { return _instance; }
	}

	public enum SoundEffects {
		Alarm, Boom, FlashBang, BoomstickReload, Heli,
		MachineGun, ShotgunReload, Reload, ReloadOrGunSwap
	}

	void Awake() {
		if(_instance != null) {
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		_instance = this;
	}

	public void MakeSoundEffect(SoundEffects soundeffect) {
		switch(soundeffect) {
			case SoundEffects.Alarm:
				PlaySoundEffect(_alarm);
				break;
			case SoundEffects.Boom:
				PlaySoundEffect(_boom);
				break;
			case SoundEffects.BoomstickReload:
				PlaySoundEffect(_boomstickReload);
				break;
			case SoundEffects.FlashBang:
				PlaySoundEffect(_flashBang);
				break;
			case SoundEffects.Heli:
				PlaySoundEffect(_heli);
				break;
			case SoundEffects.MachineGun:
				PlaySoundEffect(_machineGun);
				break;
			case SoundEffects.Reload:
				PlaySoundEffect(_reload);
				break;
			case SoundEffects.ReloadOrGunSwap:
				PlaySoundEffect(_reloadOrGunSwap);
				break;
			case SoundEffects.ShotgunReload:
				PlaySoundEffect(_shotgunReload);
				break;
		}
	}

	private void PlaySoundEffect(AudioClip originalClip) {
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}
