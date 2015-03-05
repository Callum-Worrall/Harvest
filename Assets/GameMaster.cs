using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public GUISkin GUIskin;

    private Rect pauseMenuWindow;



	// Use this for initialization
	void Start ()
    {
	    pauseMenuWindow = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 400, 200);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 80, 20), "Stamina");


    }
}
