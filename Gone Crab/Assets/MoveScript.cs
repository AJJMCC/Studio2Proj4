using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed;

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
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            float speed = moveSpeed * Input.GetAxis("Horizontal");
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
