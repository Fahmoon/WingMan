using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CoinsCollected : MonoBehaviour
{
    List<Image> myCoinsEmptySlots = new List<Image>();
    public Sprite filledCoin;
    [SerializeField] GameObject coinsEmptyPrefab;
    private void Start()
    {

        for (int i = 0; i < GameManager.Instance.coinCount; i++)
        {
            myCoinsEmptySlots.Add(Instantiate(coinsEmptyPrefab, transform).GetComponent<Image>());
        }
        PlayerStats.CheckMyCount += FillCoinsSlot;
    }
    void FillCoinsSlot(int count)
    {
        myCoinsEmptySlots[count - 1].sprite = filledCoin;

    }
    private void OnDestroy()
    {
        PlayerStats.CheckMyCount -= FillCoinsSlot;

    }
}
