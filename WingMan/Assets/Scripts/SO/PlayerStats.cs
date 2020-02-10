using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableCount", menuName = "SO/CollectableCount")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] int currentCoinsCount;
    public int currentScore;
    public delegate void CoinsTaken(int count);
    public static event CoinsTaken CheckMyCount;
    public int CurrentCoinsCount
    {
        get => currentCoinsCount; set
        {
            currentCoinsCount = value;
            CheckMyCount.Invoke(currentCoinsCount);
        }
    }
    
  
}
