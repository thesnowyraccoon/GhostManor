using System.Collections.Generic;
using UnityEngine;

//Add a Sound Effect Manager to Your Game - 2D Platformer Unity #26
//Game Code Library
//Feb 16 2024
//Code Version: Unknown
//Available at: https://youtu.be/rAX_r0yBwzQ?si=mfKw2zM2xn2QU5tB

public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }
    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            soundDictionary[soundEffectGroup.soundName] = soundEffectGroup.audioClips;
        }
    }

    public AudioClip GetRandomClip(string soundName) //Randomises sound effect 
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            List<AudioClip> audioClips = soundDictionary[soundName];
            if (audioClips.Count > 0)
            {
                return audioClips[Random.Range(0, audioClips.Count)];
            }
        }
        return null;
    }

    [System.Serializable] //accessible in the inspector
    public struct SoundEffectGroup
    {
        public string soundName;
        public List<AudioClip> audioClips;
    }
}
