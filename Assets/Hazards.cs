using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        DamageableComponent dmgComp = col.gameObject.GetComponent<DamageableComponent>();
        if (dmgComp != null) {
            dmgComp.TakeDamage(1000);
        }
    }
}
