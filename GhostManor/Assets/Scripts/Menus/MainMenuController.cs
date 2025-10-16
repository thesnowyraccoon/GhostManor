using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenuController : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    public GameObject mainMenuCanvas;
    public GameObject mainMenuCamera;

    private Animator animator;

    void Start()
    {
        UI.SetActive(false);
        player.SetActive(false);

        mainMenuCanvas.SetActive(true);
        mainMenuCamera.SetActive(true);

        animator = GetComponent<Animator>();
    }

    public void OnPlay()
    {
        mainMenuCanvas.SetActive(false);

        animator.SetBool("isActive", true);
    }

    public void PlayEvent()
    {
        UI.SetActive(true);
        player.SetActive(true);

        mainMenuCamera.SetActive(false);
    }
}
