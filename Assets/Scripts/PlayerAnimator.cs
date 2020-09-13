using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

	private Animator anim;
	private Transform transform;
	private SpriteRenderer spriteRend;

	public GameObject afterImageLeft;
	public GameObject afterImageRight;
	public float afterImageLifetime = 0.1f;

	public int dashOptimizer = 5;
	private int counter;
	
	public int blinkCounter = 2;
	private Color normal;
	private Color appear;
	private Color disappear;
	private bool isAppeared = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        transform = GetComponent<Transform>();
        appear = new Color(1, 1, 1, 1);
        disappear = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        animator();
    }


    private void animator() {

    	if (PlayerStats.invincibleTimer.isEnabled()) {
    		if (counter % blinkCounter == 0) {
    			if (isAppeared) {
    				spriteRend.color = disappear;
    				isAppeared = false;
    			} else {
					spriteRend.color = appear;
    				isAppeared = true;
    			}
    		}
    	} else if (!isAppeared) {
    		spriteRend.color = appear;
    		isAppeared = true;
    	}

    	if (PlayerStats.isDash) {
    		if (PlayerStats.angle >= 0 || PlayerStats.angle < -180) {
    			anim.Play("DashL");
    			if (counter % dashOptimizer == 0) {
	    			afterImage(afterImageLeft);
    			}
    		} else {
    			anim.Play("DashR");
    			if (counter % dashOptimizer == 0) {
    				afterImage(afterImageRight);
    			}
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
	    if (counter == 10) {
	    	counter = 0;
	    }
	    counter++;
    }

    private void afterImage(GameObject image) {
    	// float dashRate = (PlayerStats.dashTimer.getMaxTime() - PlayerStats.dashTimer.getRemTime()) / PlayerStats.dashTimer.getMaxTime();
    	// float alpha = Mathf.Lerp(0.1f, 1f, dashRate);
    	// float blue = Mathf.Lerp(0.5f, 1f, dashRate);
    	// image.GetComponent<SpriteRenderer>().color = new Color(0, 0, blue, alpha);
    	GameObject afterImage = Instantiate(image, transform.position, transform.rotation);
    	Destroy(afterImage, afterImageLifetime);
    }
}
