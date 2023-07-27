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
    private void Start()
    {
        using (TimerSpan timer = new(this))
        {
            timer.StartTime = 15;
            timer.OnCompleted += () =>
            {
                timeComplite.Invoke();
                Tween.Stop((IExpansionTween)redTime);
                Tween.Stop(changeScale);
                redTime = null;
                changeScale = null;
            };
            timer.OnUpdate += () =>
            {
                TimeText.text = timer.ToString();
                ProgressTimeBar = timer.GetProgress;
                if(timer.GetSpan.TotalSeconds <= 11 && redTime == null && changeScale == null)
                {
                    redTime = Tween.SetColor(barTime.transform, Color.red, 1F).
                    ChangeLoop(TypeLoop.PingPong).
                    TypeComponentChange(TypeComponentChangeColor.Image);
                    changeScale = Tween.AddScale(barTime.transform.parent, Vector3.one * 0.2F, 0.5F).
                    ChangeLoop(TypeLoop.PingPong);
                }
            };
            timer.IsViewMillisecond = false;
        }
        
    }
}
