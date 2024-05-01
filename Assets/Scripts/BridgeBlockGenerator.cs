using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBlockGenerator : MonoBehaviour
{
    public GameObject stairPrefab; // Assign your stair prefab in the Inspector
    public float stairWidth = 1.0f; // Width of each stair
    public float stairHeight = 0.2f; // Height of each stair

    void Start()
    {
        GenerateStairs();
    }

    void GenerateStairs()
    {
        float bridgeLength = transform.localScale.z;
        int numberOfStairs = Mathf.CeilToInt(bridgeLength / stairWidth); // Calculate the number of stairs needed

        for (int i = 0; i < numberOfStairs; i++)
        {
            // Instantiate stair at the correct position
            Vector3 stairPosition = new Vector3(transform.position.x, transform.position.y + (i * stairHeight), transform.position.z + (i * stairWidth));
            Instantiate(stairPrefab, stairPosition, Quaternion.identity, transform);
        }
    }
}
