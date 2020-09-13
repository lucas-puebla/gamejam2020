using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyStatus : MonoBehaviour
{
    private Transform player;
    private Transform transform;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 1f;

    public int maxHealth = 5;
    private int health;
    private bool isAlive = true;

    private SpriteRenderer sr;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        movement = direction;
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    public void moveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void hit(int amount = 1) {
        health -= amount;
        if (health <= 0) {
            isDead();
        }
        changeColor();
        changeSpeed();
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

    private void changeColor() {
        float rate = ((float) health) / ((float) maxHealth);
        sr.color = new Color(rate, rate, 1, 1);
    }

    private void changeSpeed() {
        float rate = ((float) maxHealth) / ((float) health);
        moveSpeed = rate;
    }
}
