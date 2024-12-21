using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ItemDestroyer
{
    private List<UniTask> _tasks = new List<UniTask>();
    public void DestroyOneItem(Item item)
    {
        if (item == null) return;
         
        _tasks.Add(item.DestroyItemAsync().AttachExternalCancellation(item.GetCancellationTokenOnDestroy()));
    }
    public async UniTask DestroyItemsAsync(CancellationToken cancellationToken)
    {
        await UniTask.WhenAll(_tasks);

        _tasks.Clear();
    }
}