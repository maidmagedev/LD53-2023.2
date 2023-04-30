using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerableObjectSpawner : MonoBehaviour, PowerableElement
{
    [Header("Settings")]
    
    [SerializeField] GameObject pfSpawnObject;
    [SerializeField] PowerSource powerSource;
    [SerializeField] bool onlyActivateOnce; // Prevents future activations.
    [SerializeField] bool destroyOldInstantiated; // Destroys the old instantiated object on activation.
    [SerializeField] float cooldown;

    [Header("References")]
    [SerializeField] GameObject instantiatedObject; // Set during runtime.
    [SerializeField] Animator animator;
    [SerializeField] Transform spawnOrigin;
    Rigidbody2D instObjRB;
    private bool activatedOnce;
    [SerializeField] bool waitingForCooldown = false;

    void PowerableElement.EndPowered()
    {
        // Do Nothing.
    }

    void PowerableElement.SetPowerSource(PowerSource pSource)
    {


        powerSource = pSource;
    }


    void PowerableElement.StartPowered()
    {
        Debug.Log("Hello!");
        if (waitingForCooldown) {
            return;
        }

        if (onlyActivateOnce && activatedOnce) {
            return;
        }
        if (instantiatedObject != null) {
            GameObject.Destroy(instantiatedObject);
        }
        instantiatedObject = Instantiate(pfSpawnObject, Vector3.zero, Quaternion.identity, spawnOrigin);
        //instantiatedObject.transform.SetParent(spawnOrigin);
        instantiatedObject.transform.localPosition = Vector3.zero;
        instObjRB = instantiatedObject.GetComponentInChildren<Rigidbody2D>();
        if (instObjRB != null) {
            instObjRB.simulated = false;
        }
        animator.SetTrigger("SpawnObj");
        activatedOnce = true;
        StartCoroutine(CooldownManager());
    }

 
    IEnumerator CooldownManager() {
        waitingForCooldown = true;
        yield return new WaitForSeconds(cooldown);
        waitingForCooldown = false;
    } 

    // Animation Event script. Doesnt show up references.
    public void DropObj() {
        instObjRB.simulated = true;
        instantiatedObject.transform.SetParent(null);
    }
}
