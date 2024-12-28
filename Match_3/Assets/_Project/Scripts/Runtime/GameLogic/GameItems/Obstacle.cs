
using Cysharp.Threading.Tasks;

public class Obstacle : Item
{
    public ObjectPool<BoxExplosion> boxExplosionPool;

    public async override UniTask DestroyItemAsync()
    {
        PlayDestroyParticle();

        await base.DestroyItemAsync();
    }

    private void PlayDestroyParticle()
    {
        BoxExplosion boxExplosion= boxExplosionPool.Get();

        boxExplosion.transform.position = transform.position;

        boxExplosion.Play();
    }
}
