using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShipController : ShipCommon
{
    private GameObject player;

    private TextMesh debugText;

    // Some parameters to give this enemy a bit of variation
    private float my_acceleration = 1f;
    private float my_angular_acceleration = 1f;
    private float my_attack_range = 200f;

    private Vector3 last_player_direction;

    private enum State
    {
        Stopping,
        Chasing,
        Attacking,
        Fleeing
    }

    private State my_state = State.Chasing;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        // Add some variation
      //  my_acceleration = UnityEngine.Random.Range(0.5f, 2.0f);
        my_angular_acceleration = UnityEngine.Random.Range(0.5f, 2.0f);
        my_attack_range = UnityEngine.Random.Range(50f, 300f);

        player = GameObject.Find("PlayerFighter");
        debugText = this.transform.Find("DebugText").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        // Determine which direction to rotate towards
        Vector3 player_direction = player.transform.position - this.transform.position;
        float player_distance = player_direction.magnitude;
        float player_angle = Vector3.Angle(player_direction, transform.forward);

        string action = "None";

        switch (my_state)
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

            case State.Chasing:

                // 1. Pointing
                RotateTowardsTarget(player, my_angular_acceleration * 2f);

                // 2. Acceleration

                Vector3 direction_delta = player_direction - last_player_direction;

                float desired_velocity = 100; // (player_distance / 100);
                float current_velocity = rb.velocity.magnitude;
                float delta_velocity = current_velocity - desired_velocity;

                if (delta_velocity > 0) // Too fast
                {
                    ApplyBrakes(delta_velocity / 10);
                    action = "Braking " + (delta_velocity / 10).ToString("F1");
                }
                else // Too slow
                {
                    Accelerate(-delta_velocity);
                    action = "Accelerating " + (delta_velocity / 10).ToString("F1");
                }               

                // 3. State Changes
                if (player_distance <= my_attack_range)
                {
                     my_state = State.Attacking;
                }

                if (player_distance >= 1000)
                {
                    // myState = State.Stopping;
                }

                break;

                
            case State.Attacking:

                action = "Attacking ";

                // 1. Pointing
                RotateTowardsTarget(player, my_angular_acceleration * 0.4f);

                // 2. Acceleration (Stop)
                ApplyBrakes(0.5f);

                // 3. State Changes
                if (player_distance > my_attack_range)
                {
                    my_state = State.Chasing; 
                }

                // TODO, fleeing

                break;
        }

        // Take a shot in any state

        if ((player_angle < 0.4f) && (player_distance < my_attack_range))
        {
            FireATorpedo();
        }

        // Remember last direction
        last_player_direction = last_player_direction;

        ShowDebug("State = " + my_state.ToString() + "\nDistance = " + player_distance.ToString("F1") + "\n" + action + "\nAngle = " + player_angle.ToString("F1"), 0.07f); // player_distance/400f);
    }

    void RotateTowardsTarget(GameObject target, float rotation_rate)
    {
        // Calculate a rotation a step closer to the target and applies rotation to this object
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float singleStep = rotation_rate * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ShowDebug(string text, float size)
    {
        if (debugText == null) return;

        debugText.text = text;
        debugText.characterSize = size;
    }
}
