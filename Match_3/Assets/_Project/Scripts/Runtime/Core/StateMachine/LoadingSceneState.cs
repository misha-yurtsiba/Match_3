using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class LoadingSceneState : IState
{
    private readonly LoadingCurtain _loadindCurtain;
    private readonly StateMachine _stateMachine;

    private AsyncOperation _loadingAsyncOperation;

    public LoadingSceneState(LoadingCurtain loadindCurtain, StateMachine stateMachine)
    {
        _loadindCurtain = loadindCurtain;
        _stateMachine = stateMachine;
    }
    public async void Enter()
    {
        await _loadindCurtain.ActiveLoadingCurtain();
        await ChangeScene();
    }

    public void Exit()
    {

    }

    private async UniTask ChangeScene()
    {
        _loadingAsyncOperation = SceneManager.LoadSceneAsync(SceneChanger.sceneToChange.ToString());

        await _loadingAsyncOperation;

        _loadindCurtain.DisactiveLoadingCurtain(() =>
        {
            switch (SceneChanger.sceneToChange)
            {
                case Scenes.Menu :
                    _stateMachine.EnterState<MenuState>();
                    break;
                case Scenes.Gameplay :
                    _stateMachine.EnterState<GameplayState>();
                    break;
            }
        });
    }
}
