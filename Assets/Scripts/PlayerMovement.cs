﻿using System.Collections;
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
	public float dashSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		uiManager = GetComponent<UIManager>();
		transform = GetComponent<Transform>();
    }

    void Update() {
    	if (Input.GetButtonDown("Dash") && !PlayerStats.dashCooldownTimer.isEnabled()) {
			doDash();
    	}

    	if (PlayerStats.dashTimer.isEnabled()) {
    		setSpeed(dashSpeed);
    	} else {
    		setSpeed(speed);
    	}

        movement = new Vector2(velX, velY);

        checkDash();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
       	rb.velocity = movement;
    }

    void setSpeed(float speed) {
    	velX = Input.GetAxisRaw("Horizontal") * speed;
        velY = Input.GetAxisRaw("Vertical") * speed;

        if (velX == 0 && velY == 0) {
        	PlayerStats.isIdle = true;
        } else {
        	PlayerStats.isIdle = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
    	if (other.gameObject.tag == "Hole") {
			if (!PlayerStats.dashTimer.isEnabled()){
				PlayerStats.lifeLost();
				rb.position = new Vector2(0, 0);
			}
    	}else if(other.gameObject.tag == "Ennemy"){
			if (!PlayerStats.dashTimer.isEnabled()){
				PlayerStats.lifeLost();
			}
		}
    }

	// We create the dust in the action we want
	void CreateDust(){
		dust.Play();
	}

	public void doDash() {
		CreateDust();
		PlayerStats.dashTimer.reset();
		PlayerStats.dashCooldownTimer.reset();
		PlayerStats.isDash = true;
	}

	public void checkDash() {
		PlayerStats.dashTimer.countDown();
        PlayerStats.dashCooldownTimer.countDown();
		PlayerStats.invincibleTimer.countDown();
		if (!PlayerStats.dashTimer.isEnabled()) {
			PlayerStats.isDash = false;
		}
	}
}
