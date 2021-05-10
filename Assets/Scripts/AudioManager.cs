using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource backmusic;
    public static AudioManager Instance {get; set;}

    void Start()
    {
        Instance = this;
        backmusic = GetComponent<AudioSource>();
    }

    void SelectEffect()
    {

    }

    void DecideEffect()
    {

    }

    void PauseEffect()
    {

    }


    
}
