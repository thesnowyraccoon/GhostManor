using System.Collections.Generic;
using UnityEngine;

// Create a Quest System with Scriptable Objects - Top Down Unity 2D #25
// Game Code Library
// 7 May 2025 
// Code Version: Unknown
// Available at: https://youtu.be/_hA3y45P4Ow?si=chsZa7y1_6Eb68jw

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private FPController player;
    [SerializeField] private HotbarController hotbar;

    public static ObjectiveController Instance { get; private set; }
    public List<Objective> activeObjectives = new();
    public List<string> handinObjectiveIDs = new();
    
    private ObjectiveUI objectiveUI;
 
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        objectiveUI = FindAnyObjectByType<ObjectiveUI>();
    }

    public void AcceptObjective(Objective objective)
    {
        if (IsObjActive(objective.objectiveID)) return;

        Debug.Log("Quest Accepted");

        activeObjectives.Add(objective);

        objectiveUI.UpdateObjectiveUI();
    }

    public bool IsObjActive(string objectiveID)
    {
        for (int i = 0; i < activeObjectives.Count; i++)
        {
            if (activeObjectives[i].objectiveID == objectiveID)
            {
                return true;
            }
        }
        
        return false;
    }

    public bool IsObjCompleted(string objectiveID)
    {
        for (int i = 0; i < handinObjectiveIDs.Count; i++)
        {
            if (handinObjectiveIDs[i] == objectiveID)
            {
                return true;
            }
        }
        
        return false;
    }

    public void HandInObjective(string objectiveID)
    {
        if (!RemoveObjItems(objectiveID))
        {
            return;
        }

        ObjectiveProgress objective = activeObjectives.Find(o => o.ObjectiveID == objectiveID);

        if (objective != null)
        {
            handinObjectiveIDs.Add(objectiveID);

            activeObjectives.Remove(objective);
            objectiveUI.UpdateObjectiveUI();
        }
    }

    public bool isObjHandedIn(string objectiveID)
    {
        return handinObjectiveIDs.Contains(objectiveID);
    }

    public bool RemoveObjItems(string objectiveID)
    {
        ObjectiveProgress objective = activeObjectives.Find(o => o.ObjectiveID == objectiveID);

        if (objective == null) return false;

        Dictionary<int, int> requiredItems = new();

        foreach (ObjectiveTask task in objective.tasks)
        {
            if (task.type == TaskType.CollectItem && int.TryParse(task.taskID, out int itemID))
            {
                requiredItems[itemID] = task.requiredAmount;
            }
        }

        foreach (var itemRequirement in requiredItems)
        {
            hotbar.RemoveItem(player.heldObject.gameObject);
            Destroy(player.heldObject.gameObject);
        }

        return true;
    }
}
