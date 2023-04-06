using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColonyInfoManager : MonoBehaviour
{
    [SerializeField] private Colony myColony;

    [SerializeField] private TextMeshProUGUI colonyName;
    [SerializeField] private TextMeshProUGUI zoneNumber;

    [SerializeField] private GameObject homeInfo;
    [SerializeField] private GameObject zoneInfo;

    [SerializeField] private GameObject zoneContent;

    [SerializeField] private GameObject zoneInfoTile;

    public void OpenColonyInfo()
    {
        zoneNumber.text = (myColony.GetNextFarmZoneNumber() - 1).ToString();
        colonyName.text = myColony.GetColonyName();
    }

    public void BuildZoneList()
    {
        foreach(Transform zoneChild in zoneContent.transform)
        {
            Destroy(zoneChild.gameObject);
        }

        foreach(FarmZone zone in myColony.GetFarmZones())
        {
            GameObject zoneTile = Instantiate(zoneInfoTile, new Vector3(), new Quaternion());
            zoneTile.transform.SetParent(zoneContent.transform);
            zoneTile.GetComponent<ZoneUIElement>().Initialize(zone, this);
        }
    }

    public void ShowZones()
    {
        Debug.Log("ShowingZones");
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
