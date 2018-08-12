using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDozer : MonoBehaviour
{

    public AudioSource bullDozerAudio;

    void Start()
    {
        bullDozerAudio = GetComponent<AudioSource>();
        PlayBullDozerAudio();
    }

    public void PlayBullDozerAudio()
    {
        print("soi audio");
        bullDozerAudio.Play();
    }

}
