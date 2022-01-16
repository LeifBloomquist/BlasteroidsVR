using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShipController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private DateTime lastshot_fired;

    private TextMesh debugText;

    public float Speed = 1;
    public GameObject TorpedoPrefab;

    private enum State
    {
     //   Searching,
        Approaching,
        Attacking,
  //      Fleeing
    }

    private State myState = State.Approaching;

    // Start is called before the first frame update
    void Start()
    {
        Speed = UnityEngine.Random.Range(0.1f, 1.0f);

        rb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerFighter");
        debugText = this.transform.Find("DebugText").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.transform.position - this.transform.position;
        float distance = targetDirection.magnitude;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        float rotation_speed = 0f;

        switch (myState)
        {
           /* case State.Searching:

                // 1. Pointing
                rotation_speed = 1f;

                // 2. Acceleration
                rb.AddForce(-rb.velocity * 10);  // "Brakes"
                
                // 3. State Changes
                if (angle < 2)
                {
                    myState = State.Approaching;
                }    

                break;
           */

            case State.Approaching:

                // 1. Pointing
                rotation_speed = 1.5f;

                // 2. Acceleration
                rb.AddForce(this.transform.forward * Speed * (distance - 100) / 1000f);

                // 3. State Changes
                if (distance < 100)
                {
             //        myState = State.Attacking;
                }

                if (distance > 1000)
                {
            //        myState = State.Searching;
                }

                break;

                /*
            case State.Attacking:

                // 1. Pointing
                rotation_speed = 0.8f;

                // 2. Acceleration
                rb.AddForce(-this.transform.forward * 1.5f);  // "Brakes" - Note -ve

                // 3. State Changes

                if (distance > 110)
                {
                    myState = State.Approaching; 
                }

                if (angle > 10)
                {
                   // myState = State.Searching;
                }

                // TODO, fleeing

                break;
                */

        }

        // Take a shot in any state

        if ((angle < 0.4f) && (distance < 200f))
        {
            TimeSpan interval = DateTime.Now - lastshot_fired;

            if (interval.TotalMilliseconds > 500)
            {
                FireATorpedo();
            }
        }


        // Calculate a rotation a step closer to the target and applies rotation to this object
        float singleStep = rotation_speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        ShowDebug("State = " + myState.ToString() + "\nDistance = " + distance.ToString("F1") + "\nAngle = " + angle.ToString("F3"), distance/400f);
    }

    void FireATorpedo()
    {
        GameObject torpedo = Instantiate(TorpedoPrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
        torpedo.transform.SetParent(null);
        Rigidbody rbt = torpedo.GetComponent<Rigidbody>();
        rbt.AddForce(this.transform.forward * 100);

        lastshot_fired = DateTime.Now;
    }


    void ShowDebug(string text, float size)
    {
        if (debugText == null) return;

        debugText.text = text;
        debugText.characterSize = size;
    }
}
