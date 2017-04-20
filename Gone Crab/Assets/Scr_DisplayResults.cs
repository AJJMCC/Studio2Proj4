using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_DisplayResults : MonoBehaviour {

    public Text Size;
    public Text Time;

    // Use this for initialization
    void Start () {
        Size.text = PlayerPrefs.GetString("Size", "Size: 0/ 10");
        Time.text = PlayerPrefs.GetString("Time", "Time: 999s");
    }
}
