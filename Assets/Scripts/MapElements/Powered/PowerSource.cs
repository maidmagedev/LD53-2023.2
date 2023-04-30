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

    public List<PowerableElement> powerableElements;

}
