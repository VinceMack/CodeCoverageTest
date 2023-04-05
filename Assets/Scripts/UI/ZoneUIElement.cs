using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZoneUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI zoneName;
    [SerializeField] private TextMeshProUGUI zoneType;

    private Zone myZone;
    private ColonyInfoManager myInfoManager;

    public void Initialize(Zone zone, ColonyInfoManager colonyInfoManager)
    {
        myZone = zone;
        zoneName.text = myZone.GetZoneName();
        zoneType.text = myZone.GetZoneType().ToString();
        myInfoManager = colonyInfoManager;
    }

    public void GoTo()
    {
        Vector2 location = myZone.GetCenter();
        GlobalInstance.Instance.sceneCamera.transform.position = new Vector3(location.x, location.y, GlobalInstance.Instance.sceneCamera.transform.position.z);
    }

    public void Delete()
    {
        myZone.DeleteZone();
        myInfoManager.BuildZoneList();
    }
}
