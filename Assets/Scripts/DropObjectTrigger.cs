using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectTrigger : TriggerBase
{
    [SerializeField] GrabberObject gObj;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip releaseNoise;
    public override void DoAction() {
        if (gObj == null) {
            gObj = FindObjectOfType<GrabberObject>();
        }
        if (gObj.isReleaseReady) {
            gObj.Release();
        }
    }
}
