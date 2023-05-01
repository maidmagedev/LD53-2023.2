using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{


    [Header("Settings")]
    [SerializeField] float moveSpeed = 0.4f;
    [SerializeField] int damageAmount;
    [SerializeField] bool destroyOnContact;
    [SerializeField] bool destroyOnGround;
    [SerializeField] string onlyTargetObjectsWithTag; // if this is null, then
    [SerializeField] float lifeTime = 20f; // lifetime in seconds.
    
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject projectileRoot;
    bool thrown;
    public GameObject sender;


    void Start() {
        StartCoroutine(LifeTimeHandler());
        StartCoroutine(ShrinkSpawn());
    }

    void FixedUpdate() {
        // this below bit happens once even though it's in update.
        Move();
    }

    void Move() {
        transform.Translate(Vector2.right * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (destroyOnGround) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                StopAllCoroutines();
                StartCoroutine(ShrinkDeath());
            }
        }

        // If this string is not null, check if this object is NOT the collider we want.
        if (onlyTargetObjectsWithTag.CompareTo("") != 0) { 
            // Since this is not the collider we want, return out of this function.
            if (!other.CompareTag(onlyTargetObjectsWithTag)) {
                //return;
            }
        }
       
        // call external damage component methods...
        DamageableComponent dmgComp = other.GetComponent<DamageableComponent>();
        if (dmgComp != null) {
            dmgComp.TakeDamage(damageAmount);
        }
        

        if (destroyOnContact) {
            StopAllCoroutines();
            //StartCoroutine(ShrinkDeath());
        }
    }

    IEnumerator LifeTimeHandler() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(projectileRoot);
    }

    // courtesy of GPT
    IEnumerator ShrinkDeath() {
    
        float duration = 0.5f;
        Vector3 endScale = Vector3.zero;

        Vector3 startScale = projectileRoot.transform.localScale;

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            float t = (Time.time - startTime) / duration;
            projectileRoot.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        projectileRoot.transform.localScale = endScale;

        yield return null;
        Destroy(projectileRoot);
    }

    // courtesy of GPT
    IEnumerator ShrinkSpawn() {
    
        float duration = 0.3f;
        Vector3 endScale = projectileRoot.transform.localScale;

        Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);

        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            float t = (Time.time - startTime) / duration;
            projectileRoot.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        projectileRoot.transform.localScale = endScale;

        yield return null;
    }
}

