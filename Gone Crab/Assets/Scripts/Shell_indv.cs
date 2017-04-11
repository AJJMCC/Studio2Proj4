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
    private float ActiveDistance = 10;
    private GameObject player;
    
    [SerializeField]
    private Renderer rend; 

    private Color Green = new Color32(0,197,50,255);
    private Color Yellow = new Color32(197, 192, 0, 255);
    private Color Orange = new Color32(255, 132, 0, 255);
    private Color Red = new Color32(255,0,0,255);

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        ActiveDistance = ScaleActiveDist * player.transform.localScale.x;

        ChecksAgainstPlayer();
        PrettyLights();
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
        }

        string colourcheck = ShellState();

        if (Pdistance <= ActiveDistance)
        {
            if (colourcheck == "Spacious")
            {
                rend.material.SetColor("_Color", Green);
            }
           else  if (colourcheck == "Average")
            {
                rend.material.SetColor("_Color", Yellow);
            }
            else if (colourcheck == "Tight")
            {
                rend.material.SetColor("_Color", Orange);
            }
            else
            {
                rend.material.SetColor("_Color", Red);
            }
        }
        

        
    }
}
