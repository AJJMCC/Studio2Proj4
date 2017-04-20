using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_soundmanager : MonoBehaviour {
    public static Scr_soundmanager Instance;
    public AudioSource VFX;
    public AudioClip shelloff;
    public AudioClip shellon;
    public AudioClip SizzleLoop;
    public AudioClip fallingSound;
    public AudioClip sandWalk;
    public AudioClip rockWalk;

    //Use this for initialization
	void Start ()
    {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		//if (Input.GetKeyDown("w"))
  //      {
  //          ChangeWalkClip(0);
  //      }
  //      else if (Input.GetKeyDown("s"))
  //      {
  //          ChangeWalkClip(1);
  //      }
	}

    // Plays the shell on clip
    public void ShellAdded()
    {
        VFX.PlayOneShot(shellon);
    }

    // Plays the shell off clip
    public void ShellPopped()
    {
        VFX.PlayOneShot(shelloff);
    }

    // Plays the falling sound clip
    public void Falling()
    {
        VFX.PlayOneShot(fallingSound);
    }

    // Checks what the current clip is and changes it accordingly
    public void ChangeWalkClip(int walkType)
    {
        if (walkType == 0)
        {
            VFX.clip = rockWalk;
            VFX.Play();
        }
        else
        {
            VFX.clip = sandWalk;
            VFX.Play();
        }
    }

    public void WalkVolume(float walkSpeed)
    {
        VFX.volume = walkSpeed;
    }
}
