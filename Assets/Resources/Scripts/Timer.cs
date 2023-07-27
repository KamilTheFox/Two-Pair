using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

public class TimerSpan : IDisposable
{
    MonoBehaviour behaviour = null;
    IEnumerator localCorrutine;
    public TimerSpan(MonoBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }
    public void Start()
    {
        lockChange = true;
        var values = GetIntFromFloat(startTime);
        FixedStartTime = DateTime.Now.AddSeconds(values.Item1).AddMilliseconds(values.Item2 * 10);
        localCorrutine = UpdateTick();
        behaviour.StartCoroutine(localCorrutine);
    }

    private bool lockChange;

    private float startTime;
    public float StartTime
    {
        private get
        {
            if (startTime > MaxTime)
            {
                Debug.LogWarning("Max secounds " + MaxTime);
                startTime = MaxTime;
            }
            return startTime;
        }
        set
        {
            if (lockChange)
                return;
            startTime = value;
        }
    }
    private bool viewMillisecond = true;
    public bool IsViewMillisecond
    {
        set
        {
            viewMillisecond = value;
        }
    }
    private bool viewSecond = true;
    public bool IsViewSecond
    {
        set
        {
            viewSecond = value;
        }
    }
    private bool viewMinute = true;
    public bool IsViewMinute
    {
        set
        {
            viewMinute = value;
        }
    }
    private UnityEvent onCompleted = new();

    public event UnityAction OnCompleted
    {
        add
        {
            onCompleted.AddListener(value);
        }
        remove
        {
            onCompleted.RemoveListener(value);
        }
    }
    private UnityEvent onUpdate = new();

    public event UnityAction OnUpdate
    {
        add
        {
            onUpdate.AddListener(value);
        }
        remove
        {
            onUpdate.RemoveListener(value);
        }
    }
    public float GetProgress 
    {   
        get
        {
            TimeSpan spanCurrent = (FixedStartTime - DateTime.Now);
            TimeSpan spanStarted;
            if (startTime == 0)
            {
                spanStarted = new TimeSpan(0, 1, 0);
                return (float)(Math.Abs(spanCurrent.Seconds) / spanStarted.TotalSeconds);
            }
            var values = GetIntFromFloat(startTime);
            spanStarted = new TimeSpan(0, 0, 0, values.Item1, values.Item2 * 10);
            return (float)(spanCurrent / spanStarted);
        }
    }
    private bool isPause;

    private DateTime FixedStartTime;

    private TimeSpan span;

    public TimeSpan GetSpan => span;
    
    public void AddSecond(float second = 1)
    {
        startTime += second;
        FixedStartTime = FixedStartTime.AddSeconds(second);
    }

    public bool IsPause
    {
        get
        {
            if (Time.timeScale == 0)
                return true;
            return isPause;
        }
        set
        {
            isPause = value;
        }
    }
    public int MaxTime { private get; set; } = 600;
    private IEnumerator UpdateTick()
    {
        TimeSpan previous = FixedStartTime - DateTime.Now;
        while (true)
        {
            yield return null;
            TimeSpan span = FixedStartTime - DateTime.Now;
            if (IsPause)
                yield return new WaitUntil(() =>
                {
                    if (!IsPause)
                    {
                        FixedStartTime = DateTime.Now.AddTicks(span.Ticks);
                    }
                    return !IsPause;
                });
            this.span = span;
            onUpdate.Invoke();
            if(startTime > 0F)
            if (span.Minutes <= 0 && span.Seconds <= 0 && span.Milliseconds <= 0)
            {
                onCompleted.Invoke();
                break;
            }
            previous = span;
        }
    }
    private Tuple<int, int> GetIntFromFloat(float value)
    {
        string[] values = startTime.ToString("0.00").Split(',');
        return new(int.Parse(values[0]), int.Parse(values[1]));
    }
    public override string ToString()
    {
        string format = "";
        if(viewMinute)
        {
            format += "mm";
        }
        if(viewSecond)
        {
            if (viewMinute)
                format += "\\:";
            format += "ss";
        }
        if(viewMillisecond)
        {
            if (viewSecond || viewMinute)
                format += "\\:";
            format += "ff";
        }
        if (format == string.Empty)
            return span.ToString();
        return span.ToString(format);
    }
    public void Stop()
    {
        behaviour.StopCoroutine(localCorrutine);
    }
    public void Dispose()
    {
        Start();
    }

}
