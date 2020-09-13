using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
	public Text text;

    // Start is called before the first frame update
    void Start()
    {
    	InvokeRepeating("updateDeathCount", 0f, 0.3f);
        
    }

    private void updateDeathCount() {
    	text.text = "x   " + PlayerStats.ennemiesKilled;
    }


}
