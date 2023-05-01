using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedButton : PowerSource
{
    public GameObject weight;


    void Start () {
        SetupReferences();
    }

    void OnCollisionEnter2D(Collision2D collision) {


        weight = collision.gameObject;        

        StopAllCoroutines();
        StartCoroutine(MovePlatform(transform.localPosition, new Vector3(0, -0.25f, 0)));
        if (!isPowered) {
            isPowered = true;

            foreach (PowerableElement pElem in powerableElements) {
                pElem.StartPowered();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        StopAllCoroutines();
        StartCoroutine(Tether());
    }



    IEnumerator MovePlatform(Vector3 startPos, Vector3 endPos) {
        
        float duration = 0.5f;


        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            float t = (Time.time - startTime) / duration;
            this.transform.localPosition = Vector3.Slerp(startPos, endPos, t);
            yield return null;
        }

        this.transform.localPosition = endPos;

        yield return null;
    }

    // Our weighted object has left the button. 
    // If it goes to far away, remove it.
    // If it has been away for too long, also remove it.
    IEnumerator Tether() {
        float duration = 0.5f;
        float startTime = Time.time;
        float endTime = startTime + duration;

        float maxDistance = 1f;

        while (weight != null) {
            float t = (Time.time - startTime) / duration;

            if (Vector3.Distance(weight.transform.position, this.transform.position) > maxDistance) {
                weight = null;
                isPowered = false;
                //StopCoroutine("MovePlatform");
                StartCoroutine(MovePlatform(transform.localPosition, Vector3.zero));

                foreach (PowerableElement pElem in powerableElements) {
                    pElem.EndPowered();
                }

                yield break;
            }
            yield return null;
        }


    }
}
