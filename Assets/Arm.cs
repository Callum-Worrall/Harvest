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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("I am an arm!");
            return;
        }

        Component t = GetComponentInParent<Player>();

        if (col.gameObject.tag == "obstacle")
        {
            if (t.GetComponent<Player>().breaking == true)
            {
                Debug.Log("Destroyed!");

                Destroy(col.gameObject);
            }
            else
                Physics.IgnoreCollision(GameObject.Find("Arm Collider").GetComponent<BoxCollider>(), col.gameObject.GetComponent<BoxCollider>());
        }

            return;
    }
}
