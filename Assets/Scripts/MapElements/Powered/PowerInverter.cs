using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This child will call the EndPowered to its children when it recieves StartPowered().
public class PowerInverter : PowerSource, PowerableElement
{
    [SerializeField] PowerSource powerSource;

    void Start() {
        SetupReferences();
    }


    void PowerableElement.EndPowered()
    {
        foreach (PowerableElement pElem in powerableElements) {
            pElem.StartPowered();
        }
    }

    void PowerableElement.SetPowerSource(PowerSource pSource)
    {
        powerSource = pSource;
    }

    
    void PowerableElement.StartPowered()
    {
        foreach (PowerableElement pElem in powerableElements) {
            pElem.EndPowered();
        }
    }
}

