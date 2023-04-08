using System.Collections;
using System.Collections.Generic;
using SonaruUtilities;
using UnityEngine;

public class AudioHandler : TSingletonMonoBehaviour<AudioHandler>
{
    private AudioSource audioSource;
    
    public float Volume;
    public List<AudioData> AudioList;
    private Dictionary<AudioType, AudioClip> audioDataDict;

    private SimpleTimer timer;

    protected override void Awake()
    {
        base.Awake();
        
        audioSource = GetComponent<AudioSource>();
        audioDataDict = new Dictionary<AudioType, AudioClip>();
        
        foreach(var data in AudioList) audioDataDict.Add(data.AudioType, data.Clip);
    }



    public void SpawnAudio(AudioType type, bool untilPlayOver = false, bool stopLast = false)
    {
        if(!audioDataDict.ContainsKey(type)) return;
        if(!audioDataDict[type]) return;

        if(untilPlayOver && audioSource.isPlaying) return;
        
        if(stopLast && audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioDataDict[type]);
    }


    public void SetInterval(float delayTime)
    {
        timer = new SimpleTimer(delayTime);
        timer.Pause();
    }
    
    
    /// <summary>
    /// Need set interval time first.
    /// </summary>
    /// <param name="type"></param>
    public void SpawnAudioWithInterval(AudioType type)
    {
        if(timer.IsPause) timer.Resume();
        
        if (timer.IsFinish)
        {
            SpawnAudio(type);
            timer.Reset();
        }
    }
}
