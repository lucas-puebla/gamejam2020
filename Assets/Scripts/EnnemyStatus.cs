using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyStatus : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;

    public int health = 5;
    private bool isAlive = true;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void hit(int amount = 1) {
        health -= amount;
        Debug.Log(health);
        if (health <= 0) {
            isDead();
        }
    }

    public void isDead() {
        isAlive = false;
        PlayerStats.ennemiesKilled += 1;
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            hit(PlayerStats.weaponDamage);
        }
    }
}