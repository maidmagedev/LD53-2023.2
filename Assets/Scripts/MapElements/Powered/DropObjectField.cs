using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectField : TriggerBase, PowerableElement
{
    [Header("Settings")]
    [SerializeField] bool blocksPlayer;
    [SerializeField] Behavior behavior;
    [SerializeField] bool swapBehaviorOnPowered;

    [Header("References")]
    [SerializeField] GrabberObject gObj;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] BoxCollider2D selectiveCollider;
    [SerializeField] ActiveCharacterManager activeCharacterManager;
    private PowerSource pSource;


    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip releaseNoise;
    [SerializeField] AudioClip deactivationNoise;
    [SerializeField] AudioClip activationNoise;
    [SerializeField] float volume;
    
    public enum Behavior {
        droneOnly,
        blockDrone
    }

    void Start() {
        audioSource.dopplerLevel = 0f;
        SetupBlocker();
    }

    void Update() {
        
    }

    public override void DoAction() {
        if (behavior == Behavior.droneOnly) {
            if (gObj == null) {
                gObj = FindObjectOfType<GrabberObject>();
            }
            if (gObj.isReleaseReady) {
                audioSource.clip = releaseNoise;
                audioSource.volume = volume;
                audioSource.Play();
                gObj.Release();
            }
        } else if (behavior == Behavior.blockDrone) {
            audioSource.clip = releaseNoise;
            audioSource.volume = volume;
            audioSource.Play();
            Rigidbody2D droneRB = activeCharacterManager.drone.GetComponent<Rigidbody2D>();
            droneRB.AddForce(-droneRB.velocity.normalized * 30f, ForceMode2D.Impulse);
        }
    }

    public void ActivateField() {
        audioSource.clip = activationNoise;
        audioSource.volume = volume;
        audioSource.Play();
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        selectiveCollider.enabled = true;
    }

    public void DeactivateField() {
        audioSource.clip = deactivationNoise;
        audioSource.volume = volume;
        audioSource.Play();
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        selectiveCollider.enabled = false;
    }

    void PowerableElement.StartPowered()
    {
        if (swapBehaviorOnPowered) {
            FlipBehavior();
        } else {
            DeactivateField();
        }
        
    }

    void PowerableElement.EndPowered() {
        if (swapBehaviorOnPowered) {
            FlipBehavior();
        } else {
            ActivateField();
        }
    }

    void PowerableElement.SetPowerSource(PowerSource powerSource) {
        pSource = powerSource;
    }

    void SetupBlocker() {
        if (activeCharacterManager == null) {
            activeCharacterManager = FindObjectOfType<ActiveCharacterManager>();
        }
        if (behavior == Behavior.droneOnly) {
            selectiveCollider.enabled = true;
            Physics2D.IgnoreCollision(activeCharacterManager.drone.GetComponent<BoxCollider2D>(), selectiveCollider, true);
            Physics2D.IgnoreCollision(activeCharacterManager.fridge.GetComponent<BoxCollider2D>(), selectiveCollider, false);
        } else if (behavior == Behavior.blockDrone) {
            selectiveCollider.enabled = false;
        }   
    }

    void FlipBehavior() {
        if (behavior == Behavior.droneOnly) {
            behavior = Behavior.blockDrone;
            spriteRenderer.color = new Color32(0xFF, 0xA0, 0x00, 0x77);

        } else if (behavior == Behavior.blockDrone) {
            behavior = Behavior.droneOnly;
            spriteRenderer.color = new Color32(0x00, 0xDD, 0xFF, 0x77);
        }
        audioSource.clip = activationNoise;
        audioSource.volume = volume;
        audioSource.Play();
        SetupBlocker();
    }

    
}
