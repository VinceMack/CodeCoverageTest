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
        entityDictionary = GlobalInstance.Instance.entityDictionary;
        foreach(KeyValuePair<string, GameObject> kvp in entityDictionary.entityDictionary)
        {
            SaveableEntity currentEntity = kvp.Value.GetComponent<SaveableEntity>();

            if(currentEntity != null)
            {
                Debug.Log("Saveable Entity found");
                Debug.Log(currentEntity.GetType());
                currentEntity.SaveMyData();
            }

            entityStats.entitiesInScene.Add(currentEntity.Id, currentEntity.GetPrefabName());
        }
        SaveData<EntityDictionaryStats>(entityStats, 1);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        entityDictionary = GlobalInstance.Instance.entityDictionary;
        EntityDictionaryStats entity = LoadData<EntityDictionaryStats>(1);
        foreach(KeyValuePair<string, string> kvp in entity.entitiesInScene)
        {
            GameObject loadedEntity = entityDictionary.InstanitateEntity(kvp.Value, kvp.Key);
            SaveableEntity saveableComp = loadedEntity.GetComponent<SaveableEntity>();
            saveableComp.LoadMyData();
        }
    }

    public virtual void SaveData<T>(T stats, int saveNumber)
    {
        if(!DataService.SaveData($"/Save-{saveNumber}.json", stats, Constants.ENCRYPT_SAVE_DATA))
        {
            Debug.LogError($"Could not save Save-{saveNumber}!");
        }
    }

    public virtual T LoadData<T>(int saveNumber)
    {
        try
        {
            return DataService.LoadData<T>($"/Save-{saveNumber}.json", Constants.ENCRYPT_SAVE_DATA);
        }
        catch(Exception e)
        {
            Debug.LogError($"Could not load Save-{saveNumber}: {e.Message}");
            return default;
        }
    }

    public void ClearData(int saveNumber)
    {
        string path = Application.persistentDataPath + $"/Save-{saveNumber}.json";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
