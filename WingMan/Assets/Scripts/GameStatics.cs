using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatics : MonoBehaviour
{
    [SerializeField] GameObject ground;
    [SerializeField] GameObject parachuteTrigger;
    [SerializeField] GameObject playerHeight;
    [HideInInspector] public float groundY;
    [HideInInspector] public float parachuteTriggerY;
    [HideInInspector] public float playerHeightY;
    public float prefabsStep;
    public float startOffset;
    public static GameStatics instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        groundY = ground.transform.position.y;
        parachuteTriggerY = parachuteTrigger.GetComponent<Collider>().bounds.center.y + parachuteTrigger.GetComponent<Collider>().bounds.extents.y;
        playerHeightY = playerHeight.transform.position.y;
    }
}
