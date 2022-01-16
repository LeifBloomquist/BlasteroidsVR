using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    private GameObject cameraToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = GameObject.Find("OVRCameraRig");
    }

    void Update()
    {
        if (cameraToLookAt == null) return;

        transform.LookAt(cameraToLookAt.transform.position);
        this.transform.rotation = Quaternion.Inverse(cameraToLookAt.transform.rotation);
    }
}
