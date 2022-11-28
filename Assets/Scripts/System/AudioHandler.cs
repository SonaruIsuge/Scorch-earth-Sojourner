using System.Collections;
using System.Collections.Generic;
using SonaruUtilities;
using UnityEngine;

public class AudioHandler : TSingletonMonoBehaviour<AudioHandler>
{
    private AudioSource audioSource;
    
    public float Volume;
    public List<AudioClip> ClipList;
    
    
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }
    
    
    public void SpawnAudio(int index)
    {
        audioSource.PlayOneShot(ClipList[index]);
    }
}
