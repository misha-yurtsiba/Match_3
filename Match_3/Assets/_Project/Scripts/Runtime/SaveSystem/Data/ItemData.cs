using System;

[Serializable]
public class ItemData
{
    public ItemType itemType;
    public int index;

    public ItemData(ItemType itemType, int index)
    {
        this.itemType = itemType;
        this.index = index;
    }
}
