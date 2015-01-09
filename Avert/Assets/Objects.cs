using UnityEngine;
using System.Collections;

public class Objects : MonoBehaviour {
	
	float life = 10f;
	private float speed = 5.0f;
	private float deathTime = 0.0f;
	public Vector3 endPosition;
	//public Quaternion originalRotationValue;
	GameObject thePlayer;
	int randomX = 0;
	int randomX2 = 0;
	SceneController sceneScript;
	int randomNumber = 0;
	//public TrailRenderer TR;
	//For raycasting---
	//RaycastHit hit;
	//float maxRange = 1.1f;
	void Start()
	{
		thePlayer = GameObject.Find("Cam");
		sceneScript = thePlayer.GetComponent<SceneController>();
		if(sceneScript.purgeB)
		{
			transform.position = new Vector3(0,-6,-1);
			endPosition = new Vector3(0,-6,-1);
		}
	}
	
	void Update () 
	{
		if(transform.position == endPosition)
		{
			sceneScript.numberSpawnedPassed++;
			Deactivate();
		}
		CountDown();
		AutoMove();
		//per object raycasting to another main object
		//		if(Physics.Raycast(transform.position,(player.transform.position-transform.position), out hit))
		//			
		//		{
		//			Debug.DrawRay(transform.position, (player.transform.position-transform.position), Color.green);
		//			
		//		}    
		//		if(Vector3.Distance (player.transform.position, transform.position)<= maxRange)
		//		{
		//			player.GetComponent<PlayerController>().PlayerDeath();
		//		}
	}
	
	public void Activate()
	{
		speed = sceneScript.objectSpeed;
//		scaleTo = new Vector3(.075f, .075f, .075f);
//		float tempWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
//		
		deathTime = Time.time + life;
		
		DeterminePosition();
	}
	void DeterminePosition()
	{
		if(sceneScript.left)
		{
			if(sceneScript.objectsPlaced == 1)
			{
				transform.position = new Vector3(0, 6, -1);
				endPosition = new Vector3(0, -6, -1);
			}
			else
			{
				transform.position = new Vector3(-1.8f, 6, -1);
				endPosition = new Vector3(-1.8f, -6, -1);
			}
		}
		if(sceneScript.right)
		{
			if(sceneScript.objectsPlaced == 1)
			{
				transform.position = new Vector3(0, 6, -1);
				endPosition = new Vector3(0, -6, -1);
			}
			else
			{
				transform.position = new Vector3(1.8f, 6, -1);
				endPosition = new Vector3(1.8f, -6, -1);
			}
		}
		if(sceneScript.middle)
		{
			if(sceneScript.objectsPlaced == 1)
			{
				transform.position = new Vector3(1.8f, 6, -1);
				endPosition = new Vector3(1.8f, -6, -1);
			}
			else
			{
				transform.position = new Vector3(-1.8f, 6, -1);
				endPosition = new Vector3(-1.8f, -6, -1);
			}
		}

	}
	public void Deactivate()
	{
		this.gameObject.SetActiveRecursively(false);
	}
	private void CountDown()
	{
		if(deathTime<Time.time)
		{
			this.gameObject.SetActiveRecursively(false);
			//			sceneScript.scoreMultiplier = 1;
			//			sceneScript.cycle = 6;
			//			sceneScript.multiplierLimit = 2;
			//			sceneScript.multiplierCounter = 0;
			//			sceneScript.numberOfLives-=1;
		}
	}
	//	public void OnTriggerEnter(Collider other)
	//	{
	//		if(other.tag == "objectDeath")
	//		{
	//			this.gameObject.SetActiveRecursively(false);
	//			sceneScript.scoreMultiplier = 1;
	//			sceneScript.cycle = 6;
	//			sceneScript.multiplierLimit = 2;
	//			sceneScript.multiplierCounter = 0;
	//			sceneScript.numberOfLives-=1;
	//		}
	//	}
	private void AutoMove()
	{
		//if(randomNumber == 0){
		//renderer.material = lerpMaterial;
		//transform.position = Vector3.Lerp(transform.position, endPosition, (speed/10) * Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);
		//}
	}
	void OnTriggerEnter(Collider Other)
	{
		if(Other.tag == "Passed")
		{
			//sceneScript.score += sceneScript.multiplier*sceneScript.scoreValue;
			sceneScript.score+=1;
		}
		if(Other.tag == "Player")
		{
            sceneScript.score += 1;
			Time.timeScale = 0.1f;
		}
	}
}
