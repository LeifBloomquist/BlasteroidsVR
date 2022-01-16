using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusInputController : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource thruster;
    private AudioSource trim;
    private AudioSource laser;
    private bool laser_fired = false;
    private Color thruster_col;

    public float acceleration = 1;
    public float torque = 1;
    public GameObject TorpedoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        thruster = GameObject.Find("Thruster").GetComponent<AudioSource>();
        thruster_col = GameObject.Find("Thruster").GetComponent<Renderer>().material.color;
        trim = GameObject.Find("TrimControl").GetComponent<AudioSource>();
        laser = GameObject.Find("Laser").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        // Spacecraft pointing
        // returns a Vector2 of the primary(typically the Left) thumbstick’s current state.
        // (X/Y range of -1.0f to 1.0f)
        Vector2 pointing = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);   // Right controller
        rb.AddRelativeTorque(torque * pointing.y, torque * pointing.x,0);
        trim.volume = pointing.magnitude;

        // Thrust
        float thrust = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        rb.AddForce(this.transform.forward * thrust * acceleration);
        thruster.volume = thrust;
        thruster_col.a = 1 - thrust;

        // Brakes
        bool brakes = OVRInput.Get(OVRInput.Button.One);
        if (brakes)
        {
            rb.AddTorque(-rb.angularVelocity * 5);
            rb.AddForce(-rb.velocity * 5);
        }

        // Slow rotation
        bool brakes2 = OVRInput.Get(OVRInput.Button.Two);
        if (brakes2)
        {
            rb.AddTorque(-rb.angularVelocity * 10);
        }

        // Weapon
        float trigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        bool fire = trigger > 0.8;

        if (fire)
        {
            if (!laser_fired)
            { 
                FireATorpedo();
                laser.Play();
                laser_fired = true;
            }
        }
        else
        {
            laser_fired = false;
        }
    }

    void FireATorpedo()
    {
        GameObject torpedo = Instantiate(TorpedoPrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
        torpedo.transform.SetParent(null);
        Rigidbody rbt = torpedo.GetComponent<Rigidbody>();
        rbt.AddForce(this.transform.forward * 100);
    }
}
