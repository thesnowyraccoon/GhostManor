using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Add a Pause System to your Game! - Top Down Unity 2D #17
// Game Code Library
// 11 Feb 2025
// Code Version: Unknown
// Available at: https://youtu.be/fspxIduosYQ?si=_vm6Td2PG3Pnm0PY 

public class MenuController : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject menuCanvas;

    [Header("Menus")]
    public GameObject[] menus;
    public Button[] buttons;

    public GameObject hotbarSlots;
    public Slider lookSlider;
    public FPController player;

    void Start()
    {
        menuCanvas.SetActive(false);
        //ActivateMenu(0);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!menuCanvas.activeSelf && PauseController.isPaused)
            {
                return;
            }

            menuCanvas.SetActive(!menuCanvas.activeSelf);
            //hotbarSlots.SetActive(menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);

            ActivateMenu(0);
        }
    }

    public void ActivateMenu(int menuNo)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

        //buttons[menuNo].Select();
        menus[menuNo].SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        if (!menuCanvas.activeSelf && PauseController.isPaused)
        {
            return;
        }

        menuCanvas.SetActive(!menuCanvas.activeSelf);
        //hotbarSlots.SetActive(menuCanvas.activeSelf);
        PauseController.SetPause(menuCanvas.activeSelf);
    }

    public void Back()
    {
        ActivateMenu(0);
    }

    public void SensitivitySlider()
    {
        player.SetLookSensivity(lookSlider.value);
    }
}
