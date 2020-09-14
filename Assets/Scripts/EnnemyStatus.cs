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

    // animation
    public float colorRate = 1f;
    public float speedRate = 5f;
    public float animSpeedRate = 2f;
    // TODO resize ennemy
    // public float vertSize = 1f;
    // public float horSize = 1f;

    // mechanics
    public GameObject perceptionPoint;
    private CircleCollider2D ennemyPerception;

    private Animator anim;
    private AudioSource source;
    private SpriteRenderer sr;

    void Start()
    {
        health = maxHealth;

        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        sr = GetComponent<SpriteRenderer>();
        
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform = GetComponent<Transform>();
        
        source = GetComponent<AudioSource>();

        ennemyPerception = perceptionPoint.GetComponent<CircleCollider2D>();
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
        changeAnimSpeed();
        changeSize();
    }

    public void isDead() {
        isAlive = false;
        PlayerStats.ennemiesKilled += 1;;
        Destroy(gameObject);
    }

    public int getHealth() {
        return health;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            hit(PlayerStats.weaponDamage);
        }
    }

    private void changeColor() {
        float rate = ((float) health) / ((float) maxHealth) * colorRate;
        sr.color = new Color(rate, rate, 1, 1);
    }

    private void changeSpeed() {
        float rate = ((float) maxHealth) / ((float) health) * speedRate;
        moveSpeed = rate;
    }

    private void changeAnimSpeed() {
        float rate = ((float) maxHealth) / ((float) health) * animSpeedRate;
        anim.speed = rate;
    }

    private void changeSize() {
        // TODO
        // float vertRate = ((float) maxHealth) / ((float) health) * vertSize;
        // float horRate = ((float) maxHealth) / ((float) health) * horSize;
        // Debug.Log(vertRate + ", " + horRate);
        // sr.size += new Vector2(vertRate, horRate);
    }

}
