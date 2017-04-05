using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrabTilt : MonoBehaviour {

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
        Vector3 v = this.gameObject.transform.position;

        Physics.Raycast(v + backLeft, Vector3.down, out lr);
        Physics.Raycast(v + backRight, Vector3.down, out rr);
        Physics.Raycast(v + frontLeft, Vector3.down, out lf);
        Physics.Raycast(v + frontRight, Vector3.down, out rf);

        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                ).normalized;

        upDir.y = 0;

        transform.up = upDir;

        Debug.DrawRay(rr.point, Vector3.up, Color.white, 1000);
        Debug.DrawRay(lr.point, Vector3.up, Color.white, 1000);
        Debug.DrawRay(lf.point, Vector3.up, Color.white, 1000);
        Debug.DrawRay(rf.point, Vector3.up, Color.white, 1000);
    }
}
