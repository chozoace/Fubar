using UnityEngine;
using System.Collections;

public class BulletFlashScript : MonoBehaviour
{
	Animator anim;
	int facing;
	int prevFacing;
	GameObject player;
	Vector3 currentScale;
	
	void Start ()
	{
		player = null;
		Invoke ("DestroySelf", .2f);
		anim = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer> ().sortingLayerName = "New Layer 5";
		facing = 3;
	}
	
	public void playerReference(GameObject playerRef, int facing)
	{
		player = playerRef;	
	}
	
	void DestroySelf()
	{
		GameObject.Destroy(this.gameObject);
	}
	
	void Update () 
	{
		//find a way to make it so that the player isnt the only possible person to be called
		//also fix the left flash, it goes right then left. Pass players in start function?
		
		Debug.Log (currentScale.x);
		player = GameObject.FindGameObjectWithTag("Player");
		prevFacing = facing;
		facing = player.GetComponent<MovementController>().getFacing();
			
		if(player != null)
		{
			if(facing == 0)
			{
				//Debug.Log("in flash update");
				this.transform.position = new Vector2(player.transform.position.x + .4f, player.transform.position.y + .06f);
			}
			else if(facing == 1)
			{
				this.transform.position = new Vector2(player.transform.position.x - .4f, player.transform.position.y + .06f);
				if(prevFacing != 1)
				{
					currentScale = transform.localScale;
					currentScale.x *= -1;
					transform.localScale = currentScale;
				}
			}
		}
	}
}
