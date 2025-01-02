using System;

public interface IItemProvider
{
    public int Count { get; }
    public void AddItem(Item item);
    public void RemoveItem(Item item);

    public event Action<int> OnValueChanged;
} 
