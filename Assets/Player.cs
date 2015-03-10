using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //////////VARIABLES///////////
    #region

    // Utility //
    float timer = 0f;

    // Player Stats //
    public int currentStamina = 5;
    public int maxStamina = 10;

    // Speed Settings //
	public Vector3 speed;
    public Vector3 sprintSpeed;
    public Vector3 stumbleSpeed;
	public Vector3 jumpSpeed;
	public Vector3 gravity;

    // Stumbling Variables //
    public float stumbleTime = 1.0f;
    public bool stumbling = false;
    float stumblingTimer;

    // Sprinting Variables //
    public float sprintTime = 1.0f;
    public bool sprinting = false;
    float sprintingTimer;
   
    // Jumping Variables //
    public float jumpHeight = 2.0f;
    public float maxJumpHeight = 2.0f;
    float maxJumpSpeed;
	public bool grounded = true;
	public bool reachedApex;
    float apexTimer;
    float jumpVelocity = 0.0f;
    Vector3 jumpVector;
    public float jumpCounter;

    // Attack Variables //
    public bool breaking = false;
    float breakTimer = 0.0f;
    public float breakSwingTimer = 1.0f;
    public float attackDistance = 3.0f;
    Vector3 forward;

    #endregion
    /////////////////////////////

    //////////FUNCTIONS///////////

	// Initialization //
	void Start ()
	{
		forward.x = 1.0f;
		forward.y = 0.0f;
		forward.z = 0.0f;
	}
	
	// Update //
	void Update ()
	{
        timer += Time.deltaTime;

        //Autorun
        if (sprinting == false)
        {
            if (stumbling == false)
            {
                transform.position += speed * Time.deltaTime;
            }
            else
                transform.position += stumbleSpeed * Time.deltaTime;
        }
        else
            transform.position += sprintSpeed * Time.deltaTime;

        EatCritter();
        Stumble();
        Sprint();
		Jump();
        BreakObject();
        EatPrey();
	}

    //Eat Critters//
    void EatCritter()
    {
        if (Input.GetButtonDown("Eat"))
        {
            GameObject[] critters = UnityEngine.Object.FindObjectsOfType<GameObject>();

            for (int i = 0; i < critters.Length; i++)
            {
                if (critters[i].CompareTag("critter") == true)
	            {
                    if (critters[i].transform.position.x < (transform.position.x + 2) &&
                        critters[i].transform.position.x > (transform.position.x - 2))
                    {
                        if (currentStamina < 10)
                        {
                            currentStamina += 1;
                            Destroy(critters[i]);
                            Debug.Log("Ate the " + critters[i].name + "!", gameObject);
                        }
                    }
	            }
            }
        }
    }

    // Break Object //
    void BreakObject()
    {
        if (Input.GetButtonDown("Attack") && breaking == false)
        {
            breaking = true;
        }

        if (breaking == true)
        {
            breakTimer += Time.deltaTime;
        }

        if (breaking == true && breakTimer > breakSwingTimer)
        {
            breaking = false;
            breakTimer = 0;
        }

        //if (breaking == true)
        //{
        //    RaycastHit hit;
        //    Vector3 hitRange = transform.position;
        //    for (int i = 0; i < 31; i++)
        //    {
        //        hitRange.y = (transform.position.y + 2.0f) - 0.05f * i;
        //        Ray ray = new Ray(hitRange, forward);
        //
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.collider != null)
        //            {
        //                if (Physics.Raycast(transform.position, forward, attackDistance) &&
        //                        hit.collider.gameObject.tag == "obstacle")
        //                {
        //                    hit.collider.enabled = false;
        //                    Debug.Log("Destroyed the " + hit.collider.gameObject.name + "!");
        //
        //                    hit.collider.GetComponent<Obstacle>().Die();
        //                }
        //            }
        //        }
        //    }
        //}
    }

    // Sprint //
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint") &&
                        sprinting == false &&
                            stumbling == false &&
                                grounded == true &&
                                    currentStamina > 0)
        {
            sprinting = true;
            sprintingTimer = 0.0f;
            Debug.Log("Sprinting!", gameObject);

            currentStamina -= 1;
        }

        if(sprinting == true)
        {
            sprintingTimer += Time.deltaTime;
        }

        if (sprintingTimer > sprintTime && grounded == true)
        {
            sprinting = false;
        }
    }

    // Stumble //
    void Stumble()
    {
        if (stumbling == true)
        {
            stumblingTimer += Time.deltaTime;
        }

        if (stumblingTimer > stumbleTime)
        {
            stumbling = false;
        }
    }

    // Jump //
	void Jump()
	{
        //Check if grounded
		if(Input.GetButtonDown("Jump") && grounded == true)
		{
			grounded = false;
            jumpVelocity = 0.0f;
		}

        //Check if apex is reached and not on the ground, if true continue to jump
		if (grounded == false && reachedApex == false)
		{
            float newPosition = Mathf.SmoothDamp(
                transform.position.y,
                jumpHeight, 
                ref jumpVelocity, 
                1.0f * Time.deltaTime, 
                jumpSpeed.y);

            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
		}

        //Check if jump height is reached
		if (transform.position.y >= maxJumpHeight)
		{
			reachedApex = true;
            jumpVelocity = 0.0f;

            //apexTimer += 0.1f * (float)Time.deltaTime;
		}

        //Start acting gravity on the player to bring it back to the ground
		if (reachedApex == true)
		{
            float newPosition = Mathf.SmoothDamp(
                transform.position.y,
                0.15f,
                ref jumpVelocity,
                1.0f * Time.deltaTime, 
                -gravity.y);

            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);

            apexTimer = 0.0f;
		}

        //Player land
        if (grounded == false && transform.position.y <= 0.15f)
        {
            transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
            grounded = true;
            reachedApex = false;
        }        
	}


    bool GetSprinting()
    {
        return sprinting;
    }

    public void SetStumbling()
    {
        stumbling = true;
        stumblingTimer = 0.0f;
    }

    void EatPrey()
    {
        GameObject Prey = GameObject.Find("Prey");

        if (Prey.transform.position.x < (transform.position.x + 2) &&
            Prey.transform.position.x > (transform.position.x - 2))
        {
            if (currentStamina < 10)
            {
                //currentStamina += 1;
                //Destroy(Prey);
                Debug.Log("You caught your prey, you win!", gameObject);
            }
        }
        
    }
}





