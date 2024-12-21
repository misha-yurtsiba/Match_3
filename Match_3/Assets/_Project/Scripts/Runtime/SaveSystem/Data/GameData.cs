using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{ 

    public bool isFirstRun = true;
    public Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();
}