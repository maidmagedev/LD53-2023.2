using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerableMonitor : MonoBehaviour, PowerableElement
{
    [SerializeField] Light2D light2D;
    [SerializeField] PowerSource powerSource;

    void PowerableElement.SetPowerSource(PowerSource pSource)
    {
        powerSource = pSource;
    }

    void PowerableElement.EndPowered()
    {
        Debug.Log("end");
        light2D.intensity = 0;
    }


    void PowerableElement.StartPowered()
    {
        Debug.Log("Start");
        light2D.intensity = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
