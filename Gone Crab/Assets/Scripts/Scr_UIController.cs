using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_UIController : MonoBehaviour
{
    public bool isMainMenu = true;
    public bool resetOnStart = false;

    int currentSelection = 1;
    public bool isPaused = false;
    public Image selectImage;
    public Button[] menuButtons;
    public GameObject menu;
    public float selectCooldown = 0.2f;
    private float selectTimer;

    public Sprite ButtonUnhoveredImage;
    public Vector2 ButtonUnhoveredScale;
    public Sprite ButtonHoveredImage;
    public Vector2 ButtonHoveredScale;

    public Vector3 CursorOffset = new Vector3(10,0,0);

    public bool inOptionsMenu = false;

    public GameObject MenuRoot;

    private int Sensitivity = 3;
    private bool Inverted = false;

    public Image SensitivitySlider;
    public Sprite[] SensitivityFrames;
    public Image CheckMark;

    public Image Splash;

    // Use this for initialization
    void Start()
    {
        // Load Options
        Sensitivity = PlayerPrefs.GetInt("LookSensitivity", 3);
        int inv = PlayerPrefs.GetInt("Inverted", 0);
        if (inv > 0)
            Inverted = true;

        SetOptionsGraphics();

        MouseOver(1);

        if (SceneManager.GetActiveScene().buildIndex == 1)
            OnResume();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
        {
            if (Input.GetAxis("Vertical") > 0 && selectTimer <= Time.realtimeSinceStartup)
            {
                ChangeSelection(1);
            }
            else if (Input.GetAxis("Vertical") < 0 && selectTimer <= Time.realtimeSinceStartup)
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
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    InEndGame();
                }
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

    void MoveCursor(Vector3 newPos)
    {
        selectImage.transform.localPosition = newPos + CursorOffset;
        selectImage.GetComponent<Animation>().Play();
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
        else if (currentSelection == 5)
        {
            OnChangeSensitivity();
        }
        else if (currentSelection == 6)
        {
            OnToggleInverted();
        }
        else if (currentSelection == 7)
        {
            OnBackToMenu();
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

    // Checks the selected button and calls the corresponding function in the end game menu
    void InEndGame()
    {
        if (currentSelection == 1)
        {
            LoadScene(1);
        }
        else if (currentSelection == 2)
        {
            LoadScene(0);
        }
    }

    // Sets the current selection to the given parameter, moves the selection image to the current selection position
    public void MouseOver (int buttonNum)
    {
        foreach(Button b in menuButtons)
        {
            b.image.rectTransform.sizeDelta = ButtonUnhoveredScale;
            b.image.sprite = ButtonUnhoveredImage;
        }

        menuButtons[buttonNum-1].image.rectTransform.sizeDelta = ButtonHoveredScale;
        menuButtons[buttonNum-1].image.sprite = ButtonHoveredImage;
        menuButtons[buttonNum-1].GetComponent<Animation>().Play();

        currentSelection = buttonNum;
        MoveCursor(menuButtons[currentSelection - 1].transform.position);
    }

    // Increases or decreases the current selection based on the given paramter, moves the selection image to the current selection position
    void ChangeSelection (int dir)
    {
        selectTimer = Time.realtimeSinceStartup + selectCooldown;

        if (isMainMenu)
        {
            if (!inOptionsMenu)
            {
                if (dir == 1 && currentSelection > 1)
                {
                    currentSelection--;
                }
                else if (dir == 0 && currentSelection < 4)
                {
                    currentSelection++;
                }
            }
            else
            {
                if (dir == 1 && currentSelection > 5)
                {
                    currentSelection--;
                }
                else if (dir == 0 && currentSelection < 7)
                {
                    currentSelection++;
                }
            }
        }
        else
        {
            if (dir == 1 && currentSelection > 1)
            {
                currentSelection--;
            }
            else if (dir == 0 && currentSelection < 2)
            {
                currentSelection++;
            }
        }
        MoveCursor(menuButtons[currentSelection - 1].transform.position);

        MouseOver(currentSelection);
    }

    public void OptionsMenu(bool Entering)
    {
        if (Entering)
        {
            inOptionsMenu = true;
            currentSelection = 5;
            MenuRoot.GetComponent<Animation>().Play("MainMenuToOptionsTransit", PlayMode.StopAll);
            Splash.GetComponent<Animation>().Play("SplashIntoOptions", PlayMode.StopAll);
        }
        else
        {
            inOptionsMenu = false;
            currentSelection = 2;
            MenuRoot.GetComponent<Animation>().Play("OptionsToMainMenuTransit", PlayMode.StopAll);
        }
        MouseOver(currentSelection);
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
        OptionsMenu(true);
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
        //Time.timeScale = 0;
        FindObjectOfType<Scr_PlayerCrab>().bControlLocked = true;
    }

    // Deactivates the pause menu and sets the time scale to one
    public void OnResume ()
    {
        menu.SetActive(false);
        isPaused = false;
        //Time.timeScale = 1;
        FindObjectOfType<Scr_PlayerCrab>().bControlLocked = false;
    }

    // Adjusts the sensitivity
    public void OnChangeSensitivity()
    {
        Sensitivity++;
        if (Sensitivity > 5)
            Sensitivity = 0;

        PlayerPrefs.SetInt("LookSensitivity", Sensitivity);

        SetOptionsGraphics();
    }

    // Toggles Look Inversion
    public void OnToggleInverted()
    {
        Inverted = !Inverted;

        if (Inverted)
            PlayerPrefs.SetInt("Inverted", 1);
        else
            PlayerPrefs.SetInt("Inverted", 0);

        SetOptionsGraphics();
    }

    // Returns to the main menu
    public void OnBackToMenu()
    {
        OptionsMenu(false);
    }

    private void SetOptionsGraphics()
    {
        if (isMainMenu)
        {
            SensitivitySlider.sprite = SensitivityFrames[Sensitivity];
            CheckMark.enabled = Inverted;
        }
    }
}
