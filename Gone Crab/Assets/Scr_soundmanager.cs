using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_soundmanager : MonoBehaviour {
    public static Scr_soundmanager Instance;
    public AudioSource VFX;
    public AudioClip shelloff;
    public AudioClip shellon;
    public AudioClip SizzleLoop; 

    //Use this for initialization
	void Start ()
    {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShellAdded()
    {
        VFX.PlayOneShot(shellon);
    }

    public void ShellPopped()
    {
        VFX.PlayOneShot(shelloff);
    }
}
