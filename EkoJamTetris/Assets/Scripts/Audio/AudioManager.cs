﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach(Sound s in sounds)
        {
           s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            if(s.source.playOnAwake == true)
            {
                s.source.Play();
            } 
        }
    }

    public void Play(string name)
    {
        Sound s =Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.Log(name +" not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
		
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
