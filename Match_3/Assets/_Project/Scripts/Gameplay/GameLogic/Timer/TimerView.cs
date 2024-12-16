using UnityEngine;
using TMPro;
using Zenject;

public class TimerView : MonoBehaviour
{
    public const string TIME_FORMAT = "{0}:{1:00}";

    [SerializeField] private TextMeshProUGUI _timerText;

    private TimerController _timerController;

    [Inject]
    private void Construct(TimerController timerController)
    {
        _timerController = timerController;
    }
    private void OnEnable() => _timerController.OnValueChanget += UpdateTimerText;
    private void OnDisable() => _timerController.OnValueChanget -= UpdateTimerText;

    private void UpdateTimerText(Timer timer)
    {
        _timerText.text = string.Format(TIME_FORMAT, timer.Minutes, timer.Seconds);
    }
}
