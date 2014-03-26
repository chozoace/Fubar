using UnityEngine;
using System.Collections;

public class Gernade : MonoBehaviour {
	
	[SerializeField] PhysicsMaterial2D _bounceMaterial;
	[SerializeField] float _bounceReduction;
	[SerializeField] float _leftForce;

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
	void Update () {
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "wall") {
			_bounceMaterial.bounciness -= _bounceReduction;
		}

		if (other.gameObject.tag == "Player") {
			_lensFlare.enabled = true;
			Invoke("TurnOffFlare", 2);
		}

	}
}
