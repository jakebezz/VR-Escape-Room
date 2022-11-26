using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : KeysParent
{
    private void Start()
    {
        //Sets delegate to this key
        pressKey = InputKey;
    }
}
