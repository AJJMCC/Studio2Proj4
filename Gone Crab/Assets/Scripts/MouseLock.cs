using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseLock : MonoBehaviour
{
    public bool mouseLocked = true;

    // Update is called once per frame
    void Update()
    {
        if (mouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mouseLocked = !mouseLocked;
        }
    }
}