using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Instances;

    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("AudioManager Instance not found.");

            return _instance;
        }

    }


    #endregion

    private void OnEnable()
    {
        _instance = this;
    }

    public Sound[] sounds;

    public Sound currentBgm;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

       
    }

    private void Start()
    {
        int idx = UnityEngine.Random.Range(0, 3);
        switch (idx)
        {
            case 0:
                Play("Bgm1");
                break;
            case 1:
                Play("Bgm2");
                break;
            case 2:
                Play("Bgm3");
                break;
        }
    }

    public void UpdateBgm()
    {
        if (!currentBgm.source.isPlaying)
        {
            string bgmName = currentBgm.name;
            switch (bgmName)
            {
                case "Bgm1":
                    Play("Bgm2");
                    break;
                
                case "Bgm2":
                    Play("Bgm3");
                    break;
                
                case "Bgm3":
                    Play("Bgm1");
                    break;
            }
        }
    }
    private void Update()
    {
        UpdateBgm();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if(name == "Bounce")
        {
            //s.source.pitch = GameManager.Instance.ball.countRebond * 0.045f + 0.09f;
        }

        if(name == "Bgm1" || name == "Bgm2" || name== "Bgm3")
        {
            currentBgm = s;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

}
