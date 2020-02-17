using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Collider myCollider;
    Vector3 myCenter;
    float radius;
    [SerializeField] int scoreDistanceFactor = 10;
    private void Start()
    {
        myCollider = GetComponent<Collider>();
        myCenter = myCollider.bounds.center;
        radius = Vector3.Distance(Vector3.right * myCollider.bounds.extents.x + myCenter, myCenter);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WinRoutine(other));
    }
    IEnumerator WinRoutine(Collider other)
    {
        GameManager.Instance.CurrentGameState = GameStates.Waiting;
        int scoreToAdd = CalculateScore(other);
        yield return new WaitForSeconds(2);
        GameManager.Instance.CurrentGameState = GameStates.Win;
        AddToPlayerScore(scoreToAdd);
    }
    private int CalculateScore(Collider other)
    {
        float distance = Vector3.Distance(myCenter, other.bounds.center);
        int percent = Mathf.RoundToInt(distance * 100 / radius);
        if (percent < 20)
        {
            return FactorizeScore(10);
        }
        if (percent < 40)
        {
            return FactorizeScore(8);
        }
        if (percent < 60)
        {
            return FactorizeScore(6);
        }
        if (percent < 80)
        {
            return FactorizeScore(4);
        }
        if (percent < 100)
        {
            return FactorizeScore(2);
        }

        return FactorizeScore(0);


    }
    int FactorizeScore(int factor)
    {
        return scoreDistanceFactor * factor;
    }
    void AddToPlayerScore(int scoreToAddToPlayer)
    {
        GameManager.Instance.playerStats.currentScore += scoreToAddToPlayer;
    }
}
