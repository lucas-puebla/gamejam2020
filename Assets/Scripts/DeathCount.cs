using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
	public Text deathCount;
	public Text wave;

    // Start is called before the first frame update
    void Start()
    {
    	InvokeRepeating("updateDeathCount", 0f, 0.3f);
    	if (wave != null) {
			InvokeRepeating("updateWave", 0f, 1f);
    	}
        
    }

    private void updateDeathCount() {
    	deathCount.text = "x   " + PlayerStats.currentScore();
    }

    private void updateWave() {
    	wave.text = "WAVE    " + (PlayerStats.currentWave + 1);
    }

}
