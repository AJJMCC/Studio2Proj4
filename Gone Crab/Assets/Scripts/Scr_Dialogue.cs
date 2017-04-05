using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Dialogue : MonoBehaviour
{
    public GameObject playerModel;
    public Canvas diaCanvas;
    public Text diaText;

    private bool isActive;
    public float diaCooldown;
    private float diaTimer;
    public float textFadeSpeed;

    // Arrays that store dialogue lines
    public string[] diaLines;
    public string[] spaciousShellLines;
    public string[] averageShellLines;
    public string[] tightShellLines;
    public string[] popShellLines;
    public string[] halfCookLines;
    public string fallingLine;

	// Use this for initialization
	void Start ()
    {
        diaCanvas = GetComponent<Canvas>();
        diaTimer = diaCooldown;
	}
	
	// Update is called once per frame
	void Update ()
    {
        TextAlpha();

        if (isActive && diaTimer > 0)
        {
            diaTimer -= Time.deltaTime;
        }
        else if (isActive && diaTimer <= 0)
        {
            isActive = false;
            diaTimer = diaCooldown;
        }
    }

    // Public function that is called from outside scripts, displays a random line from the given array chosen by the index parameter
    public void DisplayLine (int index)
    {
        if (index == 0)
        {
            diaText.text = spaciousShellLines[Random.Range(0, spaciousShellLines.Length - 1)];
        }
        else if (index == 1)
        {
            diaText.text = averageShellLines[Random.Range(0, averageShellLines.Length - 1)];
        }
        else if (index == 2)
        {
            diaText.text = tightShellLines[Random.Range(0, tightShellLines.Length - 1)];
        }
        else if (index == 3)
        {
            diaText.text = diaLines[0];
        }
        else if (index == 4)
        {
            diaText.text = popShellLines[Random.Range(0, popShellLines.Length - 1)];
        }
        else if (index == 5)
        {
            diaText.text = halfCookLines[Random.Range(0, halfCookLines.Length - 1)];
        }
        else if (index == 6)
        {
            diaText.text = fallingLine;
        }

        diaTimer = diaCooldown;
        isActive = true;
    }

    // Fades the text alpha in and out which is determined by the isActive bool
    void TextAlpha ()
    {
        if (isActive && diaText.color.a < 1)
        {
            Color alphaFade = diaText.color;
            alphaFade.a += textFadeSpeed * Time.deltaTime;
            diaText.color = alphaFade;
        }
        else if (!isActive && diaText.color.a > 0)
        {
            Color alphaFade = diaText.color;
            alphaFade.a -= textFadeSpeed * Time.deltaTime;
            diaText.color = alphaFade;
        }
    }
}
