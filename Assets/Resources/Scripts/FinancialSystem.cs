using System;
using TMPro;
using UnityEngine;

public class FinancialSystem : MonoBehaviour
{
    private int starCount = 0;
    private ProtectInt coinCount = new(0);

    [SerializeField] private TMP_Text starCountText;
    [SerializeField] private TMP_Text coinCountText;

    [SerializeField] private GameObject sliderCombo;
    private bool isSliderFull = false;

    public int CountCoin
    {
        get 
        {
            return starCount;
        }
        private set
        {
            coinCountText.text = value.ToString();
            starCount = value;
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            CountCoin++;
        }
    }
        public void FillSlider()
    {
        if (IsSliderFull())
        {

        }
    }


    internal void SetTrophy(ITrophy trophy)
    {
        CountCoin = trophy.GetCoins;
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
