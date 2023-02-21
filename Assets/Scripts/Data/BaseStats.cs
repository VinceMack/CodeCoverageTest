using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseStats : IStats
{
    public string guid;

    public int x;
    public int y;
    public int z;

    public bool isDownward; //Move this to its own 'StaircaseStats.cs'
}
