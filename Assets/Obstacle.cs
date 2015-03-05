using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.tag == "Player")
        //{
        //    Debug.Log("Detection Detected!", gameObject);
        //}
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
