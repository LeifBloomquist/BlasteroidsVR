using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.gameObject.name + " hit " + collision.gameObject.name);
        Destroy(this.gameObject);
       // Destroy(collision.gameObject);
    }
}
