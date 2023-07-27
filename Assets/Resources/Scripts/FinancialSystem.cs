using System;
using TMPro;
using UnityEngine;

public class FinancialSystem : MonoBehaviour
{
    private ProtectInt starCount = new(0);
    private ProtectInt coinCount = new(0);

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
            return coinCount.GetValue;
        }
        private set
        {
            UpdateCountCoin?.Invoke(value.ToString());
            coinCount = new(value);
        }
    }
    public int CountStar
    {
        get
        {
            return starCount.GetValue;
        }
        private set
        {
            UpdateCountStar?.Invoke(value.ToString());
            starCount = new(value);
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
