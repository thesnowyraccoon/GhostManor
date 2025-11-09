using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public Transform pillars;
    public GameObject endScreen;

    void Start()
    {
        endScreen.SetActive(false);
    }

    private bool CheckPillars()
    {
        foreach (Transform child in pillars)
        {
            if (child.GetChild(0).childCount <= 0)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckEnd()
    {
        if (CheckPillars())
        {
            PauseController.SetPause(true);

            endScreen.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
