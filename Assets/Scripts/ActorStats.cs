using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is mainly intended for the fridge and the drone.
public class ActorStats : MonoBehaviour, IKillable
{
    [SerializeField] PlayerExpressions pExp;
    void IKillable.Die()
    {
        throw new System.NotImplementedException();
    }

    void IKillable.NotifyDamage()
    {
        HitBuffer();
    }

    void HitBuffer() {
        if (pExp != null) {
            StartCoroutine(pExp.PlayAnim("exp_x_xfaceB", true, 0.5f));
        }

    }
   
}
