using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Tweener;

public class ComboStar : MonoBehaviour
{
    private static ComboStar instance;
    private TimerSpan timerCombo;

    [SerializeField] private Scrollbar barCombo;

    [SerializeField] private Transform TextCombo;

    private StateCombo stateCombo;
    public static StateCombo GetStateCombo
    {
        get
        {
            return instance.stateCombo;
        }
    }
    public static float ProgressComboBar
    {
        get
        {
            return instance.barCombo.size;
        }
        set
        {
            instance.barCombo.value = 0;
            instance.barCombo.size = value;
        }
    }
    private void Awake()
    {
        if (!instance)
            instance = this;
        Tween.AddScale(instance.TextCombo, new Vector3(0.2f, 0.2f, 0.2f), 0.2F).ChangeLoop(TypeLoop.PingPong);
    }
    public static void StartCombo(float seconds)
    {
        instance.barCombo.gameObject.SetActive(true);
        TimerSpan timerCombo = instance.timerCombo;
        if (timerCombo != null)
        {
            timerCombo.Stop();
            timerCombo = null;
        }
        using (timerCombo = new(instance))
        {
            TimerSpan timerSpan = timerCombo;
            timerCombo.StartTime = seconds;
            timerCombo.OnCompleted += () =>
            {
                instance.barCombo.gameObject.SetActive(false);
                timerCombo = null;
            };
            timerCombo.OnUpdate += () =>
            {
                ProgressComboBar = timerSpan.GetProgress;
            };
        }
    }
    public enum StateCombo
    {
        Level_none,
        Level_1,
        Level_2,
        Level_3,
    }
}
