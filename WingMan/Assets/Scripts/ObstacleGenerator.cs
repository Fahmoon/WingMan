using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public static ObstacleGenerator Instance;
    private void Awake()
    {
        Instance = this;
    }
 
    public void GenerateNewLevel(ClipPoints _clipPoints)
    {
        for (int i = 0; i < 60; i++)
        {
            Instantiate(obstaclePrefabs[0], new Vector3(Random.Range(_clipPoints.upperLeft.x, _clipPoints.upperRight.x), Random.Range(0, 850), Random.Range(_clipPoints.upperLeft.z, _clipPoints.downLeft.z)), Quaternion.identity, transform);
        }

    }
}
