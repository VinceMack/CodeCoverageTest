using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveStats : IStats
{
    public SaveStats(DateTime time)
    {
        lastPlayed = time;
    }

    DateTime lastPlayed;
}
