using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CameraScript : MonoBehaviour
{
    public GameObject FollowGO;

    public float sensitivityX = 5.0f;
    public float sensitivityY = 5.0f;

    public float minimumX = -360.0f;
    public float maximumX = 360.0f;

    public float minimumY = -5.0f;
    public float maximumY = 25.0f;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;

    private float Sensitivity = 3;
    private bool Inverted = false;

    void Start()
    {
        // Load Options
        Sensitivity = (PlayerPrefs.GetInt("LookSensitivity", 3) + 1) * 0.9f;

        sensitivityX = Sensitivity * sensitivityX;
        sensitivityY = Sensitivity * sensitivityY;

        int inv = PlayerPrefs.GetInt("Inverted", 0);
        if (inv > 0)
            Inverted = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        //if (!FindObjectOfType<Scr_PlayerCrab>().bControlLocked)
        //{
            rotAverageY = 0f;
            rotAverageX = 0f;

            float inv = 1.0f;
            if (Inverted)
                inv = -1.0f;

            rotationY -= (Input.GetAxis("Mouse Y") + (-Input.GetAxis("RightV")/4)) * sensitivityY * inv;
            rotationX += (Input.GetAxis("Mouse X") + Input.GetAxis("RightH")/2) * sensitivityX;

            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            rotArrayY.Add(rotationY);
            rotArrayX.Add(rotationX);

            if (rotArrayY.Count >= frameCounter)
            {
                rotArrayY.RemoveAt(0);
            }
            if (rotArrayX.Count >= frameCounter)
            {
                rotArrayX.RemoveAt(0);
            }

            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }

            rotAverageY /= rotArrayY.Count;
            rotAverageX /= rotArrayX.Count;

            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.forward);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        //}

        transform.position = FollowGO.transform.position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
