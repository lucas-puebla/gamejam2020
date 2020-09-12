using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text life;

    // Start is called before the first frame update
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
    	life.text = "Player Life: " + PlayerStats.playerLife();
    }

    public void UIplayerDead() {
    	life.text = "Player is dead";
    }
}
