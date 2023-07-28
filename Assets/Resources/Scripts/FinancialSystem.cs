using System;
using TMPro;
using UnityEngine;
using _int = ProtectInt;

public class FinancialSystem : MonoBehaviour
{
    private _int starCount;
    private _int coinCount;

    private static FinancialSystem instance;

    public event Action<string> UpdateCountCoin;

    public event Action<string> UpdateCountStar;

    public static FinancialSystem Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject("Finance", typeof(FinancialSystem)).GetComponent<FinancialSystem>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    public int CountCoin
    {
        get 
        {
            return coinCount;
        }
        private set
        {
            UpdateCountCoin?.Invoke(value.ToString());
            coinCount = value;
        }
    }
    public int CountStar
    {
        get
        {
            return starCount;
        }
        private set
        {
            UpdateCountStar?.Invoke(value.ToString());
            starCount = value;
        }
    }
    internal void SetTrophy(ITrophy trophy)
    {
        CountCoin += trophy.GetCoins;
        CountStar += trophy.GetStars;
    }
    public void ResetSlider()
    {

    }
}
