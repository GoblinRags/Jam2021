using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFace : MonoBehaviour
{
    private Camera MainCam;
    public SpriteRenderer SR;
    private void Awake()
    {
        MainCam = Camera.main;
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (SR.isVisible)
            transform.LookAt(transform.position + MainCam.transform.rotation * Vector3.forward,
                MainCam.transform.rotation * Vector3.up);
    }
}
