using System;
using System.Collections.Generic;

public class ObstecleProvider : IItemProvider
{
    private List<Item> _items = new List<Item>();

    public event Action<int> OnValueChanged;

    public int Count => _items.Count;

    public void AddItem(Item item)
    {
        _items.Add(item);
        OnValueChanged?.Invoke(_items.Count);
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        OnValueChanged?.Invoke(_items.Count);
    }

}
