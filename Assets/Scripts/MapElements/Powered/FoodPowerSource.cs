using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodPowerSource : PowerSource
{
    public int currentCount;
    public int maxCount;
    public Image meter;



    // Start is called before the first frame update
    void Start()
    {
        SetupReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCount >= maxCount) {
            foreach (PowerableElement pElem in powerableElements) {
                pElem.StartPowered();

            }
            isPowered = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (currentCount >= maxCount) {
            return;
        }
        if (other.CompareTag("Projectiles")) {
            currentCount++;
            StopAllCoroutines();
            StartCoroutine(FillAmountLerp());
            //meter.fillAmount = (float) currentCount / maxCount;
        }
    }

    IEnumerator FillAmountLerp() {
        float duration = 0.5f;
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime) {
            float tVal = (Time.time - startTime) / duration;
            float fillAmount = Mathf.Lerp(meter.fillAmount, (float) currentCount / maxCount, (float)currentCount / (float)maxCount);
            meter.fillAmount = Mathf.Lerp(meter.fillAmount, fillAmount, tVal);
            yield return null;
        }

        meter.fillAmount = (float) currentCount / (float) maxCount;
                    yield return null;

    }
}
