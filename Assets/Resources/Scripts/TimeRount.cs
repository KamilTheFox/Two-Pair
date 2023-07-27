using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Tweener;

public class TimeRount : MonoBehaviour
{
    private static TimeRount instance;

    [SerializeField] private TMP_Text TimeText;

    [SerializeField] private float TimeRoundSeconds;

    [SerializeField] private Image barTime;

    [SerializeField] private UnityEvent timeComplite;

    IExpansionColor redTime;

    IExpansionTween changeScale;

    private TimerSpan timer;

    public static event UnityAction TimeComplite
    {
        add
        {
            instance.timeComplite.AddListener(value);
        }
        remove
        {
            instance.timeComplite.RemoveListener(value);
        }
    }
    private float ProgressTimeBar
    {
        set
        {
            barTime.fillAmount = value;
        }
    }
    public static TimeRount Instance => instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void AddSecond(float second)
    {
        OnCompliteAnimate(false);
        timer.AddSecond(second);
    }
    private void OnCompliteAnimate(bool isActive)
    {
        if(isActive)
        {
            redTime = Tween.SetColor(barTime.transform, Color.red, 1F).
                    ChangeLoop(TypeLoop.PingPong).
                    TypeComponentChange(TypeComponentChangeColor.Image);
            changeScale = Tween.AddScale(barTime.transform.parent, Vector3.one * 0.2F, 0.5F).
            ChangeLoop(TypeLoop.PingPong);
            return;
        }
        Tween.Stop((IExpansionTween)redTime);
        Tween.Stop(changeScale);
        barTime.transform.parent.localScale = 1F * Vector3.one;
        barTime.transform.GetComponent<Image>().color = Color.white;
        redTime = null;
        changeScale = null;
    }
    private void Start()
    {
        using (timer = new(this))
        {
            timer.StartTime = 15;
            timer.OnCompleted += () =>
            {
                timeComplite.Invoke();
                OnCompliteAnimate(false);
            };
            timer.OnUpdate += () =>
            {
                TimeText.text = timer.ToString();
                ProgressTimeBar = timer.GetProgress;
                if (timer.GetSpan.TotalSeconds <= 11 && redTime == null && changeScale == null)
                {
                    OnCompliteAnimate(true);
                }
            };
            timer.IsViewMillisecond = false;
        }
        
    }
}
