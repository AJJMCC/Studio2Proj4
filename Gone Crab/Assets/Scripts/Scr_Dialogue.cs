using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Dialogue : MonoBehaviour
{
    public GameObject playerModel;
    public Canvas diaCanvas;
    public Text diaText;
    public Image diaBG;

    private bool isActive;
    public float canvasPosCoEf;
    public float textScaleCoEf;
    public float diaCooldown;
    private float diaTimer;

    public string[] diaLines;

	// Use this for initialization
	void Start ()
    {
        diaCanvas = GetComponent<Canvas>();
        diaTimer = diaCooldown;
	}
	
	// Update is called once per frame
	void Update ()
    {
        diaCanvas.transform.localPosition = new Vector3(0, canvasPosCoEf, 0) * playerModel.transform.localScale.x;      // Set the canvas Y position to be relative to the player model scale
        diaBG.transform.localScale = playerModel.transform.localScale * textScaleCoEf;                                // Set the text scale to be relative to the player model scale

        TextAlpha();
        ImageAlpha();

        if (isActive && diaTimer > 0)
        {
            diaTimer -= Time.deltaTime;
        }
        else if (isActive && diaTimer <= 0)
        {
            isActive = false;
            diaTimer = diaCooldown;
        }

        if (Input.GetKeyDown("f"))
        {
            DisplayLine(0);
        }

        if (Input.GetKeyDown("g"))
        {
            DisplayLine(1);
        }

        if (Input.GetKeyDown("h"))
        {
            DisplayLine(2);
        }
    }

    public void DisplayLine (int index)
    {
        diaText.text = diaLines[index];             // Set the text string to the correct line
        isActive = true;                            // Enables the text alpha
    }

    void TextAlpha ()
    {
        if (isActive && diaText.color.a < 1)
        {
            Color alphaFade = diaText.color;
            alphaFade.a += Time.deltaTime;
            diaText.color = alphaFade;
        }
        else if (!isActive && diaText.color.a > 0)
        {
            Color alphaFade = diaText.color;
            alphaFade.a -= Time.deltaTime;
            diaText.color = alphaFade;
        }
    }

    void ImageAlpha ()
    {
        if (isActive && diaBG.color.a < 1)
        {
            Color alphaFade = diaBG.color;
            alphaFade.a += Time.deltaTime;
            diaBG.color = alphaFade;
        }
        else if (!isActive && diaBG.color.a > 0)
        {
            Color alphaFade = diaBG.color;
            alphaFade.a -= Time.deltaTime;
            diaBG.color = alphaFade;
        }
    }
}
