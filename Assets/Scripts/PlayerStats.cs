using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
	private static float maxLife = 3f;
	private static float life = maxLife;
	private static bool isAlive = true;

	public static void lifeLost(float amount = 1f) {
		lifeMod(-amount);
	}

	public static void lifeRecovered(float amount = 1f) {
		lifeMod(amount);
	}

	public static bool playerAlive() {
		return isAlive;
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
				isAlive = false;
			} else {
				life = temp;
			}
		}
	}
}