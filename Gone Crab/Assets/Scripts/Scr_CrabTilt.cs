using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrabTilt : MonoBehaviour {

    [SerializeField]
    private Scr_PlayerCrab Crabref;

    [SerializeField]
    private float TiltSpeed;

    [SerializeField]
    private Vector3 backLeft;
    [SerializeField]
    private Vector3 backRight;
    [SerializeField]
    private Vector3 frontLeft;
    [SerializeField]
    private Vector3 frontRight;

    private RaycastHit lr;
    private RaycastHit rr;
    private RaycastHit lf;
    private RaycastHit rf;

    private Vector3 upDir;

    void Update()
    {
        if (!Crabref.bControlLocked)
        {
            float y = this.transform.rotation.eulerAngles.y;

            Vector3 v = this.gameObject.transform.position;

            float scale = Crabref.gameObject.transform.localScale.x;

            Physics.Raycast(v + backLeft * scale, Vector3.down, out lr);
            Physics.Raycast(v + backRight * scale, Vector3.down, out rr);
            Physics.Raycast(v + frontLeft * scale, Vector3.down, out lf);
            Physics.Raycast(v + frontRight * scale, Vector3.down, out rf);

            upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                     Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                     Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                     Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                    ).normalized;

            transform.up = Vector3.Lerp(transform.up, upDir, TiltSpeed * Time.deltaTime);

            //Reset Y Rot
            Vector3 newAngles = transform.rotation.eulerAngles;
            newAngles.y = y;
            Quaternion newQuat = Quaternion.identity;
            newQuat.eulerAngles = newAngles;
            this.transform.rotation = newQuat;

            Debug.DrawRay(rr.point, Vector3.up, Color.white);
            Debug.DrawRay(lr.point, Vector3.up, Color.white);
            Debug.DrawRay(lf.point, Vector3.up, Color.white);
            Debug.DrawRay(rf.point, Vector3.up, Color.white);
        }
    }
}
