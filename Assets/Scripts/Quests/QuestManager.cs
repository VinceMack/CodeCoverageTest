using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    public QuestInfo(string requiredName, int requiredQuantity, string rewardName, int rewardQuantity)
    {
        requiredItemName = requiredName;
        requiredItemQuantity = requiredQuantity;
        rewardItemName = rewardName;
        rewardItemQuantity = rewardQuantity;
    }

    public string requiredItemName;
    public int requiredItemQuantity;

    public string rewardItemName;
    public int rewardItemQuantity;
}

public class QuestManager : MonoBehaviour
{
    // First string, int is name and quantity of required item, second is for reward
    private List<QuestInfo> possibleQuests = new List<QuestInfo> 
    { 
        new QuestInfo("wood", 64, "bread", 16),
        new QuestInfo("wood", 32, "bread", 4),
        new QuestInfo("wood", 16, "bread", 1),
        new QuestInfo("berry", 32, "bread", 5),
        new QuestInfo("honey", 5, "bread", 5)
    };

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> pendingQuests = new List<Quest>();

    [SerializeField] private Colony myColony;

    private void OnEnable() 
    {
        CreateRandomQuest();
        CreateRandomQuest();
        CreateRandomQuest();
    }

    public List<Quest> GetActiveQuestList()
    {
        return activeQuests;
    }

    public List<Quest> GetPendingQuestList()
    {
        return pendingQuests;
    }

    public void AcceptQuest(Quest acceptedQuest)
    {
        activeQuests.Add(acceptedQuest);
        pendingQuests.Remove(acceptedQuest);
    }

    public void DeclineQuest(Quest declinedQuest)
    {
        pendingQuests.Remove(declinedQuest);
    }

    public void RemoveActiveQuest(Quest activeQuest)
    {
        activeQuests.Remove(activeQuest);
    }

    public void CheckQuestCompletion()
    {
        foreach(Quest quest in activeQuests)
        {
            quest.CheckCondition();
        }
    }

    public void CreateRandomQuest()
    {
        // Getting a random index to generate a random quest
        int questNumber = Random.Range(0, possibleQuests.Count);
        QuestInfo questInfo = possibleQuests[questNumber];

        // Creating the required item
        Item requiredItem = new Item(ItemList.itemList[questInfo.requiredItemName]);
        requiredItem.Quantity = questInfo.requiredItemQuantity;

        // Creating the reward item
        Item rewardItem = new Item(ItemList.itemList[questInfo.rewardItemName]);
        rewardItem.Quantity = questInfo.rewardItemQuantity;

        pendingQuests.Add(new Quest(rewardItem, requiredItem, myColony));
    }
}
