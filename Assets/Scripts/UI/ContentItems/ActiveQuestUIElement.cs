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

        requiredItemName.text = quest.GetRequirementItem().DisplayName;
        requiredItemQuantity.text = "*" + quest.GetRequirementItem().Quantity.ToString();

        rewardItemName.text = quest.GetRewardItem().DisplayName;
        rewardItemQuantity.text = "*" + quest.GetRewardItem().Quantity.ToString();

        statusItemName.text = quest.GetRequirementItem().DisplayName;
        statusItemQuantity.text = "*" + myQuest.GetStatus().ToString();

        if(myQuest.GetStatus() >= myQuest.GetRequirementItem().Quantity)
        {
            finishQuestButton.SetActive(true);
        }
    }

    public void UpdateStatus()
    {
        statusItemQuantity.text = "*" + myQuest.GetStatus().ToString();

        if(myQuest.GetStatus() >= myQuest.GetRequirementItem().Quantity)
        {
            finishQuestButton.SetActive(true);
        }
    }

    public void FinishQuest()
    {
        if(myQuest.CompleteQuest())
        {
            myManager.RemoveActiveQuest(myQuest);
            return;
        }
        UpdateStatus();
    }
}
