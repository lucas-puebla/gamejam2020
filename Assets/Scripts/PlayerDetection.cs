using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Animator ennemyAnim;
    public bool isAttacking;

    void Start() {
        ennemyAnim = GetComponentInParent<Animator>();
        InvokeRepeating("resetDetection", 0.5f, .8f);
    }

    void LateUpdate() {
        if (isAttacking) {
            ennemyAnim.Play("EnnemyAttack");
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
        }   
    }

    void resetDetection() {
        isAttacking = false;
    }

}

