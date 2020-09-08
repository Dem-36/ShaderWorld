using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float currentTime { get; private set; } = 0.0f;
    public float limitTime { get; private set; } = 0.0f;

    public Timer()
    {
        limitTime = 1.0f;
        Initialize();
    }

    public Timer(float limitTime)
    {
        this.limitTime = limitTime;
        Initialize();
    }

    public void Initialize()
    {
        currentTime = 0.0f;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
    }

    public bool IsTime()
    {
        return currentTime >= limitTime;
    }
}
