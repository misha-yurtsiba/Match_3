using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Item
{
    [SerializeField] private int _index;
    public int Index => _index;
}
