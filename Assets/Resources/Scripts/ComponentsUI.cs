using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Tweener;
using TMPro;

public class ComponentsUI : MonoBehaviour
{
    [SerializeField] private Scrollbar barCombo;

    [SerializeField] private Image barTime;

    [SerializeField] private UnityEvent timeComplite;

    public event UnityAction TimeComplite
    {
        add
        {
            timeComplite.AddListener(value);
        }
        remove
        {
            timeComplite.RemoveListener(value);
        }
    }

    [SerializeField] private TMP_Text TimeText;

    [SerializeField] private float TimeRoundSeconds;

    [SerializeField] private TMP_Text coinInfo;

    TimerSpan timerCombo;
    public int SetCoinInfo
    {
        set
        {
            coinInfo.text = value.ToString();
        }
    }
    [SerializeField] private TMP_Text scoreInfo;

    [SerializeField] private Transform TextCombo;

    public int SetScoreInfo
    {
        set
        {
            scoreInfo.text = value.ToString();
        }
    }

    private float ProgressTimeBar
    {
        set
        {
            barTime.fillAmount = value;
        }
    }
    
    private float ProgressComboBar
    {
        set
        {
            barCombo.size = value;
        }
    }
    public bool IsCombo
    {
        get
        {
            return timerCombo != null;
        }
    }
    public void StartCombo(float seconds)
    {
        barCombo.gameObject.SetActive(true);
        if(timerCombo != null)
        {
            timerCombo.Stop();
            timerCombo = null;
        }
        using (timerCombo = new(this))
        {
            TimerSpan timerSpan = timerCombo;
            timerCombo.StartTime = seconds;
            timerCombo.OnCompleted += () =>
            {
                timeComplite.Invoke();
                barCombo.gameObject.SetActive(false);
                timerCombo = null;
            };
            timerCombo.OnUpdate += () =>
            {
                ProgressComboBar = timerSpan.GetProgress;
            };
        }
    }
    private void Start()
    {
        Tween.AddScale(TextCombo, new Vector3(0.2f, 0.2f, 0.2f), 0.2F).ChangeLoop(TypeLoop.PingPong);
        using (TimerSpan timer = new(this))
        {
            timer.StartTime = 15;
            timer.OnCompleted += timeComplite.Invoke;
            timer.OnUpdate += () =>
            {
                TimeText.text = timer.ToString();
                ProgressTimeBar = timer.GetProgress;
            };
            timer.IsViewMillisecond = false;
        }
        
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            StartCombo(1.40F);
        }
    }
}
