using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    AudioSource EffectSound;

    public AudioClip gamebgm;
    public AudioClip pawnmove;
    public AudioClip pawnselect;
    public AudioClip spawnwall;
    public AudioClip endgame;
    public AudioClip buttonselect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        EffectSound = GetComponent<AudioSource>();
        EffectSound.PlayOneShot(gamebgm);
        EffectSound.loop = true;
        EffectSound.volume = 0.15f;
    }
  
    public void PawnMoveEffect()
    {
        EffectSound.PlayOneShot(pawnmove);
    }
    public void PawnSelectEffect()
    {
        EffectSound.PlayOneShot(pawnselect);
    }

    public void SpawnWallEffect()
    {
        EffectSound.PlayOneShot(spawnwall);
    }
    public void EndGameEffect()
    {
        EffectSound.Stop();
        EffectSound.volume = 0.5f;
        EffectSound.PlayOneShot(endgame);
    }

    public void ButtonSelectEffect()
    {
        EffectSound.PlayOneShot(buttonselect);
    }

}


