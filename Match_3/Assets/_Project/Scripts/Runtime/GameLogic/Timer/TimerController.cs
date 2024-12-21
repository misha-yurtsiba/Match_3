using System;
using UnityEngine;
using Zenject;
public class TimerController : MonoBehaviour
{
    public event Action<Timer> OnValueChanget;
    private Timer _timer;
    private GameOver _gameOver;

    public bool canUpdateTimer;

    [Inject]
    private void Construct(GameOver gameOver)
    {
        _gameOver = gameOver;

    }
    public void Init(int startTime)
    {
        _timer = new Timer(startTime);
        canUpdateTimer = true;
    }
    private void Update()
    {
        if (!canUpdateTimer) return;

        if(_timer.curentTime > 0)
        {
            OnValueChanget?.Invoke(_timer);
            _timer.curentTime -= Time.deltaTime;
        }
        else
        {
            canUpdateTimer = false;
            _gameOver.LoseGame();
        }
    }
}