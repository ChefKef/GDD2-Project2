﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    //Longer Clips
    [SerializeField] private AudioClip currentBGM;
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip levelBGM;
    [SerializeField] private AudioClip firingBGM;

    //Shorter Clips
    //Blocks
    [SerializeField] private AudioClip blockBreak1;
    [SerializeField] private AudioClip blockBreak2;
    [SerializeField] private AudioClip blockHit;
    //Buttons
    [SerializeField] private AudioClip cancel;
    [SerializeField] private AudioClip confirm;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip select;
    //Game Screens
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip loss;
    [SerializeField] private AudioClip pause;

    private AudioSource bgm;
    private AudioSource effects;

    // Start is called before the first frame update
    void Start()
    {
        bgm = transform.Find("BGM").GetComponent<AudioSource>();
        effects = transform.Find("Effects").GetComponent<AudioSource>();

        if (bgm != null) {
            if (currentBGM == levelBGM || currentBGM == firingBGM)
                bgm.volume = 0.5f;
            else
                bgm.volume = 0.9f;
        }

        playCurrentBGM();
    }

    //------------------BGM------------------
    //Sets current BGM to title screen music
    public void setTitleBGM()
    {
        if (bgm != null)
        {
            currentBGM = titleBGM;
            bgm.volume = 0.9f;
        }
        
    }
    //Sets current BGM to level music
    public void setLevelBGM()
    {
        if (bgm != null)
        {
            currentBGM = levelBGM;
            bgm.volume = 0.5f;
        }
    }
    //Sets current BGM to firing music
    public void setFiringBGM()
    {
        if (bgm != null)
        {
            currentBGM = firingBGM;
            bgm.volume = 0.5f;
        }
    }
    public void playCurrentBGM()
    {
        if (bgm != null)
        {
            bgm.clip = currentBGM;
            bgm.loop = true;
            bgm.Play();
        }
    }
    //Stops any background music
    public void stopBGM()
    {
        if (bgm != null)
        {
            bgm.Stop();
        }
    }

    //------------------Effects------------------
    //------------Menu------------
    //Plays cancel sound
    public void playCancelClip()
    {
        if (effects != null)
        {
            effects.clip = cancel;
            effects.Play();
        }
    }
    //Plays confirm sound
    public void playConfirmClip()
    {
        if (effects != null)
        {
            effects.clip = confirm;
            effects.Play();
        }
    }
    //Plays jump sound
    public void playJumpClip()
    {
        if (effects != null)
        {
            effects.clip = jump;
            effects.Play();
        }
    }
    //Plays confirm sound
    public void playSelectClip()
    {
        if (effects != null)
        {
            effects.clip = select;
            effects.Play();
        }
    }
    //------------Blocks------------
    //Plays first block breaking sound
    public void playBlockBreak1Clip()
    {
        if (effects != null)
        {
            effects.clip = blockBreak1;
            effects.Play();
        }
    }
    //Plays second block breaking sound
    public void playBlockBreak2Clip()
    {
        if (effects != null)
        {
            effects.clip = blockBreak2;
            effects.Play();
        }
    }
    //Plays  block hitting sound
    public void playBlockHitClip()
    {
        if (effects != null)
        {
            effects.clip = blockHit;
            effects.Play();
        }
    }

    //------------Game States------------
    //Plays win sound
    public void playWinClip()
    {
        if (effects != null)
        {
            effects.clip = win;
            effects.Play();
        }
    }
    //Plays loss sound
    public void playLossClip()
    {
        if (effects != null)
        {
            effects.clip = loss;
            effects.Play();
        }
    }
    //Plays win sound
    public void playPauseClip()
    {
        if (effects != null)
        {
            effects.clip = pause;
            effects.Play();
        }
    }
    //Stops any effects from playing
    public void stopEffects()
    {
        effects.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
