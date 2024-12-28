using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ItemDestroyer
{
    private List<UniTask> _tasks = new List<UniTask>();
    private readonly GameScore _gameScore;

    public ItemDestroyer(GameScore gameScore)
    {
        _gameScore = gameScore;
    }

    public void DestroyOneItem(Item item)
    {
        if (item == null) return;

        _gameScore.AddScore(item.PointsCount);

        _tasks.Add(item.DestroyItemAsync().AttachExternalCancellation(item.GetCancellationTokenOnDestroy()));
    }
    public async UniTask DestroyItemsAsync(CancellationToken cancellationToken)
    {
        await UniTask.WhenAll(_tasks);

        _tasks.Clear();
    }
}