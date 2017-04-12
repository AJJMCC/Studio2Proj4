using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BoatShell : Shell_indv {

    private GameObject playercrab;
    [SerializeField]
    private Transform playerpos;

    private bool moving;
    


    public void CalledByPlayer()
    {
        playercrab = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("crabtoboat");
        moving = true;
        //call soundmanager boat noises
    }

     void Update()
    {
        if (moving)
        {
            transform.position += transform.forward * 0.1f;
               
        }
    }
    IEnumerator crabtoboat()
    {
        if (playercrab.transform.position != playerpos.position)
        {
            playercrab.transform.position = Vector3.Lerp(playercrab.transform.position, playerpos.position, 0.2f);

            if(playercrab.transform.rotation != playerpos.rotation)
            {
                playercrab.transform.rotation = Quaternion.Lerp(playercrab.transform.rotation, playerpos.rotation, 0.2f);
            }
        }
        yield return null;
    }

}
