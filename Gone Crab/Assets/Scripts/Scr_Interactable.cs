using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Interactable : MonoBehaviour
{
    public float minSize;
    public float maxSize;

    public float size;

    private float Pdistance;
    public float ScaleActiveDist;
    private float ActiveDistance;
    private GameObject player;

    private Rigidbody rb;

    public float MassMultiplier;

    [SerializeField]
    private Renderer rend;

    public Color purple;
    public Color red;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.mass = transform.localScale.x * MassMultiplier;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ActiveDistance = ScaleActiveDist * player.transform.localScale.x;

        ChecksAgainstPlayer();
        PrettyLights();
    }

    private void ChecksAgainstPlayer()
    {
        Pdistance = Vector3.Distance(this.transform.position, player.transform.position);

        size = this.transform.localScale.x / player.transform.localScale.x;
    }

    public bool isAcceptable()
    {
        string shellV = ShellState();
        if (shellV == "UNACCEPTABLE")
        {
            return false;
        }
        else
            return true;
    }


    public string ShellState()
    {
        if (size <= maxSize && size >= minSize)
        {
            return "Interactable";
        }
        else
            return "UNACCEPTABLE";
    }


    private void PrettyLights()
    {

        if (Pdistance >= ActiveDistance)
        {
            rend.material.SetColor("_Color", Color.white);
        }

        string colourcheck = ShellState();

        if (Pdistance <= ActiveDistance)
        {
            if (colourcheck == "Interactable")
            {
                rend.material.SetColor("_Color", purple);
            }
           // else
               // rend.material.SetColor("_Color", red);
        }
    }
}
