using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector3 velocity;
    [SerializeField] int spin; // maybe idk?
    [SerializeField] bool doDamage;
    [SerializeField] int damageAmount;
    [SerializeField] bool destroyOnImpact;
    [SerializeField] string onlyTargetObjectsWithTag; // if this is null, then
    [SerializeField] GameObject target;
    [SerializeField] float lifeTime = 1200f; // lifetime in seconds.
    
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject projectileRoot;
    bool thrown;
    public GameObject sender;


    void Start() {
        SetupReferences();
        StartCoroutine(LifeTimeHandler());
    }

    void Update() {
        if (!thrown && sender != null) {
            //rb.AddForce(sender.transform.forward * 400f);
            rb.velocity = new Vector3(velocity.x * sender.transform.localScale.x, velocity.y, velocity.z);
            thrown = true;
        }
    }

    void FixedUpdate() {
        rb.rotation += spin;
        Debug.Log(sender.transform.forward);
        
        //projectileRoot.transform.Translate(new Vector3(sender.transform.localScale.x, 0, 0) * 0.4f);
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
        }

        if (destroyOnImpact) {
            Destroy(projectileRoot);
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
}
