using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : TriggerBase
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] int activeClipIndex;
    [SerializeField] int volume;
    [SerializeField] bool activateOnce;
    private bool activatedBefore = false;
    

    public override void DoAction() {
        if (activateOnce && activatedBefore) {
            return;
        }
        activatedBefore = true;
        audioSource.clip = audioClips[activeClipIndex];
        audioSource.volume = volume;
        audioSource.Play();
    }
}
