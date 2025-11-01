using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// How to make a LOADING BAR in Unity
// Brackeys
// 14 Jun 2017
// Code Version: Unknown
// Available at: https://youtu.be/YMj2qPq9CP8?si=HR-7Em-YaORENLz9

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public Animator animator;

    public void IntroAnimation()
    {
        animator.SetTrigger("Start");
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
