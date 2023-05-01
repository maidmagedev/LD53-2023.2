using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;
    }
}
