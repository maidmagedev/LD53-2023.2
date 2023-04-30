using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector3 velocity;
    [SerializeField] int spin; // spin amount per frame.
    [SerializeField] bool doDamage;
    [SerializeField] int damageAmount;
    [SerializeField] bool destroyOnImpact;
    [SerializeField] string onlyTargetObjectsWithTag; // if this is null, then
    [SerializeField] GameObject target;
    [SerializeField] float lifeTime = 20f; // lifetime in seconds.
    
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject projectileRoot;
    bool thrown;
    public GameObject sender;


    void Start() {
        SetupReferences();
        StartCoroutine(LifeTimeHandler());
        StartCoroutine(ShrinkSpawn());
    }

    void FixedUpdate() {
        rb.rotation += spin;
        if (!thrown && sender != null) {
            //rb.AddForce(sender.transform.forward * 400f);
            rb.velocity = new Vector3(velocity.x * sender.transform.localScale.x, velocity.y, velocity.z);
            thrown = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // If this string is not null, check if this object is NOT the collider we want.
        if (onlyTargetObjectsWithTag.CompareTo("") != 0) { 
            // Since this is not the collider we want, return out of this function.
            if (!other.CompareTag(onlyTargetObjectsWithTag)) {
                return;
            }
        }
       
        if (doDamage) {
            // call external damage component methods...
            DamageableComponent dmgComp = other.GetComponent<DamageableComponent>();
            if (dmgComp != null) {
                Debug.Log("Test");
                dmgComp.TakeDamage(damageAmount);
            }
        }

        if (destroyOnImpact) {
            StopAllCoroutines();
            StartCoroutine(ShrinkDeath());
            //Destroy(projectileRoot);
        }
    }

    void SetupReferences() {
        if (rb == null) {
            rb = this.gameObject.GetComponentInParent<Rigidbody2D>();
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
