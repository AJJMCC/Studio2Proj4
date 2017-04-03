using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;

    public float moveSpeed;
    public float maxVelocity;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetAxis("Vertical") != 0)
        {
            float speed = moveSpeed * Input.GetAxis("Vertical");
            rb.AddForce(player.transform.forward * speed);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            float speed = moveSpeed * Input.GetAxis("Horizontal");
            rb.AddForce(player.transform.right * speed);
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
}
