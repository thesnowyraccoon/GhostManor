using System;
using System.Collections.Generic;
using UnityEngine;

// Create a Quest System with Scriptable Objects - Top Down Unity 2D #25
// Game Code Library
// 7 May 2025 
// Code Version: Unknown
// Available at: https://youtu.be/_hA3y45P4Ow?si=chsZa7y1_6Eb68jw

[CreateAssetMenu(menuName = "Objectives/Objective")]
public class Objective : ScriptableObject
{
    public string objectiveID;
    public string objectiveName;
    public string description;
    public List<ObjectiveTask> tasks;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(objectiveID))
        {
            objectiveID = objectiveName + Guid.NewGuid().ToString();
        }
    }
}

[System.Serializable]
public class ObjectiveTask
{
    public string taskID;
    public string description;
    public TaskType type;
    public int requiredAmount;
    public int currentAmount;

    public bool isCompleted => currentAmount >= requiredAmount;
}

public enum TaskType { CollectItem, ReachLocation, TalkNPC, Custom }

[System.Serializable]
public class ObjectiveProgress
{
    public Objective objective;
    public List<ObjectiveTask> tasks;

    public ObjectiveProgress(Objective objective)
    {
        this.objective = objective;
        tasks = new List<ObjectiveTask>();

        foreach (var task in objective.tasks)
        {
            tasks.Add(new ObjectiveTask
            {
                taskID = task.taskID,
                description = task.description,
                type = task.type,
                requiredAmount = task.requiredAmount,
                currentAmount = 0
            });
        }
    }

    public bool isCompleted => tasks.TrueForAll(t => t.isCompleted);

    public string ObjectiveID => objective.objectiveID;
}

