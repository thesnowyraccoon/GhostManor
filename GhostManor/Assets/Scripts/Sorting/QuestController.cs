using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<Quests.QuestProgress> activateQuests = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AcceptQuest(Quests quests)
    {
        if (IsQuestActive(quests.questID)) return;
        activateQuests.Add(new Quests.QuestProgress(quests));
    }

    //Allows only one quest per character (cant skip story)
    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.QuestID == questID);
}
