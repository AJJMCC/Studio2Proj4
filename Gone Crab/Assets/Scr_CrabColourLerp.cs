using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrabColourLerp : MonoBehaviour {

    public Material rend;
    private Color StartCol = Color.white;
    private Color EndCol = Color.black;
    private Color CurrentColor;
    private bool Burn;
    public float colourchangerate;
    public float colourhealrate;
    public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
    
    
    
	void Start ()
    {
        //rend = this.GetComponent<Renderer>();
        //StartCol =rend.GetColor("_Color");
    }

    private void Update()
    {
      
          
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
        //StopCoroutine("colorheal");
        //StartCoroutine("colorheal");
        StopCoroutine("colorburn");
        rend.SetColor("_Color", StartCol);
    }


    IEnumerator colorburn()
    {
        float percentthru = 0;
        //need to sync this up with the time it takes to burn. 
       while(CurrentColor != EndCol)
        {
            Debug.Log("startedcolourlerp");
            CurrentColor = Color.Lerp(StartCol, EndCol, curve.Evaluate(percentthru));
            rend.SetColor("_Color", CurrentColor);
            percentthru += colourchangerate *  Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator colorheal()
    {
        float percentthru = 0;
        while (CurrentColor != StartCol)
        {
            Debug.Log("startedcolourheal");
            CurrentColor = Color.Lerp(EndCol, StartCol, curve.Evaluate(percentthru));
            rend.SetColor("_Color", CurrentColor);
            percentthru += colourhealrate * Time.deltaTime;
            yield return null;

        }
    }
}
