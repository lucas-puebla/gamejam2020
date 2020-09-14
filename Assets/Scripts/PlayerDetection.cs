using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Animator ennemyAnim;
    public bool isAttacking;
    private EnnemyStatus ennemy;

    public float animTime = 1f;
    private Timer animTimer;

    void Start() {
        ennemy = GetComponentInParent<EnnemyStatus>();
        ennemyAnim = GetComponentInParent<Animator>();
        animTimer = new Timer(animTime);
    }

    void Update() {
        if (isAttacking && !animTimer.isEnabled()) {
            isAttacking = false;
        }
        animTimer.countDown();
    }

    void LateUpdate() {
        if (isAttacking) {
            if (ennemy.getHealth() < 3) {
                ennemyAnim.Play("EnnemyRage");
            } else {    
                ennemyAnim.Play("EnnemyAttack");
            }
        } else {
            ennemyAnim.Play("EnnemyIdle");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !isAttacking) {
            isAttacking = true;
        }        
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !isAttacking) {
            ennemyAnim.Play("EnnemyAttack");
            isAttacking = true;
            animTimer.reset();
        }   
    }
}

