using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressSand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision collider)
    {
        if (collider.collider.tag == "Player")
        {
            if (collider.transform.localScale.x >= this.transform.localScale.x/2)
            {
                this.GetComponent<QuickStart>().enabled = true;
            }
        }


    }
}
