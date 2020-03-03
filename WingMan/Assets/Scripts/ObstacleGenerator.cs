using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public static ObstacleGenerator Instance;
    List<GameObject> generatedObjects = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    [ContextMenu("generate")]
    public void GenerateNewLevel()
    {
        for (int i = 0; i < (GameStatics.instance.playerHeightY - GameStatics.instance.parachuteTriggerY - GameStatics.instance.startOffset) / (GameStatics.instance.prefabsStep); i++)
        {

            generatedObjects.Add(Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], new Vector3(0, i * GameStatics.instance.prefabsStep + GameStatics.instance.parachuteTriggerY, 0), Quaternion.identity, transform));
        }

    }
    [ContextMenu("delete")]

    public void DestroyCurrentObstacles()
    {
        generatedObjects.ForEach(obj => Destroy(obj));
        generatedObjects.RemoveRange(0,generatedObjects.Count);
    }
}
