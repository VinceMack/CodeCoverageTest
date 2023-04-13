using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceListElement : MonoBehaviour
{
    private Colony myColony;

    private Resource resource;

    [SerializeField] private TextMeshProUGUI resourceQuantityText;
    [SerializeField] private TextMeshProUGUI resourceNameText;
    [SerializeField] private Image resourceIcon;

    public void Initialize(Colony colony, Resource resource)
    {
        this.resource = resource;
        myColony = colony;

        resourceNameText.text = resource.displayName;
        resourceQuantityText.text = colony.GetResourceQuantity(resource.resourceName).ToString(); //Get value
        resourceIcon.sprite = Resources.Load<Sprite>(resource.resourceIconLocation);

        colony.RegisterResourceListElement(this, resource.resourceName);
    }

    public void UpdateValue()
    {
        resourceQuantityText.text = myColony.GetResourceQuantity(resource.resourceName).ToString(); //Get value
    }
}
