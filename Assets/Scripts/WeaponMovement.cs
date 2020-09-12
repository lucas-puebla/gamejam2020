using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
	private Transform transform;

	//Mouse Movement
	public Camera cam;
	Vector3 mousePos;

	private float angle;
	private Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
     	transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
		PlayerStats.angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    }

    void FixedUpdate() {
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, PlayerStats.angle));
    }
}
