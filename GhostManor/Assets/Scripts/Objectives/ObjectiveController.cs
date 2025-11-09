using System.Collections.Generic;
using UnityEngine;

// Create a Quest System with Scriptable Objects - Top Down Unity 2D #25
// Game Code Library
// 7 May 2025 
// Code Version: Unknown
// Available at: https://youtu.be/_hA3y45P4Ow?si=chsZa7y1_6Eb68jw

public class ObjectiveController : MonoBehaviour
{
    [Header("Player")]  
    [SerializeField] private FPController player;
    [SerializeField] private HotbarController hotbar;

    [Header("Objectives")]
    public static ObjectiveController Instance { get; private set; }
    public List<Objective> activeObjectives = new();
    public List<string> completedObjectives = new();

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

        Debug.Log("Objective Accepted");

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
        Objective objective = activeObjectives.Find(o => o.objectiveID == objectiveID);

        if (objective != null && player.heldObject != null)
        {
            Debug.Log("Checking");

            if (player.heldObject.itemID == objective.objectiveItemID) return true;
        }
        
        return false;
    }

    public void HandInObjective(string objectiveID)
    {
        if (!RemoveObjItems(objectiveID))
        {
            return;
        }

        Objective handinObjective = activeObjectives.Find(o => o.objectiveID == objectiveID);

        if (handinObjective != null)
        {
            completedObjectives.Add(objectiveID);

            activeObjectives.Remove(handinObjective);
            objectiveUI.UpdateObjectiveUI();
        }
    }

    public bool isObjHandedIn(string objectiveID)
    {
        return completedObjectives.Contains(objectiveID);
    }

    public bool RemoveObjItems(string objectiveID)
    {
        Objective objective = activeObjectives.Find(o => o.objectiveID == objectiveID);

        if (objective == null) return false;

        if (player == null || hotbar == null) return false;

        GameObject held = player.heldObject.gameObject;

        player.heldObject.Drop();
        player.heldObject = null;

        hotbar.RemoveItem(held);

        Destroy(held);
        
        hotbar.RebuildHotbar();

        return true;
    }
}
