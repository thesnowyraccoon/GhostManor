using UnityEngine;
using UnityEngine.InputSystem;

//6 Minute PAUSE MENU Unity Tutorial
//BMo
//May 18 2020
//Code Version: Unknown
//Available at:https://youtu.be/9dYDBomQpBQ?si=qfuc1Y4Ob3UXEmVq

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    public GameObject pauseMenu;
    //Animator pauseAnimation;
    //bool isPlaying = false;

    public GameObject hotbarSlots;
    public GameObject dialogueBox;

    [Header("Controls")]
    public GameObject controlsMenu;

    [Header("Settings")]
    public GameObject settingsMenu;
    
    [Header("Quests")]
    public GameObject questsMenu;
    
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        questsMenu.SetActive(false);
    }

    public void PauseGame()
    {
        questsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        hotbarSlots.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        dialogueBox.SetActive(false);
        
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        hotbarSlots.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        dialogueBox.SetActive(false);
    }

    public void Contols()
    {
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        dialogueBox.SetActive(false);
        isPaused = true;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
         Time.timeScale = 0f;
        dialogueBox.SetActive(false);
        isPaused = true;
    }

    public void Quests()
    {
        questsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Back()
    {
        PauseGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


}

