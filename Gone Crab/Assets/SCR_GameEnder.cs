using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GameEnder : MonoBehaviour {

    public Material rend;
    private Color StartCol = Color.white;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndGame()
    {
        rend.SetColor("_Color", StartCol);
        Application.LoadLevel("End Scene");
    }
}
