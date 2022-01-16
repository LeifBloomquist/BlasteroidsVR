using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAIFighters : MonoBehaviour
{
    // Number of Asteroid to instantiate
    public int numFighters;

    // size of the field
    public float fieldSize;

    // Reference to the Prefabs. Drag a Prefab into this field in the Inspector.
    public GameObject FighterPrefab;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        RandomPlacement();
    }

    void RandomPlacement()
    {
        GameObject newFighter = FighterPrefab;

        for (int i = 0; i < numFighters; i++)
        {
            float px = Random.Range(-fieldSize, fieldSize);
            float py = Random.Range(-fieldSize, fieldSize);
            float pz = Random.Range(-fieldSize, fieldSize);

            GameObject clone = Instantiate(newFighter, new Vector3(px, py, pz), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
}
