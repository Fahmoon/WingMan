using System;
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


    public void GenerateNewLevel()
    {


    }
}
