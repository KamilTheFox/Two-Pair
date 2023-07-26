using System;
using TMPro;
using UnityEngine;

public class FinancialSystem : MonoBehaviour, IFinancial, IReward
{
    private int starCount = 0;
    private int coinCount = 0;

    [SerializeField] private TMP_Text starCountText;
    [SerializeField] private TMP_Text coinCountText;

    [SerializeField] private GameObject sliderCombo;
    private bool isSliderFull = false;

    public int Count
    {
        get 
        {
            return starCount;
        }
        private set
        {
            starCount = value;
            coinCount = value;
        }
    }

    public void FillSlider()
    {
        if (IsSliderFull())
        {

        }
    }


    public void GetCoin()
    {
        throw new NotImplementedException();
    }

    public void GetStar()
    {
        throw new NotImplementedException();
    }

    public bool IsSliderFull()
    {
        isSliderFull = true;
        return isSliderFull;
    }

    public void ResetSlider()
    {

    }
}
