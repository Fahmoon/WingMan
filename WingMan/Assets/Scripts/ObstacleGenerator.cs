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
        for (int i = 0; i < (GameStatics.instance.playerHeightY-GameStatics.instance.parachuteTriggerY-GameStatics.instance.startOffset)/ (GameStatics.instance.prefabsStep); i++)
        {
           
            Instantiate(obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)], new Vector3(0, i*GameStatics.instance.prefabsStep+GameStatics.instance.parachuteTriggerY, 0), Quaternion.identity, transform);
        }

    }
}
