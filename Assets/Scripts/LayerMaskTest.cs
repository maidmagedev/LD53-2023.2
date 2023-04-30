using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskTest : MonoBehaviour
{
    LayerMask layer;
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter!");
        if (other.CompareTag("Player")) {
            Debug.Log("is player");
        }
    }
}
