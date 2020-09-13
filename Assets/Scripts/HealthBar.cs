using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private Slider slider;

	public void Start() {
		slider = GetComponent<Slider>();
		InvokeRepeating("updateHealth", 0f, 0.3f);
	}

    public void updateHealth() {
    	slider.value = PlayerStats.playerLife();
	}
}
