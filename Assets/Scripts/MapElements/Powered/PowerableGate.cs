using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerableGate : MonoBehaviour, PowerableElement
{
    
    
    [Header("Gate Settings")]
    [SerializeField] Behavior behavior;
    [SerializeField] private Vector3 endPosition;

    [Header("settings: moveTowards")]
    [SerializeField] private float movementSpeed = 2f;
    private bool movingForward = true;
    [Header("settings: lerpTo")]
    [SerializeField] float duration;

    [Header("References")]
    [SerializeField] PowerSource powerSource;
    private Vector3 startPosition;

    public enum Behavior {
        moveTowards, 
        lerpTo
    }


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (behavior == Behavior.moveTowards) {
            if (powerSource.isPowered)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * movementSpeed);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime * movementSpeed);
            }
        }
    }

    public void StartPowered()
    {
        if (behavior == Behavior.moveTowards) {
            if (transform.position == startPosition)
            {
                movingForward = true;
            }
            else if (transform.position == endPosition)
            {
                movingForward = false;
            }
        } else {
            StopAllCoroutines();
            StartCoroutine(LerpToPos());
        }
        
        
    }

    public void EndPowered()
    {
        //throw new System.NotImplementedException();
    }

    public void SetPowerSource(PowerSource pSource)
    {
        powerSource = pSource;
    }

    IEnumerator LerpToPos() {
        Debug.Log("Test");
        float time = 0.0f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPosition = transform.InverseTransformPoint(endPosition);
        while (time < duration) {
            float tVal = time / duration;
            tVal = tVal * tVal * (3f - 2f * tVal);
            transform.localPosition = Vector3.Lerp(startPos, endPosition, tVal);
            time += Time.deltaTime;
            yield return null;
        }
        //transform.localPosition = endPosition;

        yield return null;
    }
     
}
