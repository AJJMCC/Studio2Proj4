using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public GameObject player;
    public float scaleSpeed;
    public float maxScale;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.transform.localScale.x <= maxScale)
        player.transform.localScale += (scaleSpeed* Vector3.one) * Time.deltaTime;
	}
}
