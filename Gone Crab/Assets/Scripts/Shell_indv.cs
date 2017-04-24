using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_indv : MonoBehaviour {

    private float Mintight = 0.7f;

    private float Minavg = 0.9f;

    private float MinSpac = 1.1f;

    private float Maxspac = 1.3f;

    private float Size;
    private float Pdistance;
    public float ScaleActiveDist;
    private float ActiveDistance = 20;
    private GameObject player;
    
    [SerializeField]
    private Renderer rend; 
    [SerializeField]
    private float massModifier;

    private Rigidbody rb;

    [SerializeField]
    private GameObject ShellMesh;

    private Color Green = new Color32(137,255,114,255);
    private Color Yellow = new Color32(197, 192, 0, 255);
    private Color Orange = new Color32(255, 132, 0, 255);
    private Color Red = new Color32(255,114,114,255);

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.mass = this.transform.localScale.x * massModifier;
        rend = this.GetComponentInChildren<Renderer>();
    }
	
	void Update ()
    {
        ActiveDistance = ScaleActiveDist * player.transform.localScale.x;

        ChecksAgainstPlayer();
        PrettyLights();

        if(Pdistance < ActiveDistance && !player.GetComponent<Scr_PlayerCrab>().bPlayedTut01)
        {
            player.GetComponent<Scr_PlayerCrab>().bPlayedTut01 = true;
            player.GetComponent<Scr_PlayerCrab>().Tut01.SetActive(true);
            player.GetComponent<Scr_PlayerCrab>().Tut01.GetComponent<Animation>().Play();
        }
	}

    private void ChecksAgainstPlayer()
    {
        Pdistance = Vector3.Distance(this.transform.position, player.transform.position);

        Size = this.transform.localScale.x / player.transform.localScale.x;
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
        if (Size <= Maxspac && Size >= MinSpac )
        {
            return "Spacious";
        }
        if (Size <= MinSpac && Size >= Minavg)
        {
            return "Average";
        }
        if (Size <= Minavg && Size >= Mintight)
        {
            return "Tight";
        }

        else
        return "UNACCEPTABLE";
    }


    private void PrettyLights()
    {
       
        if (Pdistance >= ActiveDistance)
        {
            rend.material.SetColor("_Color", Color.white);
            ShellMesh.GetComponent<cakeslice.Outline>().enabled = false;
        }

        string colourcheck = ShellState();
        if (rend != null)
        {
            if (Pdistance <= ActiveDistance)
            {

                if (colourcheck == "Spacious")
                {
                    ShellMesh.GetComponent<cakeslice.Outline>().enabled = true;
                    ShellMesh.GetComponent<cakeslice.Outline>().color = 0;
                   // rend.material.SetColor("_Color", Green);
                }
                else if (colourcheck == "Average")
                {
                    ShellMesh.GetComponent<cakeslice.Outline>().enabled = true;

                    ShellMesh.GetComponent<cakeslice.Outline>().color = 1;
                   // rend.material.SetColor("_Color", Yellow);
                }
                else if (colourcheck == "Tight")
                {
                    ShellMesh.GetComponent<cakeslice.Outline>().enabled = true;

                    ShellMesh.GetComponent<cakeslice.Outline>().color = 2;
                    //rend.material.SetColor("_Color", Orange);
                }
                else
                {
                    ShellMesh.GetComponent<cakeslice.Outline>().enabled = false;
                   // rend.material.SetColor("_Color", Red);
                }
            }
        }
        

        
    }
}
