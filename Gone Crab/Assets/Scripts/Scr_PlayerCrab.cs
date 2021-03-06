﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCrab : MonoBehaviour {

    #region Private References
    private Rigidbody rb;
    [SerializeField]
    private GameObject CameraBoom;
    [SerializeField]
    private Shell_indv MyShell;
    [SerializeField]
    private GameObject ShellSocket;
    [SerializeField]
    private Animator MyAnim;
    private Scr_Dialogue dialogueController;
    [SerializeField]
    private QuickStart quickstart;
    [SerializeField]
    private GameObject ExplosionPrefab;

    //[SerializeField]
    //private AnimationClip Walk;
    #endregion

    #region Serialized Fields
    [SerializeField]
    private float groundSpeed = 10.0f;
    [SerializeField]
    private float speedModifier = 10.0f;
    [SerializeField]
    private float maxVelocity = 50.0f;
    [SerializeField]
    private float shellessSpeedBoost = 10.0f;
    [SerializeField]
    private float turningSpeed = 10.0f;
    [SerializeField]
    private float shellSizeThreshold = 1.0f;
    [SerializeField]
    private float interactDistance = 1.0f;
    [SerializeField]
    private float growthRate = 1.0f;
    [SerializeField]
    private float minSize = 0.5f;
    [SerializeField]
    private float maxSize = 11.0f;
    [SerializeField]
    private float camScaleMod = 0.75f;
    [SerializeField]
    private float BurnTimerMax = 100.0f;
    [SerializeField]
    private float BurnDrainSpeed = 1.0f;
    [SerializeField]
    private float BurnHealSpeed = 10.0f;
    [SerializeField]
    private float FallDamageMinDist = 5.0f;
    [SerializeField]
    private float ShellLerpSpeed = 20.0f;
    [SerializeField]
    private float ShellLockDistance = 10.0f;
    [SerializeField]
    private float ShellPopForce = 10.0f;
    [SerializeField]
    private float growthRateScaleFactor = 1.0f;
    [SerializeField]
    private float interactForce = 10.0f;
    [SerializeField]
    private Vector3 yInteractAmount;
    [SerializeField]
    private Vector3 WaterResistanceVector = new Vector3(100, 10, 0);
    [SerializeField]
    private GameObject TailMesh;

    [SerializeField]
    private float CrabShelledWalkAnimSpeed = 1.5f;
    [SerializeField]
    private float CrabUnshelledWalkAnimSpeed = 2.0f;
    #endregion

    #region Private Variables
    private float BurnTimer;
    private bool bSaidHalfbakedLine = false;
    private bool bInAirLastFrame = false;
    private float StoredHeight = 0.0f;
    private string MyShellState;
    private bool ShellDoneLerp = false;

    private bool PControl = true;

    private bool toldtostartburning;
    private bool toldtostopburning;

    private float ControlX;
    private float ControlY;

    private float TimeTaken = 0.0f;

    public bool bPlayedTut01 = false;
    private bool bPlayedTut02 = false;

    [SerializeField]
    private GameObject Tut00;
    public GameObject Tut01;
    [SerializeField]
    private GameObject Tut02;
    #endregion

    public bool bControlLocked = false;

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialogueController = FindObjectOfType<Scr_Dialogue>();
        rb = this.GetComponent<Rigidbody>();
        SetCrabSize(0);
        BurnTimer = BurnTimerMax;
        Tut00.SetActive(true);
        Tut00.GetComponent<Animation>().Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!bControlLocked)
        {
            // Big boi found new home <3
            ShellInteract();
            // [Insert pun]
            OtherInteract();
            // Kicked out of home RIP
            ShellUpdate();
            // Grow up big and strong!
            GrowthByTime();
            // Ouch dats hot REEE
            UpdateBurn();
            // He did a bit humpty dumpty
            FallDamageUpdate();
            // Baby gon do the first worderinos <3 <3
            DialougeUpdate();
            // Check the ground
            CheckGround();
        }

        TimeTaken += Time.deltaTime;
        SaveStats();

        //Walk["Take001"].speed = 1 + (transform.localScale.x * 0.1f);

    }

    void FixedUpdate()
    {
        // Call Control function
        if (!bControlLocked)
        {
            Control();
        }
    }

    float CalcSpeed()
    {
        float isShelled = 0;

        if (!MyShell) { isShelled = 1.0f; }

        return ((groundSpeed + (groundSpeed * shellessSpeedBoost * isShelled)) * (this.transform.localScale.x * speedModifier));
    }

    // Updates the position of the crab by taking input from the player
    void Control()
    {
        bool bMoving = false;

        // TODO: Get this infor from our Input Script
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            ControlX = Input.GetAxis("Vertical") * -1.0f;
            ControlY = Input.GetAxis("Horizontal");
            bMoving = true;
        }
        MyAnim.SetBool("Walking", bMoving);
        Scr_soundmanager.Instance.ChangeWalkVol(bMoving);

        // Movement & Rotation
        if (bMoving)
        {
            Vector3 CamRot = CameraBoom.transform.eulerAngles;
            CamRot.x = 0;
            CamRot.y += 180;
            CamRot.z = 0;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler( 0, -90, 0) * Quaternion.Euler(CamRot) * Quaternion.Euler(new Vector3(0, Mathf.Atan2(ControlX, ControlY) * 180 / Mathf.PI, 0)), turningSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + this.transform.right * (CalcSpeed() * Time.deltaTime));
        }
    }

    // Handles Shell equipping and dropping when manually called by the player.
    void ShellInteract()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetAxis("ShellOn") > 0.25)
        {
            Debug.Log("ShellOn");
            PickupShell();
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetAxis("ShellOff") > 0.25)
        {
            Debug.Log("ShellOff");
            RemoveShell(false);
        }
    }

    void OtherInteract()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetAxis("ShellOn") > 0.25)
            Interactable();
    }

    void Interactable()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject obj in objects)
        {
            if (Vector3.Distance(this.transform.position, obj.transform.position) < interactDistance * this.transform.localScale.x)
            {
                obj.GetComponent<Rigidbody>().AddForce(((obj.transform.position - this.transform.position) + yInteractAmount) * (interactForce * obj.transform.localScale.x), ForceMode.Impulse);
                obj.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(obj.transform.position, 1);
            }
        }
    }

    void PickupShell()
    {
        if (MyShell == null)
        {
            Shell_indv currentNext = null;

            Shell_indv[] ShellsOnMap = FindObjectsOfType<Shell_indv>();
            foreach (Shell_indv shell in ShellsOnMap)
            {
                //DistCheck && Size Check
                if (Vector3.Distance(this.transform.position, shell.transform.position) < interactDistance * this.transform.localScale.x && shell.isAcceptable())
                {
                    if (shell.tag == "Boat")
                    {
                        BoardVessel(shell);
                        break;
                    }
                    else if(currentNext == null)
                    {
                        currentNext = shell;
                    }
                    else if(shell.transform.localScale.x > currentNext.transform.localScale.x)
                    {
                        currentNext = shell;
                    }
                }                
            }
            if(currentNext != null)
            {
                currentNext.gameObject.GetComponentInChildren<Collider>().enabled = false;
                currentNext.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                MyShell = currentNext;
                ShellDoneLerp = false;
                MyShellState = "";
                Scr_soundmanager.Instance.ShellAdded();
                if (Scr_AnalyticController.Analytics)
                {
                    Scr_AnalyticController.Analytics.ReportOnShell(MyShell.ShellState());
                    Scr_AnalyticController.Analytics.CheckTimeSpentNaked();
                }

                if(!bPlayedTut02)
                {
                    bPlayedTut02 = true;
                    Tut02.SetActive(true);
                    Tut02.GetComponent<Animation>().Play();
                }
            }
        }
    }

    void BoardVessel(Shell_indv Boat)
    {
        MyAnim.SetBool("Walking", false);
        bControlLocked = true;
        Debug.Log("board vessel called");
        Boat.gameObject.GetComponent<Scr_BoatShell>().CalledByPlayer();
        Invoke("EndGame1", 5);
        CameraBoom.GetComponentInChildren<Camera>().enabled = false;
    }

    // Todo: This is just shit and temp lmao
    void RemoveShell(bool bDestroyed)
    {
        if(MyShell != null)
        {
            if (TailMesh) { TailMesh.SetActive(true); }
            dialogueController.DisplayLine(7);
            if (bDestroyed)
            {
                MyShell.GetComponentInChildren<QuickStart>().enabled = true;
                Scr_soundmanager.Instance.Falling();
                Destroy(MyShell.gameObject,1);
                MyShell = null;
            }
            else
            {
                GameObject g = MyShell.gameObject;
                MyShell = null;
                g.gameObject.GetComponentInChildren<Collider>().enabled = true;
                g.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                g.GetComponent<Rigidbody>().AddForce(ShellSocket.transform.right * ShellPopForce);
                Scr_soundmanager.Instance.ShellPopped();
            }
        }
    }

    void ShellUpdate()
    {
        if (MyShell != null)
        {
            if (!ShellDoneLerp)
            {
                MyShell.gameObject.transform.position = Vector3.Lerp(MyShell.gameObject.transform.position, ShellSocket.transform.position, ShellLerpSpeed * Time.deltaTime);
                MyShell.gameObject.transform.rotation = Quaternion.Lerp(MyShell.gameObject.transform.rotation, ShellSocket.transform.rotation, ShellLerpSpeed * Time.deltaTime);

                if (Vector3.Distance(MyShell.gameObject.transform.position, ShellSocket.transform.position) < ShellLockDistance)
                    ShellDoneLerp = true;
            }
            else
            {
                if (TailMesh) { TailMesh.SetActive(false); }
                MyShell.gameObject.transform.position = ShellSocket.transform.position;
                MyShell.gameObject.transform.rotation = ShellSocket.transform.rotation;
            }

            MyAnim.speed = CrabShelledWalkAnimSpeed;
        }
        else
        {
            if (Scr_AnalyticController.Analytics)
                Scr_AnalyticController.Analytics.CurrentTimeSpentWithoutShell += Time.deltaTime;

            MyAnim.speed = CrabUnshelledWalkAnimSpeed;
        }
    }

    void SetCrabSize(float newSize)
    {
        float sizeNorm = Mathf.Clamp(newSize, minSize, maxSize);
        this.transform.localScale = Vector3.one * sizeNorm;
        CameraBoom.transform.localScale = Vector3.one * sizeNorm / camScaleMod;

        if (MyShell != null)
        {
            if (!MyShell.isAcceptable())
            {
                RemoveShell(true);
                if(Scr_AnalyticController.Analytics)
                    Scr_AnalyticController.Analytics.TimesOutgrownShell++;
                dialogueController.DisplayLine(4);
            }
        }
    }

    void GrowthByTime()
    {
        if (MyShell != null)
            SetCrabSize(this.transform.localScale.x + ((growthRate * this.transform.localScale.x * growthRateScaleFactor) * Time.deltaTime));
    }

    void UpdateBurn()
    {
        if (MyShell == null)
        {
            BurnTimer -= BurnDrainSpeed * Time.deltaTime;
            if (!toldtostartburning)
            {
                GetComponent<Scr_CrabColourLerp>().colourchangerate = (  BurnDrainSpeed /BurnTimerMax) ;

                GetComponent<Scr_CrabColourLerp>().BurnTime();
                toldtostartburning = true;
                toldtostopburning = false;
            }
            if (BurnTimer <= 0)
            {
                BurnTimer = 0;
                OnDie();
            }
            else if (BurnTimer < BurnTimerMax / 2 && !bSaidHalfbakedLine)
            {
                dialogueController.DisplayLine(5);
                bSaidHalfbakedLine = true;
            }
        }
        else
        {
            if (!toldtostopburning)
            {
                GetComponent<Scr_CrabColourLerp>().colourhealrate = GetComponent<Scr_CrabColourLerp>().colourchangerate * 2;
                  Debug.Log("calledheal from crabcolour");
                GetComponent<Scr_CrabColourLerp>().HealTime();
                toldtostopburning = true;
                toldtostartburning = false;
            }


            BurnTimer += BurnHealSpeed * Time.deltaTime;
            if (BurnTimer >= BurnTimerMax)
            {
                BurnTimer = BurnTimerMax;
            }
            else if (BurnTimer > BurnTimerMax / 2)
                bSaidHalfbakedLine = false;
        }
    }

    void FallDamageUpdate()
    {
        bool inAir = rb.velocity.y <= -2.0f;
        bool FallenTooFar = StoredHeight -this.transform.position.y >= FallDamageMinDist * this.transform.localScale.x / 2;

        if(inAir && FallenTooFar)
            dialogueController.DisplayLine(6);

        if (bInAirLastFrame && !inAir)
        {
            if (FallenTooFar)
            {
                RemoveShell(true);
                if (Scr_AnalyticController.Analytics)
                    Scr_AnalyticController.Analytics.TimesTakenFallDamage++;
                dialogueController.DisplayLine(4);
            } 
        }
        else if(inAir == false)
            StoredHeight = this.transform.position.y;

        bInAirLastFrame = inAir;
    }

    void DialougeUpdate()
    {
        if (MyShell != null)
        {
            if (MyShellState != MyShell.ShellState())
            {
                if (MyShell.ShellState() == "Spacious")
                {
                    dialogueController.DisplayLine(0);
                }
                else if (MyShell.ShellState() == "Average")
                {
                    dialogueController.DisplayLine(1);
                }
                else if (MyShell.ShellState() == "Tight")
                {
                    dialogueController.DisplayLine(2);
                }

                MyShellState = MyShell.ShellState();
            }
        }
    }

    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.transform.tag == "EndBound")
        {
            if (this.transform.localScale.x < maxSize - 1.0f)
            {
                dialogueController.DisplayLine(3);
            }
            else if (this.transform.localScale.x >= maxSize - 1.0f) 
            {
                otherObj.collider.isTrigger = true;
            }
        }
        if (otherObj.transform.tag == "Water")
        {
            rb.AddForce(WaterResistanceVector, ForceMode.Impulse);
            Debug.Log("Wet Boi");
        }
    }

    public float GetShellSize()
    {
        if (MyShell)
            return MyShell.gameObject.transform.localScale.x;
        else
            return -1.0f;
    }

    void OnDie()
    {
        RemoveShell(true);
        
        bControlLocked = true;
        PControl = false;
        quickstart.enabled = true;
        ExplosionPrefab.SetActive(true);
        Invoke("EndGame1", 2);
    }

    void EndGame1()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        this.GetComponent<SCR_GameEnder>().EndGame();
    }

    void SaveStats()
    {
        if (this.transform.localScale.x - 0.01f > 10.01)
            PlayerPrefs.SetString("Size", "Size: 10/10");
        else
            PlayerPrefs.SetString("Size", "Size: " + Mathf.Floor(this.transform.localScale.x - 0.01f).ToString() + "/10");

        if(TimeTaken >= 999.0f)
            PlayerPrefs.SetString("Time", "Time: 999s");
        else
            PlayerPrefs.SetString("Time", "Time: " + Mathf.Floor(TimeTaken).ToString() + "s");
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.tag == "Sand")
            {
                Scr_soundmanager.Instance.ChangeWalkClip(1);
            }
            else if (hit.transform.tag == "bigrock" || hit.transform.tag == "smallrock")
            {
                Scr_soundmanager.Instance.ChangeWalkClip(0);
            }
        }
    }
}
