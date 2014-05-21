using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour 
{
	[SerializeField] KeyCode closeGame;
    bool readyToClose = false;
    bool startUpdate = false;

    float fadeCounter = .02f;
	
	void Start ()
	{
        Color transparentColor = this.GetComponent<SpriteRenderer>().color;
        transparentColor.a = 0;
        this.GetComponent<SpriteRenderer>().color = transparentColor;
        Invoke("beginUpdate", 2f);
        //readyToClose = true;
        //startUpdate = true;
	}

    void beginUpdate()
    {
        startUpdate = true;
    }

    void fadeIn()
    {
        Color currentAlpha = this.GetComponent<SpriteRenderer>().color;
        currentAlpha.a = Mathf.Clamp(this.GetComponent<SpriteRenderer>().color.a + fadeCounter, 0, 1);
        this.GetComponent<SpriteRenderer>().color = currentAlpha;
        //Debug.Log (currentAlpha);

        if (currentAlpha.a == 1)
        {
            readyToClose = true;
        }
    }
	
	void Update () 
	{
        if (startUpdate)
        {
            if (Input.GetKeyDown(closeGame) && readyToClose)
            {
                Debug.Log("pressed");
                Application.Quit();
            }
            else if (!readyToClose)
            {
                fadeIn();
            }
        }
	}
}
