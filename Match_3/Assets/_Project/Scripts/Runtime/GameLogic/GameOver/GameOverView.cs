using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using TMPro;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AnimationCurve _scoreCurve;

    [SerializeField] private float _scoreAnimTime;

    private GameScore _gameScore;
    private ISceneChanger _sceneChanger;

    private float _score;

    [Inject]
    private void Construct(ISceneChanger sceneChanger, GameScore gameScore)
    {
        _gameScore = gameScore;
        _sceneChanger = sceneChanger;
    }

    public void Init()
    {
        _restartButton.onClick.AddListener(() =>
        {
            _sceneChanger.ChangeScene(Scenes.Gameplay);
        });

        _backToMenuButton.onClick.AddListener(() =>
        {
            _sceneChanger.ChangeScene(Scenes.Menu);
        });

        _scoreText.text = $"Score: {0}";

    }

    public void StartActiveAnimation()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(1, 0.75f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => StartCoroutine(StartScoreAnimation()));
    }

    private IEnumerator StartScoreAnimation()
    {
        float timer = 0;

        while (timer < _scoreAnimTime)
        {
            timer += Time.deltaTime;

            _score = Mathf.Lerp(0, _gameScore.Score, _scoreCurve.Evaluate(timer / _scoreAnimTime));
            _scoreText.text = $"Score: {(int)_score}";

            yield return null;
        }
    }
}
