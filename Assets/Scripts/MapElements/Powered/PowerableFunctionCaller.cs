using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// When powered, we will look for a function using a string under a component, using a string.
public class PowerableFunctionCaller : MonoBehaviour, PowerableElement
{
    [SerializeField] GameObject scriptHolder;
    [SerializeField] string functionName;
    [SerializeField] string componentName;
    [SerializeField] PowerSource powerSource;

    void PowerableElement.EndPowered()
    {
    }

    void PowerableElement.SetPowerSource(PowerSource pSource)
    {
        powerSource = pSource;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PowerableElement.StartPowered()
    {
        CallSomeFunction();
    }

    void CallSomeFunction() {
        Component someComponent = scriptHolder.GetComponent(componentName);
        MethodInfo functionInfo = someComponent?.GetType().GetMethod(functionName);
        functionInfo?.Invoke(someComponent, null);
        Debug.Log("component: " + someComponent);
        Debug.Log("MethodInfo: " + functionInfo);
        
    }
}
