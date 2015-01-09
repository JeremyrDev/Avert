using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3 to;
	float speed = 14;
	SceneControllerNew sc;
	GameObject thePlayer;
    public float score;
    public GUIStyle scoreStyle;
    public GameObject PauseOverlay;
    public bool paused=false;

	void Start () 
	{
        to = new Vector3(0, -3.84f, -1);
		thePlayer = GameObject.Find("Cam");
		sc = thePlayer.GetComponent<SceneControllerNew>();
        PauseOverlay.SetActive(false);
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
            to = new Vector3(-1.8f, -3.84f, -1);
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
		{
            to = new Vector3(0, -3.84f, -1);
		}

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
            to = new Vector3(1.8f, -3.84f, -1);
		}
		if(Input.GetKeyDown(KeyCode.U))
		{
			Time.timeScale+=.5f;
		}
		if(Input.GetKeyDown(KeyCode.I))
		{
			Time.timeScale-=.5f;
		}
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                paused = false;
                PauseOverlay.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                PauseOverlay.SetActive(true);
                Time.timeScale = 0;
            }
        }
		LerpMove();
		if(sc.timeBetweenSpawn<.3f)
			speed = 24;
		else
			speed = 14;
	}

	void LerpMove()
	{
		transform.position = Vector3.Lerp(transform.position, to, speed*Time.deltaTime);
	}
    void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);
        if (other.gameObject.tag == "LifePoint")
        {
            sc.score += 1;
        }
        if (other.gameObject.tag == "FinalLifePoint")
        {
            sc.score += 10;
            sc.finalLifePointReturned = true;
        }
    }
//	void OnTriggerEnter(Collider other)
//	{
//		if(other.tag == "Left" || other.tag == "LeftFar" || other.tag == "RightFar" || other.tag == "Right" || other.tag == "MiddleLeft" || other.tag == "MiddleRight")
//		{
//			Time.timeScale = 0;
//		}
//	}
}
