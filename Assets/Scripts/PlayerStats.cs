using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
	private static float maxLife = 3f;
	private static float life = maxLife;
	private static bool isAlive = true;
	public static float invincibleTime = 2f; 
	public static Timer invincibleTimer = new Timer(invincibleTime);
	public static float dashTime = 0.15f;
	public static Timer dashTimer = new Timer(dashTime);
	public static float dashCooldownTime = 2f;
	public static Timer dashCooldownTimer = new Timer(dashCooldownTime);
	private static UIManager uiManager = GameObject
											.FindGameObjectWithTag("Player")
											.GetComponent<UIManager>();

	public static float angle;
	public static bool isIdle;
	public static bool isDash;

	public static void lifeLost(float amount = 1f) {
		if (!PlayerStats.invincibleTimer.isEnabled()){
			lifeMod(-amount);
			PlayerStats.invincibleTimer.reset();
		}	
	}

	public static void lifeRecovered(float amount = 1f) {
		lifeMod(amount);
	}

	public static bool playerAlive() {
		return isAlive;
	}

	public static void playerDead(){
		isAlive = false;
		uiManager.UIplayerDead();
	}

	public static float playerLife() {
		return life;
	}

	private static void lifeMod(float amount) {
		float temp = life + amount;
		if (isAlive) { 
			if (temp > maxLife) {
				life = maxLife;
			} else if(temp <= 0) {
				life = 0;
				playerDead();
			} else {
				life = temp;
			}
		}
		uiManager.UIlifeUpdate();
	}
}