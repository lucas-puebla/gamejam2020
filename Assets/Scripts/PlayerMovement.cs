﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
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

	// Pause
	private bool isPause = false;
	public Image pauseOverlay;
	private Timer pauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		transform = GetComponent<Transform>();
		pauseTimer = new Timer(0.1f);
    }

    void Update() {
    	if (!isPause) {
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
		pause();

		if (Input.GetKey(KeyCode.Escape) && !pauseTimer.isEnabled()) {
			pauseTimer.reset();
			isPause = !isPause;
		}
		pauseTimer.countDown();
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
    	}else {
    		ennemyInteraction(other);
    	}
    }

    private void OnTriggerStay2D(Collider2D other) {
    	ennemyInteraction(other);
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

	private void ennemyInteraction(Collider2D other) {
		if(other.gameObject.tag == "Ennemy"){
			if (!PlayerStats.dashTimer.isEnabled()){
				PlayerStats.lifeLost();
			}
		}
	}

	private void pause() {
		if (isPause) {
			Time.timeScale = 0.05f;
			pauseOverlay.color = new Color(1, 1, 1, 1);
		} else {
			Time.timeScale = 1f;
			pauseOverlay.color = new Color(1, 1, 1, 0);
		}

	}
}
