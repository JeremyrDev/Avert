using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneControllerNew : MonoBehaviour 
{
	public float fireTime = 1.5f;
	public GameObject platform;
    public GameObject lifePoint;
	public int pooledAmount = 20;
	List<GameObject> platforms;
    List<GameObject> lifePoints;

    public GameObject background;
    public GameObject pauseOverlay;

	public float objectSpeed = 2;
	public float timer = 0;
	public float timeBetweenSpawn = 1;
	public float scoreValue = 1;
	public float multiplier = 1;
	public float score = 10;
    public float seconds = 10;
    public float miliSeconds = 0;
    public float spawnfloat=0;

	public int randomObject = 0;
	public int objectsPlaced = 0;
	public int spawnCounter = 0;
	public int spawnReturned = 0;
	public int spawnCountLimit = 10;
    public int lifePointCounter = 0;
    public int waveCounter = 1;
    public int blockCounter;
    int leftCounter = 0;
    int rightCounter = 0;
    int middleCounter = 0;

	public bool left = false;
	public bool right = false;
	public bool middle = false;
	public bool spawn = true;
	public bool wait = false;
    public bool spawnLifePoint = false;
    public bool finalLifePointFired = false;
    public bool finalLifePointReturned = false;
    public bool middleSpawnedright = false;


	public Vector3 endPos;

	public string info;
	public string scoreString;
    public string milisecondString;
    public string spawnPosition;
    public string testString;

	public GUIStyle infoStyle;
	public GUIStyle scoreStyle;
    public GUIStyle milisecondStyle;

    public TextMesh WaveText;

	void Start()
	{
		platforms = new List<GameObject>();
        lifePoints = new List<GameObject>();
        WaveText = new TextMesh();
		for(int i =0; i<pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(platform);
			obj.SetActive(false);
			platforms.Add(obj);

            GameObject obj3 = (GameObject)Instantiate(lifePoint);
            obj3.SetActive(false);
            lifePoints.Add(obj3);
		}
        pauseOverlay.SetActive(false);
		//InvokeRepeating("Fire", fireTime, fireTime);
	}
	void OnGUI () 
	{
		GUI.Label (new Rect (0,0,100,50), info, infoStyle);
        GUI.Label(new Rect(0, 0, Screen.width,0), scoreString, scoreStyle);
        GUI.Label(new Rect(0, 0, Screen.width, 0),milisecondString, milisecondStyle);
		//GUI.Label (new Rect (Screen.width - 100,0,100,50), multiplierString, multiplierTextStyle);
	}
    public void GameOver()
    {
        pauseOverlay.SetActive(true);
        Time.timeScale = 0.1f;
    }
	void Update()
	{
		//debug info, what's in the left hand corner, just have a central score counter for when you collect life points
        info =  timeBetweenSpawn+"\n"+
                spawnCounter+"\n"+
                spawnReturned+"\n"+
                timer+"\n"+
                spawnPosition+"\n"+
                score+"\n"+
                blockCounter+"\n"+
                waveCounter+"\n"+
                wait.ToString()+"\n"+
                spawn.ToString()+"\n"+
                lifePointCounter ;

        if (score < 0)
        {
            score = 0;
            GameOver();
        }

        //-----Timer--------
        if(!wait)
            miliSeconds -= 1 * Time.deltaTime;
        if (miliSeconds <= 0)
        {
            if (seconds == 0)
            {
                spawnCountLimit += 2;
                spawn = false;
                wait = true;
                seconds = 10;
            }
            miliSeconds = .99f;
            seconds -= 1;
            
        }
        if(wait)
        {
            milisecondString = "";
            scoreString = "10";
        }
        else
        {
            milisecondString = "." + (miliSeconds * 100).ToString("f0");
            scoreString = seconds.ToString("f0");
        }

        //-----Spawn--------
		if(spawn)
		{
			timer+=1*Time.deltaTime;
			if(timer>1)
			{
				objectsPlaced = 0;
                timer = 0;

                //get position
                getPosition();

                //cycle twice
                for (int i = 0; i < 2; i++)
                {
                    Fire();
                    spawnCounter++;
                }

                lifePointCounter++;

                if (lifePointCounter > 3)
                {
                    lifePointCounter = 0;
                    spawnLifePoint = true;
                }
			}
		}

        //-----Per Wave Controller--------
		if( wait && spawnReturned == spawnCounter)
		{
            //wait until finalLifePoint is returned
            if(!finalLifePointFired)
                fireLifePoint();

            //add wave, set counters to zero, wait false, spawn true, false finalLifePoint stuff, change timeBetweenSpawn and object speed
            if (finalLifePointReturned)
            {
                waveCounter++;
                spawnCounter = 0;
                spawnReturned = 0;
                spawn = true;
                wait = false;
                if (objectSpeed >= 12)
                {
                    objectSpeed += .1f;
                    timeBetweenSpawn -= .005f;
                }
                else
                {
                    timeBetweenSpawn -= .02f;
                    objectSpeed += .5f;
                }
                finalLifePointReturned = false;
                finalLifePointFired = false;
            }
		}
	}

    void Fire()
    {
        if (spawnPosition == "middle" || spawnPosition == "sides")
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                if (!platforms[i].activeInHierarchy)
                {

                    setPosition(platforms[i]);

                    platforms[i].SetActive(true);

                    return;
                }
            }
        }
        if (spawnPosition == "left" || spawnPosition == "right")
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                if (!platforms[i].activeInHierarchy)
                {
                    setPosition(platforms[i]);
                    platforms[i].SetActive(true);
                    return;
                }
            }
        }
    }

    void fireLifePoint()
    {
        finalLifePointFired = true;
        for (int i = 0; i < pooledAmount; i++)
        {
            if (!lifePoints[i].activeInHierarchy)
            {
                lifePoints[i].transform.position = new Vector3(0, 6, -1);
                lifePoints[i].tag = "FinalLifePoint";
                lifePoints[i].SetActive(true);
                //WaveText.text = waveCounter.ToString();
                //WaveText.gameObject.SetActive(true);
                //WaveText.transform.position = new Vector3(0, 7, -1);
                return;
            }
        }
        
    }
    void getPosition()
    {
        randomObject = Random.Range(1, 5);
        if (randomObject == 1)
            spawnPosition = "left";
        if (randomObject == 2)
            spawnPosition = "right";
        if (randomObject == 3)
            spawnPosition = "middle";
        if (randomObject == 4)
            spawnPosition = "sides";

    }
    void setPosition(GameObject o)
    {
        //left
        if (spawnPosition == "left") //spawn large block to the left
        {
            if (objectsPlaced == 1)
            {
                o.transform.position = new Vector3(0, 6, -1);
            }
            else
            {
                o.transform.position = new Vector3(-1.8f, 6, -1);
                spawnfloat = 1.8f;
                objectsPlaced++;
            }
        }

        //right
        if (spawnPosition == "right")
        {
            if (objectsPlaced == 1)
            {
                o.transform.position = new Vector3(0, 6, -1);
            }
            else
            {
                o.transform.position = new Vector3(1.8f, 6, -1);
                spawnfloat = -1.8f;
                objectsPlaced++;
            }
        }

        //sides
        if (spawnPosition == "sides")
        {
            if (objectsPlaced == 1)
            {
                o.transform.position = new Vector3(1.8f, 6, -1);
                spawnfloat = 0;
            }
            else
            {
                o.transform.position = new Vector3(-1.8f, 6, -1);
                objectsPlaced++;
            }
        }

        //middle
        if (spawnPosition == "middle")
        {
            if (objectsPlaced == 1)
            {

            }
            else
            {
                o.transform.position = new Vector3(0, 6, -1);
                if (middleSpawnedright) { spawnfloat = 1.8f; } else { spawnfloat = -1.8f; }
                objectsPlaced++;
            }
        }

        if (spawnLifePoint)
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                if (!lifePoints[i].activeInHierarchy)
                {
                    lifePoints[i].transform.position = new Vector3(spawnfloat, 6, -1);
                    lifePoints[i].SetActive(true);
                    return;
                }
            }
        }
    }
}
