using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    /*[SerializeField] private QuestManager questManager;

    // Panels
    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject availableQuestPanel;
    [SerializeField] private GameObject activeQuestPanel;

    // List contents
    [SerializeField] private GameObject activeQuestListContent;
    [SerializeField] private GameObject pendingQuestListContent;

    // UI List Elements
    [SerializeField] private GameObject activeQuestUIElement;
    [SerializeField] private GameObject pendingQuestUIElement;

    public void AvailableQuestListClicked()
    {
        availableQuestPanel.SetActive(true);
        activeQuestPanel.SetActive(false);
        ReloadPendingQuestPanel();
    }

    public void ActiveQuestListClicked()
    {
        activeQuestPanel.SetActive(true);
        availableQuestPanel.SetActive(false);
        ReloadActiveQuestPanel();
    }

    public void ToggleQuestPanel()
    {
        if(questPanel.activeSelf)
        {
            questPanel.SetActive(false);
        }
        else
        {
            questPanel.SetActive(true);
            availableQuestPanel.SetActive(true);
            activeQuestPanel.SetActive(false);
            ReloadPendingQuestPanel();
        }
    }
    
    public void ReloadActiveQuestPanel()
    {
        foreach(Transform child in activeQuestListContent.transform)
        {
            Destroy(child.gameObject);
        }

        List<Quest> questList = questManager.GetActiveQuestList();
        foreach(Quest quest in questList)
        {
            GameObject newElement = Instantiate(activeQuestUIElement);
            newElement.GetComponent<ActiveQuestUIElement>().Initialize(quest, this);
            newElement.transform.SetParent(activeQuestListContent.transform);
        }
    }

    public void ReloadPendingQuestPanel()
    {
        foreach(Transform child in pendingQuestListContent.transform)
        {
            Destroy(child.gameObject);
        }

        List<Quest> questList = questManager.GetPendingQuestList();
        foreach(Quest quest in questList)
        {
            GameObject newElement = Instantiate(pendingQuestUIElement);
            newElement.GetComponent<AvailableQuestUIElement>().Initialize(quest, this);
            newElement.transform.SetParent(pendingQuestListContent.transform);
        }
    }

    public void UpdateActiveQuestProgression()
    {
        foreach(Transform missionElement in activeQuestListContent.transform)
        {
            ActiveQuestUIElement uiElement = missionElement.gameObject.GetComponent<ActiveQuestUIElement>();
            if(uiElement != null)
            {
                uiElement.UpdateStatus();
            }
        }
    }

    public void AcceptQuest(Quest acceptedQuest)
    {
        questManager.AcceptQuest(acceptedQuest);
        ReloadPendingQuestPanel();
    }

    public void DeclineQuest(Quest declinedQuest)
    {
        questManager.DeclineQuest(declinedQuest);
        ReloadPendingQuestPanel();
    }

    public void RemoveActiveQuest(Quest activeQuest)
    {
        questManager.RemoveActiveQuest(activeQuest);
        ReloadActiveQuestPanel();
    }*/
}
