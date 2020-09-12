using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	private Rigidbody2D rb;

	public float speed;

	private float velX;
	private float velY;

	private Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
    	velX = Input.GetAxisRaw("Horizontal") * speed;
        velY = Input.GetAxisRaw("Vertical") * speed;
    	//Debug.Log("hor: " + velX + "\nver: " + velY);

        movement = new Vector2(velX, velY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       	rb.velocity = movement;
    }


}
