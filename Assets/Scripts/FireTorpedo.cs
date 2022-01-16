using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTorpedo : MonoBehaviour
{
    public GameObject TorpedoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            FireATorpedo();
        }
    }

    void FireATorpedo()
    {
        GameObject torpedo = Instantiate(TorpedoPrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
        torpedo.transform.SetParent(null);
        Rigidbody rb = torpedo.GetComponent<Rigidbody>();        
        rb.AddForce(this.transform.forward * 100);
    }
}
