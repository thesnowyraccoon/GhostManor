using UnityEngine;
using UnityEngine.UI;

//Add a Sound Effect Manager to Your Game - 2D Platformer Unity #26
//Game Code Library
//Feb 16 2024
//Code Version: Unknown
//Available at: https://youtu.be/rAX_r0yBwzQ?si=mfKw2zM2xn2QU5tB


//SoundManager.Play("SoundName");
public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;
    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    //UI
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public static void Play(string effectName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(effectName);
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }


    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
