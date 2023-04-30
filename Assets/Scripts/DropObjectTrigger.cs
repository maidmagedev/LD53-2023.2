using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectTrigger : TriggerBase
{
    [SerializeField] GrabberObject gObj;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip releaseNoise;
    [SerializeField] float volume;
    
    public override void DoAction() {
        if (gObj == null) {
            gObj = FindObjectOfType<GrabberObject>();
        }
        if (gObj.isReleaseReady) {
            audioSource.clip = releaseNoise;
            audioSource.volume = volume;
            audioSource.Play();
            gObj.Release();
        }
    }
}
