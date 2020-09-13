using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject hitEffect;
	public float effectLifeTime = 0.3f;

	private AudioSource source;

	void Start() {
		source = GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D other) {
    	GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    	source.Play();
    	Destroy(effect, effectLifeTime);
    	Destroy(gameObject);
    }
}
