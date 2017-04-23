using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BoatShell : Shell_indv {

    [SerializeField]
    private GameObject playercrab;
    [SerializeField]
    private Transform playerpos;

    private bool moving;

    [SerializeField]
    private Camera boatcam;
    [SerializeField]
    private Camera maincam;

    void Start()
    {
        boatcam.enabled = false;
        maincam = Camera.main;
    }

    public void CalledByPlayer()
    {
        CanvasFlash.Instance.Flash = true;
        playercrab = GameObject.FindGameObjectWithTag("Player");
       // StartCoroutine("crabtoboat");
        moving = true;
        Debug.Log("boat recieved call");
        //call soundmanager boat noises

        boatcam.enabled = true;
        playercrab.transform.position = playerpos.position;
        playercrab.transform.rotation = playerpos.rotation;
        playercrab.transform.SetParent(playerpos);
        boatcam = Camera.main;
       
        Invoke("TurnOffFlash", 0.1f);
        Invoke("TurnOnSlowFlash", 1.6f);
    }


    void TurnOffFlash()
    {
        CanvasFlash.Instance.Flash = false;
    }

    void TurnOnSlowFlash()
    {
        CanvasFlash.Instance.SlowFlash = true;
    }

     void Update()
    {
        if (moving)
        {
            transform.position -= transform.forward * 0.2f;
            playercrab.transform.position = playerpos.position;

        }
    }
    IEnumerator crabtoboat()
    {

        Debug.Log("boat started coroutine");

        if (playercrab.transform.position != playerpos.position)
        {
            playercrab.transform.position = Vector3.Lerp(playercrab.transform.position, playerpos.position, 0.3f * Time.deltaTime);

            if(playercrab.transform.rotation != playerpos.rotation)
            {
                playercrab.transform.rotation = Quaternion.Lerp(playercrab.transform.rotation, playerpos.rotation, 0.2f * Time.deltaTime);
            }
            yield return null;
        }
       
    }

}
