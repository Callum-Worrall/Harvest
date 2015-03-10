using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour
{

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        Physics.IgnoreCollision(GameObject.Find("Arm Collider").GetComponent<BoxCollider>(),
            GetComponent<CapsuleCollider>());
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "arm")
        {
            return;
        }

        Component t = GetComponentInParent<Player>();

        if (col.gameObject.tag == "obstacle")
        {
            if (t.GetComponent<Player>().sprinting == false)
            {
                t.GetComponent<Player>().SetStumbling();

                //Auto run into
                Debug.Log("Ran into " + col.gameObject.name + " and stumbled!", gameObject);
            }
            else
            {
                Debug.Log("Charged through the " + col.gameObject.name + "!", gameObject);
            }

            Destroy(col.gameObject);
        }
    }
}
