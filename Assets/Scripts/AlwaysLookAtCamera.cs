using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    private GameObject cameraToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = GameObject.Find("PlayerFighter");
    }

    void Update()
    {
        if (cameraToLookAt == null) return;

        transform.LookAt(cameraToLookAt.transform);
        this.transform.rotation = cameraToLookAt.transform.rotation;
    }
}
