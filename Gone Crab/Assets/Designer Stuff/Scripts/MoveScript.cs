using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Scr_Dialogue dialogueScr;
    Shell_indv shellScr;

    public GameObject player;
    public GameObject playerModel;
    public GameObject dialogue;
    public GameObject myShell;
    Rigidbody rb;

    public string shellState;
    public float moveSpeed;
    public float maxVelocity;

	// Use this for initialization
	void Start ()
    {
        dialogueScr = dialogue.GetComponent<Scr_Dialogue>();
        shellScr = myShell.GetComponent<Shell_indv>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

        if (shellState != shellScr.ShellState())
        {
            if (shellScr.ShellState() == "Spacious")
            {
                dialogueScr.DisplayLine(0);
            }
            else if (shellScr.ShellState() == "Average")
            {
                dialogueScr.DisplayLine(1);
            }
            else if (shellScr.ShellState() == "Tight")
            {
                dialogueScr.DisplayLine(2);
            }

            shellState = shellScr.ShellState();
        }
    }

    // Function that controls player movement
    void Movement ()
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
    }

    void OnCollisionEnter (Collision otherObj)
    {
        if (otherObj.transform.tag == "EndBound")
        {
            if (playerModel.transform.localScale.x < 11)
            {
                dialogueScr.DisplayLine(3);
            }
            else if (playerModel.transform.localScale.x >= 11)
            {
                otherObj.collider.isTrigger = true;
            }
        }
    }
}
