using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public GUISkin GUIskin;

    private Rect pauseMenuWindow;

    float barDisplay = 0;

    Vector2 pos = new Vector2(20,40);
    
    Vector2 size = new Vector2(60,20);
    
    Texture2D progressBarEmpty;
    
    Texture2D progressBarFull;

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
        GameObject player = GameObject.Find("PlayerChar");

        int curStam = player.GetComponent<Player>().currentStamina;
        int maxStam = player.GetComponent<Player>().maxStamina;

        if (curStam > 0)
        {
            GUI.Box(new Rect(10, 10, 10 * curStam, 20), "");
        }

        GUI.Box(new Rect(10, 10, 10 * maxStam, 20), curStam.ToString() + "/" + maxStam.ToString());


    }
}
