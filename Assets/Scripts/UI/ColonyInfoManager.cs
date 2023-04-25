/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColonyInfoManager : MonoBehaviour
{
    [SerializeField] private Colony myColony;

    // Colony Panel Variables
    [SerializeField] private TextMeshProUGUI colonyName;
    [SerializeField] private TextMeshProUGUI zoneNumber;

    [SerializeField] private GameObject homeInfo;
    [SerializeField] private GameObject zoneInfo;

    [SerializeField] private GameObject zoneContent;

    [SerializeField] private GameObject zoneInfoTile;

    [SerializeField] private TextMeshProUGUI pawnCount;

    // Resource List Variables
    [SerializeField] private GameObject resourceListContent;

    [SerializeField] private GameObject resourceListElement;

    /////////////////////////////////////
    // Resource List Methods
    /////////////////////////////////////

    [ContextMenu("InitializeList")]
    public void InitializeResourceList()
    {
        // Remove the whole list
        foreach(Transform child in resourceListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Build the whole list
        foreach(KeyValuePair<string, Resource> kvp in ResourceManager.resourceDictionary)
        {
            GameObject newElement = Instantiate(resourceListElement);
            newElement.transform.SetParent(resourceListContent.transform);

            newElement.GetComponent<ResourceListElement>().Initialize(myColony, kvp.Value);
        }
    }

    /////////////////////////////////////
    // Colony Panel Methods
    /////////////////////////////////////

    public void OpenColonyInfo()
    {
        zoneNumber.text = (myColony.GetNextZoneNumber() - 1).ToString();
        colonyName.text = myColony.GetColonyName();
        pawnCount.text = LaborOrderManager_VM.GetPawnCount().ToString();
    }

    /////////////////////////////////////
    // Zone UI Methods
    /////////////////////////////////////

    public void BuildZoneList()
    {
        foreach(Transform zoneChild in zoneContent.transform)
        {
            Destroy(zoneChild.gameObject);
        }

        foreach(Zone zone in myColony.GetZones())
        {
            GameObject zoneTile = Instantiate(zoneInfoTile, new Vector3(), new Quaternion());
            zoneTile.transform.SetParent(zoneContent.transform);
            zoneTile.GetComponent<ZoneUIElement>().Initialize(zone, this);
        }
    }

    public void ShowZones()
    {
        homeInfo.SetActive(false);
        zoneInfo.SetActive(true);
        BuildZoneList();
    }

    public void HideZones()
    {
        OpenColonyInfo();
        zoneInfo.SetActive(false);
        homeInfo.SetActive(true);
    }
}


*/