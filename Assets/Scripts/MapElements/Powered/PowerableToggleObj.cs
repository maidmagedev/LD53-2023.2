using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerableToggleObj : MonoBehaviour, PowerableElement
{
    [SerializeField] GameObject obj;
    [SerializeField] bool stateWhenPowered;
    private PowerSource pSource;
    
    void PowerableElement.StartPowered()
    {
        obj.SetActive(stateWhenPowered);
    }

    void PowerableElement.EndPowered() {
        obj.SetActive(!stateWhenPowered);
    }

    void PowerableElement.SetPowerSource(PowerSource powerSource) {
        pSource = powerSource;
    }

}
