using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnColliderEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("I am an arm!");
            return;
        }

        if (col.gameObject.tag == "obstacle")
        {
            Debug.Log("Destroyed!");
            col.GetComponent<Obstacle>().Die();
            return;
        }
    }
}
