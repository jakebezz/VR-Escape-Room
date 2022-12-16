using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    /// <summary>
    /// Range of visiblity
    /// </summary>
    public float minValue;
    public float maxValue;

    /// <summary>
    /// //Reveals hidden objects when the hue shift reaches a certain threshold
    /// </summary>
    /// <param name="hueShiftValue"></param>
    /// <returns></returns>
    public bool RevealHiddenObject(float hueShiftValue)
    {
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
