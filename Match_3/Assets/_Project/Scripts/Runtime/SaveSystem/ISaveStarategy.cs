public interface ISaveStarategy
{
    public TData Load<TData>(string key);
    public void Save<TData>(TData data, string key);
}
