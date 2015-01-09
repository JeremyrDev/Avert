using UnityEngine;
using System.Collections;

public class WaveTextScript : MonoBehaviour {
    public Vector3 endPos;
    public float speed = 2;
    SceneControllerNew sc;
    GameObject p;
    public bool start = false;
    public Material regularMaterial;
    public Material passedMaterial;
    void Start()
    {
        p = GameObject.Find("Cam");
        sc = p.GetComponent<SceneControllerNew>();
        speed = sc.objectSpeed;

    }
    void OnEnable()
    {
        //Invoke("Destroy", 10f);
        start = true;
        endPos = new Vector3(transform.position.x, -6, -1);
        speed = sc.objectSpeed;
        gameObject.renderer.material = regularMaterial;
        gameObject.rigidbody.isKinematic = true;
        gameObject.rigidbody.useGravity = false;
        gameObject.collider.enabled = true;
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        //Invoke("Begin", .01f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    //	void OnDisable()
    //	{
    //		CancelInvoke();
    //	}
    void Update()
    {
        if (start)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        }
        if (transform.position == endPos || transform.position.y < -6 || transform.position.y > 7)
        {
            Destroy();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //sc.scoreString = "GAME OVER";

            //Time.timeScale = 0.1f;
            gameObject.collider.enabled = false;
            gameObject.rigidbody.isKinematic = false;
            gameObject.rigidbody.useGravity = true;
            gameObject.rigidbody.AddExplosionForce(2500, new Vector3(other.transform.position.x, other.transform.position.y - 2, other.transform.position.z + 2
                ), 15);
        }
    }
}
