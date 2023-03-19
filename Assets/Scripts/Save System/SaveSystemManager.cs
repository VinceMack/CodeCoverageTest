using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSystemManager : MonoBehaviour, ISaveSystemManager
{
    private IDataService DataService;

    public SaveSystemManager()
    {
        DataService = new JsonDataService();
    }

    public SaveSystemManager(IDataService service)
    {
        DataService = service;
    }

    public virtual bool SaveData<T>(T stats, int saveNumber)
    {
        if(!DataService.SaveData($"/{saveNumber}/Save-{saveNumber}.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogWarning($"Could not save Save-{saveNumber}!");
            return false;
        }
        return true;
    }

    public virtual T LoadData<T>(int saveNumber)
    {
        if(!Directory.Exists(Application.persistentDataPath + $"/{saveNumber}"))
        {
            Debug.LogWarning($"Save-{saveNumber} not detected.");
            return default;
        }
        try
        {
            return DataService.LoadData<T>($"/{saveNumber}/Save-{saveNumber}.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogWarning($"Could not load Save-{saveNumber}: {e.Message}");
            return default;
        }
    }
    
    public bool SaveInfo(int saveNumber)
    {
        SaveStats saveStats = new SaveStats(DateTime.Now);
        if(!DataService.SaveData($"/{saveNumber}/Save-Stats.json", saveStats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogWarning($"Could not save Save-Stats!");
            return false;
        }
        return true;
    }

    public T LoadInfo<T>(int saveNumber)
    {
        try
        {
            return DataService.LoadData<T>($"/{saveNumber}/Save-Stats.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogWarning($"Could not load Save-Stats: {e.Message}");
            return default;
        }
    }
}
