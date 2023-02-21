using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[RequireComponent(typeof(BaseStats))]
public class SaveableEntity : MonoBehaviour
{
    protected IStats myStats;
    private string prefabName;
    [SerializeField] private string id = string.Empty;
    public string Id => id;

    private IDataService DataService = new JsonDataService();

    [SerializeField] private Tile currentLocation;

    public void SetPrefabName(string prefabName)
    {
        this.prefabName = prefabName;
    }

    public string GetPrefabName()
    {
        return prefabName;
    }

    public void GenerateId(string premadeGUID = "")
    {
        if(id == string.Empty)
        {
            id = Guid.NewGuid().ToString();
        }
        if(premadeGUID != string.Empty)
        {
            id = premadeGUID;
        }
    }

    public Tile GetCurrentLocation()
    {
        return currentLocation;
    }

    public void SetCurrentLocation(Tile newLocation)
    {
        currentLocation = newLocation;
    }

    public virtual void SaveData<T>(T stats)
    {
        if(!DataService.SaveData($"/Entity-{id}-Stats.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Entity-{id}-Stats!");
        }
    }

    public virtual T LoadData<T>()
    {
        try
        {
            return DataService.LoadData<T>($"/Entity-{id}-Stats.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogError($"Could not load Entity-{id}-Stats: {e.Message}");
            return default;
        }
    }

    public void ClearData()
    {
        string path = Application.persistentDataPath + $"/Entity-{id}-Stats.json";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public virtual void SaveMyData()
    {
        SaveData<IStats>(myStats);
    }

    public virtual void LoadMyData()
    {
        myStats = LoadData<IStats>();
    }
}
