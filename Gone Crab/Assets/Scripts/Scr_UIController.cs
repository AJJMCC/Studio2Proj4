using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_UIController : MonoBehaviour
{
    int currentSelection = 1;
    bool isPaused = false;
    public Image selectImage;
    public Button[] menuButtons;
    public GameObject menu;
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

        // Calls the select function corresponding to the current level when submit is pressed
        if (Input.GetButtonDown("Submit"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                MainMenuSelect();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                InLevelSelect();
            }
        }

        // Pauses and unpauses the main level when cancel is pressed
        if (Input.GetButtonDown("Cancel"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && isPaused)
            {
                OnResume();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1 && !isPaused)
            {
                OnPause();
            }
        }
    }

    // Checks the selected button and calls the corresponding function in the main menu
    void MainMenuSelect ()
    {
        if (currentSelection == 1)
        {
            LoadScene(1);
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

    // Checks the selected button and calls the corresponding function in the pause menu
    void InLevelSelect ()
    {
        if (currentSelection == 1)
        {
            OnResume();
        }
        else if (currentSelection == 2)
        {
            LoadScene(0);
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
    public void LoadScene (int index)
    {
        SceneManager.LoadScene(index);
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

    // Activates the pause menu and sets time scale to zero
    public void OnPause ()
    {
        menu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    // Deactivates the pause menu and sets the time scale to one
    public void OnResume ()
    {
        menu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
