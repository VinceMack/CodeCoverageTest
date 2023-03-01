using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSystemManager : MonoBehaviour
{
    private EntityDictionary entityDictionary;
    private EntityDictionaryStats entityStats = new EntityDictionaryStats();
    private IDataService DataService = new JsonDataService();

    [ContextMenu("Save")]
    public void Save(int saveSlot)
    {
        //Need to create the save slot folder if it doesn't already exist.
        // Maybe should clear it
        if(!Directory.Exists(Application.persistentDataPath + $"/{saveSlot}"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{saveSlot}");
        }

        entityDictionary = GlobalInstance.Instance.entityDictionary;
        foreach(KeyValuePair<string, GameObject> kvp in entityDictionary.entityDictionary)
        {
            SaveableEntity currentEntity = kvp.Value.GetComponent<SaveableEntity>();

            if(currentEntity != null)
            {
                Debug.Log("Saveable Entity found");
                Debug.Log(currentEntity.GetType());
                currentEntity.SaveMyData(saveSlot);
            }

            entityStats.entitiesInScene.Add(currentEntity.Id, currentEntity.GetPrefabName());
        }
        SaveData<EntityDictionaryStats>(entityStats, saveSlot);
        SaveStats(saveSlot);
    }

    [ContextMenu("Load")]
    public void Load(int saveSlot)
    {
        entityDictionary = GlobalInstance.Instance.entityDictionary;
        EntityDictionaryStats entity = LoadData<EntityDictionaryStats>(saveSlot);
        foreach(KeyValuePair<string, string> kvp in entity.entitiesInScene)
        {
            GameObject loadedEntity = entityDictionary.InstanitateEntity(kvp.Value, kvp.Key);
            SaveableEntity saveableComp = loadedEntity.GetComponent<SaveableEntity>();
            saveableComp.LoadMyData(saveSlot);
        }
    }

    public virtual void SaveData<T>(T stats, int saveNumber)
    {
        if(!DataService.SaveData($"/{saveNumber}/Save-{saveNumber}.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Save-{saveNumber}!");
        }
    }

    public virtual T LoadData<T>(int saveNumber)
    {
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
