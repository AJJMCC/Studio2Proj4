using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndManager : MonoBehaviour {


    [SerializeField]
    private Text time;

    [SerializeField]
    private Text size;


    //private string playertime;
    //private string playersize;

    // Use this for initialization
    void Start ()
    {
        time.text = PlayerPrefs.GetString("Size");
        size.text = PlayerPrefs.GetString("Time");

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void RePlay()
    {
        Application.LoadLevel("leveldesign");

    }


    public void ToMenu()
    {
        Application.LoadLevel("mainmenu");
    }
}
