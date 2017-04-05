using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrabColourLerp : MonoBehaviour {

    private Renderer rend;
    private Color StartCol;
    private Color EndCol = Color.black;
    private Color CurrentColor;
    private bool Burn;
    
    
	void Start ()
    {
        rend = this.GetComponent<Renderer>();
        StartCol =rend.material.GetColor("_Color");
    }
	
	

    public void BurnTime()
    {
        //start the lerp to burn
        StopCoroutine("colorburn");
        StartCoroutine("colorburn");
    }

    public void HealTime()
    {

        //start the lerp to heal
        StopCoroutine("colorheal");
        StartCoroutine("colorheal");
    }


    IEnumerator colorburn()
    {
        //need to sync this up with the time it takes to burn. 
        CurrentColor = Color.Lerp(StartCol, EndCol, 0.1f);
        rend.material.SetColor("_Color", CurrentColor);

        yield return null;
    }

    IEnumerator colorheal()
    {
        CurrentColor = Color.Lerp(EndCol, StartCol, 0.5f);
        rend.material.SetColor("_Color", CurrentColor);

        yield return null;
    }
}
