using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Interactable : MonoBehaviour
{
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
    }

    private void PrettyLights()
    {
        if (Pdistance >= ActiveDistance)
        {
            rend.material.SetColor("_Color", Color.white);
        }

        if (Pdistance <= ActiveDistance)
        {
            rend.material.SetColor("_Color", purple);
        }
    }
}
