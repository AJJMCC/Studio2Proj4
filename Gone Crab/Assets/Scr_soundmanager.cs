using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_soundmanager : MonoBehaviour {
    public static Scr_soundmanager Instance;
    public AudioSource VFX;
    public AudioSource walkSound;
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
        if (walkType == 0 && walkSound.clip != rockWalk)
        {
            walkSound.clip = rockWalk;
            walkSound.Play();
        }
        else if (walkType == 1 && walkSound.clip != sandWalk)
        {
            walkSound.clip = sandWalk;
            walkSound.Play();
        }
    }

    // Lerp the audio source volume between 0 and 1 depending on whether or not the player in moving
    public void ChangeWalkVol(bool isWalking)
    {
        if (isWalking && walkSound.volume != 1)
        {
            walkSound.volume = 1;
        }
        else if (!isWalking && walkSound.volume != 0)
        {
            walkSound.volume = 0;
        }
    }
}
