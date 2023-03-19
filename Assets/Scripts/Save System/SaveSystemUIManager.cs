using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveSystemUIManager : MonoBehaviour
{
    [SerializeField] private SaveSystemManager mySaveManager;
    [SerializeField] private GameObject saveSlot;
    [SerializeField] private GameObject content;

    private EntityDictionary entityDictionary;
    private EntityDictionaryStats entityStats = new EntityDictionaryStats();

    public int activeSlot = -1;

    public SaveSystemUIManager()
    {

    }

    public GameObject GetContent()
    {
        return content;
    }

    public void LoadActiveSlot()
    {
        if(activeSlot != -1 && Directory.Exists(Application.persistentDataPath + $"/{activeSlot}"))
        {
            GlobalInstance.Instance.entityDictionary.DestroyAllSaveableObjects();
            Load(activeSlot);
            OpenSaveMenu();
        }
    }

    public void SaveActiveSlot()
    {
        // Probably should add a warning if save already exists in slot.
        if(activeSlot != -1)
        {
            Save(activeSlot);
            OpenSaveMenu();
        }
    }

    public void DeleteActiveSlot()
    {
        if(activeSlot != -1)
        {
            if(Directory.Exists(Application.persistentDataPath + $"/{activeSlot}"))
            {
                Directory.Delete(Application.persistentDataPath + $"/{activeSlot}", true);
            }
            OpenSaveMenu();
        }
    }

    [ContextMenu("GenerateSaveSlots")]
    public void OpenSaveMenu()
    {
        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for(int i = 1; i <= Constants.SAVE_SLOT_NUMBER; i++)
        {
            GameObject slot = Instantiate(saveSlot);
            slot.transform.SetParent(content.transform);
            slot.GetComponent<SaveSlot>().Initialize(i, mySaveManager, this, Directory.Exists(Application.persistentDataPath + $"/{i}"));
        }
    }

    [ContextMenu("Save")]
    public void Save(int saveSlot)
    {
        // We want to wipe out any data that already exists (if it does)
        if(Directory.Exists(Application.persistentDataPath + $"/{saveSlot}"))
        {
            Directory.Delete(Application.persistentDataPath + $"/{saveSlot}", true);
        }
        // Need to create 
        Directory.CreateDirectory(Application.persistentDataPath + $"/{saveSlot}");

        entityDictionary = GlobalInstance.Instance.entityDictionary;
        foreach(KeyValuePair<string, GameObject> kvp in entityDictionary.entityDictionary)
        {
            SaveableEntity currentEntity = kvp.Value.GetComponent<SaveableEntity>();

            if(currentEntity != null)
            {
                currentEntity.SaveMyData(saveSlot);
            }

            entityStats.entitiesInScene.Add(currentEntity.Id, currentEntity.GetPrefabName());
        }
        mySaveManager.SaveData<EntityDictionaryStats>(entityStats, saveSlot);
        mySaveManager.SaveInfo(saveSlot);
    }

    [ContextMenu("Load")]
    public void Load(int saveSlot, bool mainMenu = false, int sceneLoad = 0)
    {
        if(mainMenu)
        {
            SceneManager.LoadScene(sceneLoad);
        }
        entityDictionary = GlobalInstance.Instance.entityDictionary;
        EntityDictionaryStats entity = mySaveManager.LoadData<EntityDictionaryStats>(saveSlot);

        GlobalInstance.Instance.entityDictionary.entityDictionary.Clear();
        foreach(KeyValuePair<string, string> kvp in entity.entitiesInScene)
        {
            GameObject loadedEntity = entityDictionary.InstantiateEntity(kvp.Value, kvp.Key);
            SaveableEntity saveableComp = loadedEntity.GetComponent<SaveableEntity>();
            saveableComp.LoadMyData(saveSlot);
        }
    }
}
