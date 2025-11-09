using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    public Transform objHolder;
    public GameObject objEntryPrefab;

    void Start()
    {
        UpdateObjectiveUI();
    }

    public void UpdateObjectiveUI()
    {
        foreach (Transform child in objHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (var objective in ObjectiveController.Instance.activeObjectives)
        {
            GameObject entry = Instantiate(objEntryPrefab, objHolder);
            TMP_Text objNameText = entry.transform.Find("ObjectiveNameText").GetComponent<TMP_Text>();

            objNameText.text = objective.objectiveName;
        }
    }
}
