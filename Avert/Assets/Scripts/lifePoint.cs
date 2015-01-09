using UnityEngine;
using System.Collections;

public class lifePoint : MonoBehaviour {
	public Vector3 endPos;
	public float speed = 2;
	SceneControllerNew sc;
	GameObject p;
	public bool start = false;

	void Start()
	{
		p = GameObject.Find("Cam");
		sc = p.GetComponent<SceneControllerNew>();
        speed = sc.objectSpeed;
        gameObject.SetActive(false);
	}
	public void OnEnable()
	{
		//Invoke("Destroy", 10f);
		start = true;
		endPos = new Vector3(transform.position.x, -6, -1);
		speed = sc.objectSpeed;
        gameObject.renderer.enabled = true;
        if (sc.finalLifePointFired)
            gameObject.tag = "FinalLifePoint";
        else
            gameObject.tag = "LifePoint";
		//Invoke("Begin", .01f);
	}
	void Destroy()
	{
		gameObject.SetActive(false);
        if(sc.finalLifePointFired)
            sc.finalLifePointReturned = true;
	}
//	void OnDisable()
//	{
//		CancelInvoke();
//	}
	void Update()
	{
		if(start){
			transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
		}
		if(transform.position == endPos)
		{
			Destroy();
			//sc.spawnReturned++;
		}
	}
	void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            //sc.scorestring = "game over";
            //sc.score += 1;
            gameObject.renderer.enabled = false;
            //time.timescale = 0.1f;
        }
        if (other.tag == "p")
        {
            //sc.scorestring = "game over";
            sc.score -= 10;
            //sc.scorestring = sc.score.tostring();
            //time.timescale = 0;
        }
	}
	void OnTriggerExit(Collider other)
	{
        //if(other.tag == "p")
        //{
        //    //sc.scoreString = "GAME OVER";
        //    sc.score +=1;
        //    sc.scoreString = sc.score.ToString ();
        //    //Time.timeScale = 0;
        //}
	}
}
