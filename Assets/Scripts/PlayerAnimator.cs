using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        animator();
    }


    private void animator() {

    	if (PlayerStats.isDash) {
    		if (PlayerStats.angle >= 0 || PlayerStats.angle < -180) {
    			anim.Play("DashL");
    		} else {
    			anim.Play("DashR");
    		}
    	} else {
	    	if (PlayerStats.isIdle) {
	    		anim.Play("Idle");
	    	} else {
	    		if (PlayerStats.angle >= 45f || PlayerStats.angle < -225f) {
	    			anim.Play("Walkleft");
	    		} else if (PlayerStats.angle >= -45f && PlayerStats.angle < 45f) {
	    			anim.Play("Walkback");
	    		} else if (PlayerStats.angle >= -135f && PlayerStats.angle < -45f) {
	    			anim.Play("Walkright");
	    		} else if (PlayerStats.angle >= -225f && PlayerStats.angle < -135f) {
	    			anim.Play("Walkfront");
	    		}
	    	}
	    }
    }
}
