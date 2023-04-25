using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveQuestUIElement : MonoBehaviour
{
    private Quest myQuest;
    private QuestUIManager myManager;

    [SerializeField] private TextMeshProUGUI requiredItemName;
    [SerializeField] private TextMeshProUGUI requiredItemQuantity;

    [SerializeField] private TextMeshProUGUI rewardItemName;
    [SerializeField] private TextMeshProUGUI rewardItemQuantity;

    [SerializeField] private TextMeshProUGUI statusItemName;
    [SerializeField] private TextMeshProUGUI statusItemQuantity;

    [SerializeField] private GameObject finishQuestButton;

    public void Initialize(Quest quest, QuestUIManager manager)
    {
        myQuest = quest;
        myManager = manager;

        requiredItemName.text = quest.GetRequirementItemName();
        requiredItemQuantity.text = "*" + quest.GetRequirementItemCount().ToString();

        rewardItemName.text = quest.GetRewardItemName();
        rewardItemQuantity.text = "*" + quest.GetRewardItemCount().ToString();

        statusItemName.text = quest.GetRequirementItemName();
        statusItemQuantity.text = "*" + myQuest.GetStatus().ToString();

        if(myQuest.GetStatus() >= myQuest.GetRequirementItemCount())
        {
            finishQuestButton.SetActive(true);
        }
    }

    public void UpdateStatus()
    {
        statusItemQuantity.text = "*" + myQuest.GetStatus().ToString();

        if(myQuest.GetStatus() >= myQuest.GetRequirementItemCount())
        {
            finishQuestButton.SetActive(true);
        }
    }

    public void FinishQuest()
    {
        // if(myQuest.CompleteQuest())
        // {
        //     myManager.RemoveActiveQuest(myQuest);
        //     return;
        // }
        // UpdateStatus();
    }
}
