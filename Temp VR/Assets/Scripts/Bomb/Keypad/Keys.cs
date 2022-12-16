using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : KeysParent
{
    //Sets delegate to this key
    private void Start()
    {
        pressKey = InputKey;
    }
}
