using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioInfo> sounds=new List<AudioInfo>();

    public void PlaySound2D(string key)
    {
        audioSource.PlayOneShot(sounds.Find(a=>a.audioKey==key).audioClip);
    }
}

[Serializable]
public class AudioInfo
{
    public string audioKey;
    public AudioClip audioClip;
}
