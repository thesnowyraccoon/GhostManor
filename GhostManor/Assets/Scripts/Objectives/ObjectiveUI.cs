using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    public Transform objListContent;
    public GameObject objEntryPrefab;
    public GameObject taskTextPrefab;

    void Start()
    {
        UpdateObjectiveUI();
    }

    public void UpdateObjectiveUI()
    {
        foreach (Transform child in objListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var objective in ObjectiveController.Instance.activeObjectives)
        {
            GameObject entry = Instantiate(objEntryPrefab, objListContent);
            TMP_Text objNameText = entry.transform.Find("ObjectiveNameText").GetComponent<TMP_Text>();
            Transform taskList = entry.transform.Find("TaskList");

            objNameText.text = objective.objective.name;

            foreach (var task in objective.tasks)
            {
                GameObject taskTextGO = Instantiate(taskTextPrefab, taskList);
                TMP_Text taskText = taskTextGO.GetComponent<TMP_Text>();
                taskText.text = $"{task.description} ({task.currentAmount}/{task.requiredAmount})";
            }
        }
    }
}
