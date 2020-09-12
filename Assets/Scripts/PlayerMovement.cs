using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	public UIManager uiManager;
	public ParticleSystem dust;

	// Respawn
	private Transform transform;
	private Vector2 lastPos;


	// Movement
	private float velX;
	private float velY;
	private Vector2 movement;

	public float speed = 1f;

	// Dash
	private Timer dashCooldownTimer;
	public float dashCooldownTime = 2f;

	private Timer dashTimer;
	public float dashTime = 0.15f;
	public float dashSpeed = 5f;

	// debug
	public bool debug = true;

	//Mouse Movement
	public Camera cam;
	Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
    	dashTimer = new Timer(dashTime);
		dashCooldownTimer = new Timer(dashCooldownTime);
        rb = GetComponent<Rigidbody2D>();
		//rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		uiManager = GetComponent<UIManager>();
		transform = GetComponent<Transform>();
    }

    void Update() {
    	if (Input.GetButtonDown("Dash") && !dashCooldownTimer.isEnabled()) {
			CreateDust();
    		dashTimer.reset();
    		dashCooldownTimer.reset();
    		// TODO when entering dash mode, cannot be damaged
    	}

    	if (dashTimer.isEnabled()) {
    		setSpeed(dashSpeed);
    	} else {
    		setSpeed(speed);
    	}

        movement = new Vector2(velX, velY);

        // Countdowns
        dashTimer.countDown();
        dashCooldownTimer.countDown();
		PlayerStats.invincibleTimer.countDown();
        // TODO Remove this
        if (debug) {
        	if (!PlayerStats.playerAlive()) {
    		uiManager.UIplayerDead();
	    	}

	        if (Input.GetButtonDown("Fire1")) {
	        	PlayerStats.lifeLost();
	        }
	        if (Input.GetButtonDown("Dash")) {
	        	PlayerStats.lifeRecovered();
	        }
        }

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }



    // Update is called once per frame
    void FixedUpdate()
    {
       	rb.velocity = movement;

		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
    }

    void setSpeed(float speed) {
    	velX = Input.GetAxisRaw("Horizontal") * speed;
        velY = Input.GetAxisRaw("Vertical") * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
    	if (other.gameObject.tag == "Hole") {
			if(!dashTimer.isEnabled()){
				PlayerStats.lifeLost();
				rb.position = new Vector2(0, 0);
			}
			Debug.Log("Hole Enter");
    		// TODO consider rewsetting position when entering hole
    		// TODO when dashing, player is invincible
    	}
		if(other.gameObject.tag == "Respawn"){
			lastPos = transform.position;
			Debug.Log("Last Pos: " + lastPos.x + ", " + lastPos.y);
			
		}
		
    }

	private void OnTriggerStay2D(Collider2D other) {
		
	}


	//We create the dust in the action we want
	void CreateDust(){
		dust.Play();
	}
}
