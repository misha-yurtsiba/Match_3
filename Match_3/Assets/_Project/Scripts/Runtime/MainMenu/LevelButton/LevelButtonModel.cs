public class LevelButtonModel
{
    private readonly int _level;
    private readonly bool _isComplited;

    public LevelButtonModel(int level, bool isComplited)
    {
        _level = level;
        _isComplited = isComplited;
    }
    public int Level => _level;
    public bool IsComplited => _isComplited;
}
