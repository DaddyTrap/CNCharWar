using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public GameObject AudioSourcesGObj;
    public GameObject AudioSourcePrefab;

    private GameObject BGMSource;
    private AudioSource BGMAudioSource;
    private string currentBGM = null;

    private List<AudioSource> SEAudioSourceList;

    private Dictionary<string, AudioClip> BGMDict, SEDict;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        BGMDict = new Dictionary<string, AudioClip>();
        SEDict = new Dictionary<string, AudioClip>();

        ImportResources();

        BGMSource = Instantiate(AudioSourcePrefab, AudioSourcesGObj.transform);
        BGMAudioSource = BGMSource.GetComponent<AudioSource>();
        BGMAudioSource.loop = true;
        //BGMAudioSource.Stop();
        SEAudioSourceList = new List<AudioSource>();
    }

    void ImportResources()
    {
        Object[] bgms = Resources.LoadAll("Music/BGM", typeof(AudioClip));
        Object[] ses = Resources.LoadAll("Music/SE", typeof(AudioClip));

        foreach (var clip in bgms)
        {
            BGMDict.Add(clip.name, clip as AudioClip);
        }

        foreach (var clip in ses)
        {
            SEDict.Add(clip.name, clip as AudioClip);
        }
    }

    public void PlayBGM(string name)
    {
        if (currentBGM != name)
        {
            currentBGM = name;
            BGMAudioSource.clip = BGMDict[name];
            BGMAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        Debug.Log("音乐停止");
        Debug.Log(BGMAudioSource.isPlaying);
        BGMAudioSource.Stop();
        Debug.Log(BGMAudioSource.isPlaying);
    }

    public AudioSource PlaySE(string name)
    {
        AudioSource audioSource = null;
        bool found = false;

        // Find in pool
        foreach (var source in SEAudioSourceList)
        {
            if (source.isPlaying == false)
            {
                audioSource = source;
                found = true;
            }
        }

        // Not found, make one and add it to pool
        if (!found)
        {
            var SESource = Instantiate(AudioSourcePrefab, AudioSourcesGObj.transform) as GameObject;
            audioSource = SESource.GetComponent<AudioSource>();
            SEAudioSourceList.Add(audioSource);
        }

        audioSource.clip = SEDict[name];
        audioSource.Play();
        return audioSource;
    }

    public void ChangeBGMVolume(float number)
    {
        BGMAudioSource.volume = number / 100f;
    }

    public void ChangeSEVolume(float number)
    {
        float res = number / 100f;
        AudioSourcePrefab.GetComponent<AudioSource>().volume = res;
        foreach (var source in SEAudioSourceList)
        {
            source.volume = res;
        }
    }
}