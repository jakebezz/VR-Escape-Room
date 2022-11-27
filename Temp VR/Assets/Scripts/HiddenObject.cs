using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    public float minValue;
    public float maxValue;

    public bool RevealHiddenObject(float hueShiftValue)
    {
        //Reveals hidden objects when the hue shift reaches a certain threshold
        if (hueShiftValue > minValue && hueShiftValue < maxValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
