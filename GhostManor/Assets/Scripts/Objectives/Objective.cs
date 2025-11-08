using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

// Create a Quest System with Scriptable Objects - Top Down Unity 2D #25
// Game Code Library
// 7 May 2025 
// Code Version: Unknown
// Available at: https://youtu.be/_hA3y45P4Ow?si=chsZa7y1_6Eb68jw

[CreateAssetMenu(menuName = "Objectives/Objective")]
public class Objective : ScriptableObject
{
    [Header("Details")]
    public string objectiveID;
    public string objectiveName;

    [Header("Item/Reward")]
    public int objectiveItemID;
    public GameObject objectiveReward;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(objectiveID))
        {
            objectiveID = objectiveName + Guid.NewGuid().ToString();
        }
    }
}