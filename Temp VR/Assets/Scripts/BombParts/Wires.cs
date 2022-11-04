using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : BombParts
{
    private void Start()
    {
        bombPartRigid = gameObject.GetComponent<Rigidbody>();
    }
}
