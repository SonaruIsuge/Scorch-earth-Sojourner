using System.Collections;
using System.Collections.Generic;
using SonaruUtilities;
using UnityEngine;

public class AudioHandler : TSingletonMonoBehaviour<AudioHandler>
{
    private AudioManager audioManager;
    
    public List<AudioData> AudioList;
    private Dictionary<AudioType, AudioClip> audioDataDict;

    private SimpleTimer timer;

    protected override void Awake()
    {
        base.Awake();

        audioManager = FindObjectOfType<AudioManager>();
        
        audioDataDict = new Dictionary<AudioType, AudioClip>();
        foreach(var data in AudioList) audioDataDict.Add(data.AudioType, data.Clip);
    }



    public void SpawnAudio(AudioType type, bool untilPlayOver = false, bool stopLast = false)
    {
        if(!audioDataDict.ContainsKey(type)) return;
        if(!audioDataDict[type]) return;

        audioManager.SpawnSfx(audioDataDict[type], untilPlayOver, stopLast);
    }


    public void ChangeBgm(AudioType type, float fadeTime)
    {
        if(!audioDataDict.ContainsKey(type)) return;
        if(!audioDataDict[type]) return;

        audioManager.ChangeBGM(audioDataDict[type], fadeTime);
    }


    // public void SetInterval(float delayTime)
    // {
    //     timer = new SimpleTimer(delayTime);
    //     timer.Pause();
    // }
    //
    //
    // /// <summary>
    // /// Need set interval time first.
    // /// </summary>
    // /// <param name="type"></param>
    // public void SpawnAudioWithInterval(AudioType type)
    // {
    //     if(timer.IsPause) timer.Resume();
    //     
    //     if (timer.IsFinish)
    //     {
    //         SpawnAudio(type);
    //         timer.Reset();
    //     }
    // }
}
