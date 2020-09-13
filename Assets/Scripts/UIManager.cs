using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    void Start()
    {
    	UIlifeUpdate();   
    }

    // Update is called once per frame
    void Update()
    {
 		//
    }

    public void UIlifeUpdate() {
       HealthManager.updateHealth(PlayerStats.playerLife());
        //slider.value = PlayerStats.playerLife();
    	//life.text = "Player Life: " + PlayerStats.playerLife();
    }

    public void UIplayerDead() {
    //	life.text = "Player is dead";
    }
}
