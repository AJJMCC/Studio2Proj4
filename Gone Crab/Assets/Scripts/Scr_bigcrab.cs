using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_bigcrab : MonoBehaviour {

    private Animator thisanim;

    private GameObject playercrab;

    private float Pdist;

    private bool toldtorun;
    private bool running = false;
    private Vector3 movetopos; 


	// Use this for initialization
	void Start ()
    {
        playercrab = GameObject.FindGameObjectWithTag("Player");
        thisanim = this.GetComponent<Animator>();
        Vector3 movetopos = new Vector3(300, 150, -400);
	}
	


	// Update is called once per frame
	void Update ()
    {
        Pdist = Vector3.Distance(this.transform.position, playercrab.transform.position);

        if (Pdist <= 35 && playercrab.transform.localScale.x < 11)
        {
            thisanim.SetTrigger("Raise claws");
        }

        else if (Pdist <= 35 && playercrab.transform.localScale.x >= 11)
        {
            if (!toldtorun)
            {
                Run();
            }
           
        }
        if (running == true)
        {
            transform.Rotate(1  , 1, 1 );
            transform.position += Vector3.up;
        }

    }

    private void Run()
    {
        Destroy(gameObject, 3);
        toldtorun = true;
        running = true;
    }
}
