using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_UIController : MonoBehaviour
{
    int currentSelection = 1;
    public Image selectImage;
    public Button[] menuButtons;
    //public float selectSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //selectImage.transform.localPosition = Vector3.MoveTowards(selectImage.transform.localPosition, menuButtons[currentSelection - 1].transform.localPosition, selectSpeed * Time.deltaTime);

        // These KeyDowns may need to be changed, didn't use axis to avoid using cooldown bool
		if (Input.GetKeyDown("w"))
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown("s"))
        {
            ChangeSelection(0);
        }

        if (Input.GetButtonDown("Submit"))
        {
            if (currentSelection == 1)
            {
                OnStart();
            }
            else if (currentSelection == 2)
            {
                OnOptions();
            }
            else if (currentSelection == 3)
            {
                OnCredits();
            }
            else if (currentSelection == 4)
            {
                OnQuit();
            }
        }
	}

    // Sets the current selection to the given parameter, moves the selection image to the current selection position
    public void MouseOver (int buttonNum)
    {
        currentSelection = buttonNum;
        selectImage.transform.localPosition = menuButtons[currentSelection - 1].transform.localPosition;
    }

    // Increases or decreases the current selection based on the given paramter, moves the selection image to the current selection position
    void ChangeSelection (int dir)
    {
        if (dir == 1 && currentSelection > 1)
        {
            currentSelection--;
        }
        else if (dir == 0 && currentSelection < 4)
        {
            currentSelection++;
        }
        selectImage.transform.localPosition = menuButtons[currentSelection - 1].transform.localPosition;
    }

    // Loads the main scene
    public void OnStart ()
    {
        SceneManager.LoadScene(1);
    }

    // Opens the options menu
    public void OnOptions ()
    {
        Debug.Log("There are no options yet.");
    }

    // Opens the credits screen
    public void OnCredits ()
    {
        Debug.Log("There are no credits yet.");
    }

    // Quits the application
    public void OnQuit ()
    {
        Application.Quit();
    }
}
