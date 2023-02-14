using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string id = string.Empty;
    public string Id => id;

    private IDataService DataService = new JsonDataService();

    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }

    public void SaveData(BaseStats stats)
    {
        if(!DataService.SaveData($"/Entity-{id}-Stats.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Entity-{id}-Stats!");
        }
    }

    public BaseStats LoadData()
    {
        try
        {
            return DataService.LoadData<NPCStats>($"/Entity-{id}-Stats.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogError($"Could not load Entity-{id}-Stats: {e.Message}");
            return null;
        }
    }

    public void ClearData()
    {
        string path = Application.persistentDataPath + "/example-stats.json";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
