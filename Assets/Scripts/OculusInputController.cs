using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusInputController : ShipCommon
{
    private AudioSource thruster;
    private AudioSource trim;
    private AudioSource laser;
    private bool laser_fired = false;
    private Color thruster_col;

    public float acceleration = 1;
    public float torque = 1;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        thruster = GameObject.Find("Thruster").GetComponent<AudioSource>();
        thruster_col = GameObject.Find("Thruster").GetComponent<Renderer>().material.color;
        trim = GameObject.Find("TrimControl").GetComponent<AudioSource>();
        laser = GameObject.Find("Laser").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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

        // Brakes and Slow Rotation
        bool brakes = OVRInput.Get(OVRInput.Button.One);
        if (brakes)
        {
            ApplyBrakes(5);
            SlowRotation(8);
        }

        // Always Slow rotation, faster if button is pressed
        bool brakes2 = OVRInput.Get(OVRInput.Button.Two);
        if (brakes2)
        {
            SlowRotation(10);
        }
        else
        {
            SlowRotation(3);
        }
 
        // Weapon
        float trigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        bool fire = trigger > 0.8;

        if (fire)
        {
            if (!laser_fired)
            { 
                if (FireATorpedo())
                {
                    laser.Play();
                    laser_fired = true;
                }
            }
        }
        else
        {
            laser_fired = false;
        }
    }
}
