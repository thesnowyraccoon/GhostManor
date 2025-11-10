using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

// Add a Pause System to your Game! - Top Down Unity 2D #17
// Game Code Library
// 11 Feb 2025
// Code Version: Unknown
// Available at: https://youtu.be/fspxIduosYQ?si=_vm6Td2PG3Pnm0PY 

public class MenuController : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menuCanvas;
    public Animator animator;

    [Header("Sub-menus")]
    public GameObject[] menus;
    public Button[] buttons;

    [Header("Player")]
    public GameObject hotbarSlots;
    public Slider lookSlider;
    public Slider brightSlider;
    public FPController player;

    [Header("Post Processing")]
    public Volume volume;

    void Start()
    {
        menuCanvas.SetActive(false);
        //ActivateMenu(0);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        ActivateMenu(0);

        if (context.performed)
        {
            AnimateMenu();
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
        hotbarSlots.SetActive(!menuCanvas.activeSelf);
        PauseController.SetPause(menuCanvas.activeSelf);

        ActivateMenu(0);
    }

    public void Back()
    {
        ActivateMenu(0);
    }

    public void SensitivitySlider()
    {
        player.SetLookSensivity(lookSlider.value);
    }

    public void BrightnessSlider()
    {
        ColorAdjustments colorAdjustments;

        if (volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = brightSlider.value;
        }
    }

    public void AnimateMenu()
    {
        if (!menuCanvas.activeSelf)
        {
            animator.SetBool("isOpen", true);
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }
}
