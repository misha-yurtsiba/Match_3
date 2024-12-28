using System;
using System.Collections.Generic;
using UnityEngine;

public class GameScore
{
    private float _score;

    public float Score => _score;

    public void AddScore(float newScore)
    {
        _score += newScore;
    }
}
