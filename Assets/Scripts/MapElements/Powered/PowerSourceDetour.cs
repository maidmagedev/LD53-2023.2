using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unsure about the smartness of this script. I made this really quickly without thinking about consequences.
public class PowerSourceDetour : PowerSource
{
    // Start is called before the first frame update
    void Start()
    {
        SetupReferences();
    }
    
    public void Activate() {
        foreach (PowerableElement pElem in powerableElements) {
            pElem.StartPowered();
        }
    }

    public void DeActivate() {
        foreach (PowerableElement pElem in powerableElements) {
            pElem.EndPowered();
        }
    }
}
