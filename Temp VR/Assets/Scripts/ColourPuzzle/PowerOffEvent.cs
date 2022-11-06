using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOffEvent : MonoBehaviour
{
    public void TurnLightsOff()
    {
        Debug.Log("Lights In Scene Powered off");
    }

    public void TurnOffPowerBoxFX()
    {
        Debug.Log("Power Box FX Turned Off");
    }

    public void PowerBoxCanOpen()
    {
        Debug.Log("Power Box Can Be Opened");
    }

    public void GuardSaysSomething()
    {
        Debug.Log("Guard Makes Comment About Power");
    }


}
