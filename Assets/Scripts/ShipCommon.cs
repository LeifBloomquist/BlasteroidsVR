using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCommon : MonoBehaviour
{
    protected Rigidbody rb;
    public GameObject TorpedoPrefab;

    private DateTime lastshot_fired;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected void Update()
    {
        ;
    }

    protected bool FireATorpedo()
    {
        TimeSpan interval = DateTime.Now - lastshot_fired;

        if (interval.TotalMilliseconds > 500)
        {
            DoFireATorpedo();
            lastshot_fired = DateTime.Now;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DoFireATorpedo()
    {
        GameObject torpedo = Instantiate(TorpedoPrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
        torpedo.transform.SetParent(null);
        Rigidbody rbt = torpedo.GetComponent<Rigidbody>();
        rbt.AddForce(this.transform.forward * 100);
    }

    protected void Accelerate(float acceleration)
    {
        rb.AddForce(this.transform.forward * acceleration);
    }

    protected void ApplyBrakes(float strength)
    {
        rb.AddForce(-rb.velocity * strength);
    }

    protected void SlowRotation(float strength)
    {
        rb.AddTorque(-rb.angularVelocity * strength);
    }    
}
