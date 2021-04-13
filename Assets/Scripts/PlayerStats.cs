using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStats {
	private static int maxLife = 3;
	private static int life = maxLife;
	private static bool isAlive = true;
	private static int score = 0;
	public static float invincibleTime = 2f; 
	public static Timer invincibleTimer = new Timer(invincibleTime);
	public static float dashTime = 0.15f;
	public static Timer dashTimer = new Timer(dashTime);
	public static float dashCooldownTime = 2f;
	public static Timer dashCooldownTimer = new Timer(dashCooldownTime);
	
	public static int ennemiesKilled = 0;
	public static int weaponDamage = 1;

	public static int currentWave = 0;
	public static string[] levelName = new string[4] {"Level1", "Level2", "Level3", "Level4" };
	public static int currentLevel = 0;

	public static float angle;
	public static bool isIdle;
	public static bool isDash;

	public static void lifeLost(int amount = 1) {
		if (!PlayerStats.invincibleTimer.isEnabled()){
			lifeMod(-amount);
			PlayerStats.invincibleTimer.reset();
		}	
	}

	public static void lifeRecovered(int amount = 1) {
		lifeMod(amount);
	}

	public static bool playerAlive() {
		return isAlive;
	}

	public static void playerDead(){
		isAlive = false;
		SceneManager.LoadScene("Game Over");
		playerResetLife();
		score = ennemiesKilled;
		ennemiesKilled = 0;
	}

	public static void playerResetLife() {
		isAlive = true;
		life = maxLife;
	}

	public static int playerLife() {
		return life;
	}

	public static void playerSpawn() {
		currentWave = 0;
	}

	public static void firstSpawn() {
		currentWave = 0;
		ennemiesKilled = 0;
	}

	private static void lifeMod(int amount) {
		int temp = life + amount;
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
	}

	public static void waveCompleted() {
		currentWave++;
	}

	public static void resetWaveCounter() {
		currentWave = 0;
	}

	public static void levelCompleted() {
		if ((currentLevel + 1) < levelName.Length) {
			currentLevel++;
		}
	}

	public static int currentScore() {
		if (ennemiesKilled == 0 && score != 0) {
			return score;
		} else {
			return ennemiesKilled;
		}
	}
}