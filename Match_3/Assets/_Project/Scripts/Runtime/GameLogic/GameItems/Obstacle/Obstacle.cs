using Cysharp.Threading.Tasks;

public class Obstacle : Item
{
    public ObjectPool<BoxExplosion> boxExplosionPool;

    public override UniTask DestroyItemAsync()
    {
        PlayDestroyParticle(boxExplosionPool.Get());

        return base.DestroyItemAsync();
    }
}
