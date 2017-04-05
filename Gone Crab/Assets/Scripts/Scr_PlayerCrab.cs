using System.Collections;
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
    #endregion

    #region Serialized Fields
    [SerializeField]
    private float groundSpeed = 10.0f;
    [SerializeField]
    private float speedModifier = 10.0f;
    [SerializeField]
    private float maxVelocity = 50.0f;
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
    #endregion

    #region Private Variables
    private float BurnTimer;
    #endregion

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        SetCrabSize(0);
        BurnTimer = BurnTimerMax;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Big boi found new home <3
        ShellInteract();
        // Kicked out of home RIP
        ShellUpdate();
        // Grow up big and strong!
        GrowthByTime();
        // Ouch dats hot REEE
        UpdateBurn();
    }

    void FixedUpdate()
    {
        // Call Control function
        Control();
    }

    // Updates the position of the crab by taking input from the player
    void Control()
    {
        // TODO: Get this infor from our Input Script (P = 0)
        float ControlX = Input.GetAxis("Vertical");
        float ControlY = Input.GetAxis("Horizontal") * -1.0f;

        // Add Input Movement
        // X Update
        rb.MovePosition(rb.position + this.transform.forward * (ControlY * (groundSpeed * (this.transform.localScale.x * speedModifier)) * Time.deltaTime));
        // Y Update
        rb.MovePosition(rb.position + this.transform.right * (ControlX * (groundSpeed * (this.transform.localScale.x * speedModifier)) * Time.deltaTime));
        // Clamp Vel
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity * (this.transform.localScale.x * speedModifier));

        // Updating Rotation
        Quaternion t = Quaternion.identity;
        if(ControlX != 0 || ControlY != 0)
        {
            t = CameraBoom.transform.rotation;
        }
        else
        {
            t = this.transform.rotation;
        }
        t = Quaternion.Lerp(this.transform.rotation, t, turningSpeed * Time.deltaTime);
        Vector3 r = t.eulerAngles;
        r.x = 0;
        r.z = 0;
        t.eulerAngles = r;
        this.transform.rotation = t;
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
            Shell_indv[] ShellsOnMap = FindObjectsOfType<Shell_indv>();
            foreach (Shell_indv shell in ShellsOnMap)
            {
                //DistCheck && Size Check
                if (Vector3.Distance(this.transform.position, shell.transform.position) < interactDistance * this.transform.localScale.x && shell.isAcceptable())
                {
                    shell.gameObject.GetComponent<Collider>().enabled = false;
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
            Destroy(MyShell.gameObject);
        }
    }

    void ShellUpdate()
    {
        if (MyShell != null)
        {
            MyShell.gameObject.transform.position = ShellSocket.transform.position;
            MyShell.gameObject.transform.rotation = ShellSocket.transform.rotation;
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
                RemoveShell();
        }
    }

    void GrowthByTime()
    {
        if (MyShell != null)
            SetCrabSize(this.transform.localScale.x + (growthRate * Time.deltaTime));
    }

    void UpdateBurn()
    {
        Debug.Log(BurnTimer);
        if (MyShell == null)
        {
            BurnTimer -= BurnDrainSpeed * Time.deltaTime;
            if(BurnTimer <= 0)
            {
                BurnTimer = 0;
                Debug.Log("KILL");
            }
        }
        else
        {
            BurnTimer += BurnHealSpeed * Time.deltaTime;
            if (BurnTimer >= BurnTimerMax)
            {
                BurnTimer = BurnTimerMax;
            }
        }
    }
}
