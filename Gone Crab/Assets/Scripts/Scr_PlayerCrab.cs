﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCrab : MonoBehaviour {

    #region Private References
    private Rigidbody rb;
    [SerializeField]
    private GameObject CameraBoom;
    [SerializeField]
    private GameObject MyShell;
    [SerializeField]
    private GameObject ShellSocket;
    #endregion

    #region Serialized Fields
    [SerializeField]
    private float groundSpeed = 10.0f;
    [SerializeField]
    private float maxVelocity = 50.0f;
    [SerializeField]
    private float turningSpeed = 10.0f;
    [SerializeField]
    private float shellSizeThreshold = 1.0f;
    [SerializeField]
    private float interactDistance = 1.0f;
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
        // Shell Interact
        ShellInteract();
        // Shell Update
        ShellUpdate();
    }

    // Updates the position of the crab by taking input from the player
    void Control()
    {
        // TODO: Get this infor from our Input Script (P = 0)
        float ControlX = Input.GetAxis("Vertical");
        float ControlY = Input.GetAxis("Horizontal") * -1.0f;

        // Add Input Movement
        //rb.MovePosition(rb.position + ((ControlVector * this.transform.forward) * groundSpeed * Time.deltaTime));
        // X Update
        rb.MovePosition(rb.position + this.transform.forward * (ControlY * groundSpeed * Time.deltaTime));
        // Y Update
        rb.MovePosition(rb.position + this.transform.right * (ControlX * groundSpeed * Time.deltaTime));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

        // Updating Rotation
        Vector3 targRot = new Vector3(0, 0, 0);
        if(ControlX != 0 || ControlY != 0)
        {
            targRot.y = CameraBoom.transform.rotation.eulerAngles.y;
        }
        else
        {
            targRot.y = this.transform.rotation.eulerAngles.y;
        }
        Quaternion myRot = this.transform.rotation;
        myRot.eulerAngles = Vector3.Lerp(this.transform.rotation.eulerAngles, targRot, turningSpeed * Time.deltaTime);
        this.transform.rotation = myRot;
    }

    // Handles Shell equipping and dropping when manually called by the player.
    void ShellInteract()
    {
        if (Input.GetMouseButtonDown(0))
            PickupShell();
        else if (Input.GetMouseButtonDown(1))
            RemoveShell();
    }

    void PickupShell()
    {
        if (MyShell == null)
        {
            GameObject[] ShellsOnMap = GameObject.FindGameObjectsWithTag("ShellPickup");
            foreach (GameObject shell in ShellsOnMap)
            {
                //DistCheck && Size Check
                if (Vector3.Distance(this.transform.position, shell.transform.position) < interactDistance && Mathf.Abs(shell.transform.localScale.x - this.transform.localScale.x) < shellSizeThreshold)
                {
                    shell.GetComponent<Collider>().enabled = false;
                    MyShell = shell;
                    break;
                }                
            }
        }
    }

    // Todo: This is just shit and temp lmao
    void RemoveShell()
    {
        if(MyShell != null)
        {
            Destroy(MyShell);
        }
    }

    void ShellUpdate()
    {
        if (MyShell != null)
        {
            MyShell.transform.position = ShellSocket.transform.position;
            MyShell.transform.rotation = ShellSocket.transform.rotation;
        }
    }
}
