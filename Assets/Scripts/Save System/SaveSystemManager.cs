using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSystemManager : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();

    public virtual void SaveData<T>(T stats, int saveNumber)
    {
        if(!DataService.SaveData($"/{saveNumber}/Save-{saveNumber}.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Save-{saveNumber}!");
        }
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
            Debug.LogError($"Could not load Save-{saveNumber}: {e.Message}");
            return default;
        }
    }

    public void ClearData(int saveNumber)
    {
        string path = Application.persistentDataPath + $"/{saveNumber}/Save-{saveNumber}.json";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
    
    public void SaveInfo(int saveNumber)
    {
        SaveStats saveStats = new SaveStats(DateTime.Now);
        if(!DataService.SaveData($"/{saveNumber}/Save-Stats.json", saveStats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Save-Stats!");
        }
    }

    public T LoadInfo<T>(int saveNumber)
    {
        try
        {
            return DataService.LoadData<T>($"/{saveNumber}/Save-Stats.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogError($"Could not load Save-Stats: {e.Message}");
            return default;
        }
    }
}
