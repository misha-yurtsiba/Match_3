using System.Collections.Generic;
using UnityEngine;
public class Timer
{
    public float curentTime;
    public float Seconds => Mathf.FloorToInt(curentTime % 60);
    public float Minutes => Mathf.FloorToInt(curentTime / 60);
    public Timer(float startTime)
    {
        curentTime = startTime;
    }
}
