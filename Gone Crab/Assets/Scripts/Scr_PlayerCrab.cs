using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCrab : MonoBehaviour {

    #region Private References
    private Rigidbody rb;
    #endregion

    #region Serialized Fields
    [SerializeField]
    private float groundSpeed = 10.0f;
    [SerializeField]
    public float maxVelocity = 50.0f;
    #endregion


    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void FixedUpdate()
    {
        // Call Control function
        Control();
    }

    // Updates the position of the crab by taking input from the player
    void Control()
    {
        Vector3 ControlVector = new Vector3(0,0,0);
        // TODO: Get this infor from our Input Script (P = 0)
        ControlVector.x = Input.GetAxis("Vertical");
        ControlVector.z = Input.GetAxis("Horizontal") * -1.0f;

        // Add Input Movement
        rb.MovePosition(rb.position + (ControlVector * groundSpeed * Time.deltaTime));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
}
