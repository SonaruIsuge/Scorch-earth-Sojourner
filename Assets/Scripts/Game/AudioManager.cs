using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    
    
    public void SpawnSfx(AudioClip audioClip, bool untilPlayOver = false, bool stopLast = false)
    {
        if(untilPlayOver && sfxAudioSource.isPlaying) return;
        
        if(stopLast && sfxAudioSource.isPlaying) sfxAudioSource.Stop();
        sfxAudioSource.PlayOneShot(audioClip);
    }


    public async void ChangeBGM(AudioClip newBgm, float fadeTime)
    {
        var timer = 0f;
        audioMixer.GetFloat("BgmVolume", out var currentVolume);
        var originVolume = currentVolume;
        
        currentVolume = Mathf.Pow(10, currentVolume / 20);
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            var newVol = Mathf.Lerp(currentVolume, 0.001f, timer / fadeTime);
            audioMixer.SetFloat("BgmVolume", Mathf.Log10(newVol) * 20);
            await Task.Yield();
        }

        bgmAudioSource.clip = newBgm;
        bgmAudioSource.Play();
        
        timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            var newVol = Mathf.Lerp(currentVolume, originVolume, timer / fadeTime);
            audioMixer.SetFloat("BgmVolume", Mathf.Log10(newVol) * 20);
            await Task.Yield();
        }
    }
}
