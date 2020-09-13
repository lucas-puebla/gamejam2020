using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{

	public Transform firePoint;
	public GameObject bulletPrefab;

	public float bulletForce = 1f;

	private Rigidbody2D rb;
	private GameObject bullet;

    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
        	Shoot();
        }
    }

    void Shoot() {
    	bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    	rb = bullet.GetComponent<Rigidbody2D>();
    	rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        source.Play();
    }
}
