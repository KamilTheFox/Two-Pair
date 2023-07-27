using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AwardsStack : MonoBehaviour, ITrophy
{
    public int GetCoins => throw new System.NotImplementedException();

    public int GetStars => throw new System.NotImplementedException();

    private Stack<Card> CollectedCard = new();

    [SerializeField] private TMP_Text coinInfo;

    [SerializeField] private TMP_Text startCount;
    private int SetCoinInfo
    {
        set
        {
            coinInfo.text = value.ToString();
        }
    }
    private int SetStarCount
    {
        set
        {
            startCount.text = value.ToString();
        }
    }

}