using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;

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

    // Start is called before the first frame update
    void Start()
    {
    	dashTimer = new Timer(dashTime);
		dashCooldownTimer = new Timer(dashCooldownTime);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
    	if (Input.GetButtonDown("Dash") && !dashCooldownTimer.isEnabled()) {
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       	rb.velocity = movement;
    }

    void setSpeed(float speed) {
    	velX = Input.GetAxisRaw("Horizontal") * speed;
        velY = Input.GetAxisRaw("Vertical") * speed;
    }
}
