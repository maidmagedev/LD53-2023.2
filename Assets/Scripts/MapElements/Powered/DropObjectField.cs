using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectField : TriggerBase, PowerableElement
{
    [Header("References")]
    [SerializeField] GrabberObject gObj;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;
    private PowerSource pSource;


    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip releaseNoise;
    [SerializeField] AudioClip deactivationNoise;
    [SerializeField] AudioClip activationNoise;
    [SerializeField] float volume;
    
    void Start() {
        audioSource.dopplerLevel = 0f;
    }
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

    public void ActivateField() {
        audioSource.clip = activationNoise;
        audioSource.volume = volume;
        audioSource.Play();
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

    public void DeactivateField() {
        audioSource.clip = deactivationNoise;
        audioSource.volume = volume;
        audioSource.Play();
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
    }

    void PowerableElement.StartPowered()
    {
        DeactivateField();
    }

    void PowerableElement.EndPowered() {
        ActivateField();
    }

    void PowerableElement.SetPowerSource(PowerSource powerSource) {
        pSource = powerSource;
    }
}
