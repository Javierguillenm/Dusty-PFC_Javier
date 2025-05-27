using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
public enum SoundType
{
    STEPWOOD,
        BITE,
        ATTACKGLASS,
        ATTACKWOOD,
        ATTACKMETAL,
        DOOR,
        JUMP,
        MENUBUTTONS,
        DEAD,
        DUST,
        KEY,
        LOCK,
        PAUSE,
        GAMEOVER,
        WEAPONBREAK,
        MAXILLADEAD,
        MAXILLASOUND,
        MANUSIDLESOUND,
        MANUSSEESPLAYER,
        MANUSATTACK,
        MANUSDEAD,
        OCULUSATTACK,
        OCULUSSEESPLAYER,
        OCULUSDEAD

}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundlist;
   private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundlist[(int)sound].Sounds;
        AudioClip randomclip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomclip, volume);
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize( ref soundlist, names.Length);
        for (int i = 0; i < soundlist.Length; i++)
        {
            soundlist[i].name = names[i];
        }
    }
#endif
    [Serializable]
    public struct SoundList
    {
        public AudioClip[] Sounds { get => sounds;  }
        [HideInInspector] public string name;
        [SerializeField] private AudioClip[] sounds;
    }
}
