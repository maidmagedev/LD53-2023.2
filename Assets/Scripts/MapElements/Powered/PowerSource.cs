using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerableElement {
    public void StartPowered();
    public void EndPowered();
    public void SetPowerSource(PowerSource pSource);
}

public class PowerSource : MonoBehaviour
{
    [Header("PowerSource Variables")]
    public bool isPowered;
    public List<GameObject> powerableElementSources; // Sometimes the powerableElements are missings, so we have to grab them from their parents.
    public List<PowerableElement> powerableElements;
    
    public void SetupReferences() {
        if (powerableElementSources == null) {
            powerableElementSources = new List<GameObject>();
        }
        if (powerableElements == null) {
            powerableElements = new List<PowerableElement>();
        }

        foreach (GameObject obj in powerableElementSources) {
            powerableElements.Add(obj.GetComponentInChildren<PowerableElement>());
        }
        foreach (PowerableElement pElem in powerableElements) {
            pElem.SetPowerSource(this);
        }
    }
}
