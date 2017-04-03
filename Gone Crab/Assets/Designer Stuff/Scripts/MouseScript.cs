using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public float zDistance;
    public float rotSpeed;
    public float camSpeed;

    public Transform myTransform;

	// Use this for initialization
	void Start ()
    {
        myTransform = GetComponent<Transform>();
	}

    void LateUpdate()
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            float speed = rotSpeed * Input.GetAxis("Mouse X");
            myTransform.Rotate((Vector3.up * speed) * Time.deltaTime);
        }

        Camera.main.transform.localPosition -= new Vector3(0, 0, 1) * camSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
