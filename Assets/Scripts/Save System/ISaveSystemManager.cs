using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSystemManager
{
    public T LoadData<T>(int saveNumber);

    public bool SaveData<T>(T stats, int saveNumber);

    public T LoadInfo<T>(int saveNumber);

    public bool SaveInfo(int saveNumber);
}
