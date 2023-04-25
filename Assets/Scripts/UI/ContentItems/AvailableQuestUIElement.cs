using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvailableQuestUIElement : MonoBehaviour
{
    private Quest myQuest;
    private QuestUIManager myManager;

    [SerializeField] private TextMeshProUGUI requiredItemName;
    [SerializeField] private TextMeshProUGUI requiredItemQuantity;

    [SerializeField] private TextMeshProUGUI rewardItemName;
    [SerializeField] private TextMeshProUGUI rewardItemQuantity;

    public void Initialize(Quest quest, QuestUIManager manager)
    {
        myQuest = quest;
        myManager = manager;

        requiredItemName.text = quest.GetRequirementItem().DisplayName;
        requiredItemQuantity.text = "*" + quest.GetRequirementItem().Quantity.ToString();

        rewardItemName.text = quest.GetRewardItem().DisplayName;
        rewardItemQuantity.text = "*" + quest.GetRewardItem().Quantity.ToString();
    }

    public void Accept()
    {
        myManager.AcceptQuest(myQuest);
        myManager.ReloadPendingQuestPanel();
    }

    public void Decline()
    {
        myManager.DeclineQuest(myQuest);
        myManager.ReloadPendingQuestPanel();
    }
}
