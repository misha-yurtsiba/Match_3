﻿using System;
using System.Collections.Generic;

[Serializable]
public class LevelData 
{
    public int Level;
    public int width;
    public int height;
    public List<ItemData> itemData = new List<ItemData>();

}