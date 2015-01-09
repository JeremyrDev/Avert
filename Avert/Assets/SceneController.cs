using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	
	public int numberOfObjectsToCreate = 30;
	
	public GameObject ObjectToPool;
	GameObject[] objectsA = null;
	//public GameObject Player;

	int numberOfObjectsToSpawn = 2;
	public int randomObject;
	public int objectsPlaced = 1;
	int firstCounter = 0;
	int secondCounter = 0;
	int numberSpawned = 0;
	public int numberSpawnedPassed = 0;

	float timer;
	public float timeBetweenSpawn = 1;
	public float score = 0;
	public float scoreValue=1;
	public float multiplier=1;
	float playTimer = 0;
	float timeLimit = 10;
	public float objectSpeed = 2;
	float waitTimer = 0;
	float cycleTimer = 10;
	float offCycleObjectSpeed = 2;
	float offCycleTimeBetweenSpawn = 1;

	public bool purgeB = true;
	public bool spawn = false;
	public bool left = false;
	public bool right = false;
	public bool middle = false;
	bool justStarting = true;
	bool wait = true;
	bool startPurge = true;
	
	Color color1 = new Vector4(0.05f, .09f, .15f, 1);

	string multiplierString;
	public string debugInfo;
	
	public GUIStyle scoreTextStyle;
	public GUIStyle multiplierTextStyle;
		
	Vector3 to = new Vector3(1.8f,-2.75f,-1);
	Vector3 from;
	void Start () 
	{
		objectsA = new GameObject[numberOfObjectsToCreate];
		//		objectsB = new GameObject[numberOfObjectsToCreate];
		//		objectsG = new GameObject[numberOfObjectsToCreate];
		InstantiateObjects();
	}
	
	void OnGUI () 
	{
		GUI.Label (new Rect (0,0,100,50), debugInfo, scoreTextStyle);
		//GUI.Label (new Rect (Screen.width/2,0,100,50), score.ToString("n0"), scoreTextStyle);
		//GUI.Label (new Rect (Screen.width - 100,0,100,50), multiplierString, multiplierTextStyle);
	}

	void Update () 
	{
		if(startPurge)
			purge();
		startPurge = false;

		debugInfo=playTimer.ToString ("f2")+"\n"+numberSpawned+" : "+numberSpawnedPassed+"\n "+timeBetweenSpawn+"\n"+firstCounter+"  "+secondCounter+"\n "+score+"\n x"+multiplier+"\n"+objectSpeed;

		playTimer+=1*Time.deltaTime;
		if(playTimer>timeLimit  && numberSpawnedPassed >= numberSpawned)
		{
			numberSpawned = 0;
			numberSpawnedPassed = 0;
			timeLimit+=10;
			wait = true;
			if(justStarting)
			{
				objectSpeed = 2;
				timeBetweenSpawn = 1;
				justStarting = false;
			}
			else
			{
				offCycleObjectSpeed = objectSpeed;
				offCycleTimeBetweenSpawn = timeBetweenSpawn;
			}
			if(objectSpeed>=5)
			{
				//scale player and objects down
				if(objectSpeed>=10)
				{
					objectSpeed+=.05f;
					if(timeBetweenSpawn>.25f)
						timeBetweenSpawn-=.005f;
				}
				else
				{
					objectSpeed+=.2f;
					if(timeBetweenSpawn>.25f)
						timeBetweenSpawn-=.01f;
				}
			}
			else
			{
				objectSpeed+=.5f;
				timeBetweenSpawn-=.08f;
			}
		}
		//timeBetweenSpawn*=1*(playTimer/10);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(spawn)
				spawn = false;
			else
				spawn = true;
		}
		//debugInfo = "defaultTimer: "+defaultTimer+"\n spawnTimer: "+spawnTimer+"\n"+spawnCounter+"\n "+ amountToSpawn+"\n timebetweenSpawn: "+ timeBetweenSpawn+"\n waiting: "+waiting+"\n wave: "+wave+"\n lives: "+numberOfLives.ToString ("n0");
		//debugInfo = " Wave "+wave.ToString ()+"\n Lives: "+ numberOfLives+"\n sc: "+spawnCounter+"\n spawned: "+spawned+"\n ml: "+multiplierLimit+"\n ats: "+ amountToSpawn +"\n noots: "+numberOfObjectsToSpawn;


		if(spawn && !wait)
		{
			timer+=1*Time.deltaTime;
			if(timer>=timeBetweenSpawn)
			{
				getPosition();
				objectsPlaced = 0;
				firstCounter = 0;
				secondCounter = 0;
				for(int i = 0; i < 2; i++)
				{
					ActivateObject();
					objectsPlaced=1;
					numberSpawned++;
				}
				timer = 0;
			}
		}
		if(wait)
		{
			//same as spawn, but timeBetweenSpawn and objectSpeed are .25 and 12
			waitTimer+=1*Time.deltaTime;
			if(waitTimer > 10)
			{
				wait = false;
				spawn = true;
				waitTimer = 0;
			}
		}

		//-----------------------------Object Detection----------------------------------------------------
//		RaycastHit hit;
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		Debug.DrawRay(ray.origin, Vector3.forward);
//				
//		if(Physics.SphereCast(ray.origin, .75f,transform.forward, out hit, 10))
//		{
//			if(hit.transform.tag == "Object")
//			{
//				//audio.PlayOneShot(objectDeath);
//				//hit.transform.parent.gameObject.GetComponent<smallObject>().TR.enabled = false;
//				//hit.transform.gameObject.GetComponent<Objects>().Deactivate();
//			}
//		}
	}
	void getPosition()
	{
		randomObject = Random.Range(1,4);
		if(randomObject == 1)
		{
			left = true;
			right = false;
			middle = false;
		}
		if(randomObject == 2)
		{
			left = false;
			right = true;
			middle = false;
		}
		if(randomObject == 3)
		{
			left = false;
			right = false;
			middle = true;
		}
	}
	//----------------------------------------------------------------------------------------------------------
	private void InstantiateObjects()
	{
		for(int i = 0; i < numberOfObjectsToCreate; i++)
		{
			objectsA[i] = Instantiate(ObjectToPool) as GameObject;
			objectsA[i].SetActiveRecursively(false);
		}
	}
	//-------------------------------------------------------------------------------------
	private void ActivateObject()
	{		
		firstCounter++;
		for(int i = 0; i < 30; i++)
		{
			secondCounter++;
			if(objectsA[i].active==false)
			{
				objectsA[i].SetActiveRecursively(true);
				objectsA[i].GetComponent<Objects>().Activate();
				return;
			}
		}
	}
	void purge()
	{
		for(int i = 0; i < 30; i++)
		{
			ActivateObject();
		}
	}
}

