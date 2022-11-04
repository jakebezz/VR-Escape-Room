using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : ColourChangingPuzzle
{
    //Light One
    void Update()
    {
        ChangeOnOff(gameObject, false, lightTwo, true, true);
    }
}
