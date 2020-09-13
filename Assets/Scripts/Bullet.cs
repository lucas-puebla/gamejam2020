using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject hitEffect;
	public float effectLifeTime = 0.3f;

    void OnCollisionEnter2D(Collision2D other) {
    	GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    	Destroy(effect, effectLifeTime);
    	Destroy(gameObject);
    }
}
