using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests")]
public class Quests : ScriptableObject
{
    public string questID; //ensures each quest has a different ID
    public string questName;
    public string description;
    public List<QuestObjective> objective;

    private void OnValidate() //whenever the scriptable object is edited
    {
        if (string.IsNullOrEmpty(questID))
        {
            questID = questName + Guid.NewGuid().ToString(); // gives each quest a unique ID
        }
    }

    [System.Serializable]

    public class QuestObjective
    {
        public string objectiveID; // Unique Identify of what the objective is
        public string description;
        public int requiredAmount;
        public int currentAmount;
        public bool IsCompleted => currentAmount >= requiredAmount;
    }

    [System.Serializable]
    public class QuestProgress
    {
        public Quests quest; //quests we accept 
        public List<QuestObjective> objective;

        public QuestProgress(Quests quest)
        {
            this.quest = quest; //goes into the "Quest slot'
            objective = new List<QuestObjective>();

            //avoid modifying quests
            foreach (var obj in quest.objective)
            {
                objective.Add(new QuestObjective
                {
                    objectiveID = obj.objectiveID,
                    description = obj.description,
                    requiredAmount = obj.requiredAmount,
                    currentAmount = 0
                });
            }
        }

         public bool IsCompleted => objective.TrueForAll(o => o.IsCompleted);
        public string QuestID => quest.questID;
    }

}
