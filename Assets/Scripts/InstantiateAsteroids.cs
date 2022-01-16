using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAsteroids : MonoBehaviour
{
    // Number of Asteroid to instantiate
    public int numAsteroids;

    // size of the field
    public float fieldSize;

    // Reference to the Prefabs. Drag a Prefab into this field in the Inspector.
    public GameObject[] AsteroidPreFabs;

    public float RingRadius;

    private System.Random rnd = new System.Random();

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        Debug.Log("Found " + AsteroidPreFabs.Length + " Asteroid prefabs");
        //RingPlacement();
        RandomPlacement();
    }

    void RingPlacement()
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            int a = rnd.Next(AsteroidPreFabs.Length);

            float theta = Random.Range(0f, 6.28318530718f);

            float py = Random.Range(-50f, 50f);
            float px = (float)(RingRadius * System.Math.Sin(theta)) + Random.Range(-50f, 50f);
            float pz = (float)(RingRadius * System.Math.Cos(theta)) + Random.Range(-50f, 50f);

            float s = Random.Range(0.1f, 10f);
            float rx = Random.Range(-10f, 10f);
            float ry = Random.Range(-10f, 10f);
            float rz = Random.Range(-10f, 10f);
            float fx = Random.Range(-1f, 1f);
            float fy = Random.Range(-1f, 1f);
            float fz = Random.Range(-1f, 1f);

            GameObject newAsteroid = AsteroidPreFabs[a];

            GameObject clone = Instantiate(newAsteroid, new Vector3(px, py, pz), Quaternion.identity);
            clone.transform.localScale *= s;

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddTorque(new Vector3(rx, ry, rz));
            rb.AddForce(new Vector3(fx, fy, fz));
        }
    }

    void RandomPlacement()
    {
        for (int i = 0; i < numAsteroids; i++ )
        {
            int a = rnd.Next(AsteroidPreFabs.Length);
            float px = Random.Range(-fieldSize, fieldSize);
            float py = Random.Range(-fieldSize, fieldSize);
            float pz = Random.Range(-fieldSize, fieldSize);
            float s  = Random.Range(10f, 600f);
            float rx = Random.Range(-10f, 10f);
            float ry = Random.Range(-10f, 10f);
            float rz = Random.Range(-10f, 10f);
            float fx = Random.Range(-1f, 1f);
            float fy = Random.Range(-1f, 1f);
            float fz = Random.Range(-1f, 1f);

            GameObject newAsteroid = AsteroidPreFabs[a];

            GameObject clone = Instantiate(newAsteroid, new Vector3(px, py, pz), Quaternion.identity);
            clone.transform.localScale *= s;

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddTorque(3 * new Vector3(rx, ry, rz));
            rb.AddForce(5 * new Vector3(fx, fy, fz));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
}
