using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
public abstract class Item : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private float _pointsCount;

    public ItemType itemType;

    protected ItemDestroyer _itemDestroyer;

    private GameTile _curentTile;
    private IItemProvider _provider;

    public GameTile CurentTile => _curentTile;
    public float PointsCount => _pointsCount;
    public int Index => _index;

    public void Init(ItemDestroyer itemDestroyer, ItemType itemType, IItemProvider provider = null)
    {
        _itemDestroyer = itemDestroyer;
        _provider = provider;

        this.itemType = itemType;
    }
    public void SetTile(GameTile newTile) => _curentTile = newTile;

    virtual async public UniTask DestroyItemAsync()
    {
        if(CurentTile.curentItem == null) return;
        else _curentTile.curentItem = null;

        _provider?.RemoveItem(this);

        Tween tween = transform.DOScale(Vector3.zero, 0.2f).SetLink(gameObject);
        await tween.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(gameObject.GetCancellationTokenOnDestroy());

        Destroy(gameObject);
    }

    virtual public void DestroyAction(GameBoard gameBoard) { }

    protected void PlayDestroyParticle(ExplosionParlicle explosionParlicle)
    {
        explosionParlicle.transform.position = transform.position;

        explosionParlicle.Play();
    }
}

