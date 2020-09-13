using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

	private Animator anim;
	private Transform transform;

	public GameObject afterImageLeft;
	public GameObject afterImageRight;
	public float afterImageLifetime = 0.1f; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        transform = GetComponent<Transform>();
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
    			afterImage(afterImageLeft);
    		} else {
    			anim.Play("DashR");
    			afterImage(afterImageRight);
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

    private void afterImage(GameObject image) {
    	GameObject afterImage = Instantiate(image, transform.position, transform.rotation);
    	Destroy(afterImage, afterImageLifetime);
    }
}
