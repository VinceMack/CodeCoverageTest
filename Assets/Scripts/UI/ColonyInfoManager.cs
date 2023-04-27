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


    /////////////////////////////////////
    // Colony Panel Methods
    /////////////////////////////////////

    public void OpenColonyInfo()
    {
        zoneNumber.text = (myColony.GetNextZoneNumber() - 1).ToString();
        colonyName.text = myColony.GetColonyName();
        pawnCount.text = LaborOrderManager.GetPawnCount().ToString();
    }

    /////////////////////////////////////
    // Zone UI Methods
    /////////////////////////////////////

    public void BuildZoneList()
    {
        foreach (Transform zoneChild in zoneContent.transform)
        {
            Destroy(zoneChild.gameObject);
        }

        foreach (Zone zone in myColony.GetZones())
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



