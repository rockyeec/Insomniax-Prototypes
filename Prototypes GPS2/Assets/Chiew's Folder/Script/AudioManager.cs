using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] BGM; //Store Every Sounds, Refer Inspector
    public Sound[] SFX;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) //For Multiple Scene Purpose
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); //if have Second of Audio Manager
            return;
        }

        DontDestroyOnLoad(gameObject); //Won't destroy through out the changing scene

        //First time set all audio assets
        foreach (Sound s in BGM)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
        foreach (Sound s in SFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        AdjustAllVolume(0.5f); //Adjust all to 0.5f;
        Play("MainMenuBGM","BGM"); //Play BGM
    }

    private void Update()
    {
        //if needed for scene update put here
    }

    public Sound FindSound(string name, string type) //function to find sound
    {
        Sound s = null;
        if (type == "BGM"){s = Array.Find(BGM, sound => sound.name == name);
            foreach (Sound so in BGM)
            {
                if(s != null)
                {
                    if(so.name != s.name)
                    { so.source.Stop(); }
                }
            }
        }
        else if(type == "SFX"){s = Array.Find(SFX, sound => sound.name == name); }
        if (s == null) //Warning Debug
        {
            Debug.LogWarning("Sounds: " + name + " not found!");
            return null;
        }
        return s;
    }

    public bool FindIsPlaying(string name, string type) //a bool determine is playing or not
    {
        Sound temp = FindSound(name, type);
        return temp.source.isPlaying;
    }

    public void Play(string name,string type) //Play Sound source
    {
        Sound temp = FindSound(name,type);
        if(temp != null) { temp.source.Play();Debug.Log("Playing"); } else { return; }
    }

    public void Pause(string name, string type) //Pause Sound source
    {
        Sound temp = FindSound(name, type);
        if (temp != null) { temp.source.Pause(); } else { return; }
    }

    public void Stop(string name, string type) //Stop Sound source
    {
        Sound temp = FindSound(name, type);
        if (temp != null) { temp.source.Stop(); } else { return; }
    }

    public void AdjustVolume(string name, float amount, string type) //Adjust Volume one by one
    {
        Sound temp = FindSound(name, type);
        if (temp == null) //Warning Debug
        {
            Debug.LogWarning("Sounds: " + name + " not found!");
            return;
        }
        if (amount >= 1) {amount = 1;}
        temp.volume = amount;temp.source.volume = temp.volume;
    }

    public void AdjustTypeVolume(float amount,string type)
    {
        if (type == "BGM")
        {
            foreach (Sound s in BGM)
            {
                if (amount >= 1) { amount = 1; }
                else if (amount <= 0) { amount = 0; }
                s.volume = amount;
                s.source.volume = s.volume;
            }
        }
        else if(type == "SFX")
        {
            foreach (Sound s in SFX)
            {
                if (amount >= 1) { amount = 1; }
                else if (amount <= 0) { amount = 0; }
                s.volume = amount;
                s.source.volume = s.volume;
            }
        }
    }

    public void AdjustAllVolume(float amount) //Adjust All Volume include BGM and SFX
    {
        foreach (Sound s in BGM)
        {
            if (amount >= 1){amount = 1;}
            else if (amount <= 0){amount = 0;}
            s.volume = amount;
            s.source.volume = s.volume;
        }
        foreach (Sound s in SFX)
        {
            if (amount >= 1) { amount = 1; }
            else if (amount <= 0) { amount = 0; }
            s.volume = amount;
            s.source.volume = s.volume;
        }
    }
}
