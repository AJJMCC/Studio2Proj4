using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_InputController : MonoBehaviour {

    #region Hidden In Inspector
    // WASD, Left Thumb Stick
    [HideInInspector]
    public bool i_LeftHorizontalAxisPressed = false, i_LeftHorizontalAxisReleased = false;
    [HideInInspector]
    public bool i_LeftVerticalAxisPressed = false, i_LeftVerticalReleased = false;
    [HideInInspector]
    public float i_LeftHorizontalAxis = 0, i_LeftVerticalAxis = 0;
    // Right Thumb Stick
    [HideInInspector]
    public bool i_RightHorizontalAxisPressed = false, i_RightHorizontalAxisReleased = false;
    [HideInInspector]
    public bool i_RightVerticalAxisPressed = false, i_RightVerticalReleased = false;
    [HideInInspector]
    public float i_RightHorizontalAxis = 0, i_RightVerticalAxis = 0;

    // Mouse Buttons, Controller Triggers
    [HideInInspector]
    public bool i_LeftMouseTriggerPressed = false, i_LeftMouseTriggerReleased = false, i_LeftMouseTriggerHeld = false;
    public bool i_RightMouseTriggerPressed = false, i_RightMouseTriggerReleased = false, i_RightMouseTriggerHeld = false;
    #endregion
	
	// Update is called once per frame
	void Update () {
        float tmpAxis;

        // Left Vertical Axis
        tmpAxis = Input.GetAxis("Vertical");
	}
}