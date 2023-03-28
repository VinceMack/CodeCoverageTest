using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//[RequireComponent(typeof(BaseStats))]
public class SaveableEntity : MonoBehaviour
{
    public IStats myStats;
    private string prefabName;
    [SerializeField] private string id = string.Empty;
    public string Id => id;

    private IDataService DataService;

    [SerializeField] private GameObjectTile currentLocation;

    public SaveableEntity()
    {
        DataService = new JsonDataService();
    }

    public SaveableEntity(IDataService dataService)
    {
        DataService = dataService;
    }

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

    public GameObjectTile GetCurrentLocation()
    {
        return currentLocation;
    }

    public void SetCurrentLocation(GameObjectTile newLocation)
    {
        currentLocation = newLocation;
    }

    public virtual bool SaveData<T>(T stats, int saveSlot)
    {
        if(!DataService.SaveData($"/{saveSlot}/Entity-{id}-Stats.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogWarning($"Could not save Entity-{id}-Stats!");
            return false;
        }
        return true;
    }

    public virtual T LoadData<T>(int saveSlot)
    {
        try
        {
            return DataService.LoadData<T>($"/{saveSlot}/Entity-{id}-Stats.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogWarning($"Could not load Entity-{id}-Stats: {e.Message}");
            return default;
        }
    }

    public virtual void SaveMyData(int saveSlot)
    {
        SaveData<IStats>(myStats, saveSlot);
    }

    public virtual void LoadMyData(int saveSlot)
    {
        myStats = LoadData<IStats>(saveSlot);
    }
}
