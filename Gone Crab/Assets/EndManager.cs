using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void RePlay()
    {
        Application.LoadLevel("leveldesign");

    }


    public void ToMenu()
    {
        Application.LoadLevel("mainmenu");
    }
}
