using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeSpecialCollisions : MonoBehaviour
{
    [SerializeField] PlayerExpressions pExp;
    void OnCollisionEnter2D(Collision2D collision) {


        if (collision.gameObject.tag.CompareTo("Player") == 0 && collision.gameObject.name.CompareTo("Drone") == 0 ) {
            StartCoroutine(pExp.PlayAnim("exp_manface", true, 0.5f));
        }

    }
}
