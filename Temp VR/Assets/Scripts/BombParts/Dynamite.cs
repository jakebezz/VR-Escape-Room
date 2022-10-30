using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : BombPartsPlacement
{
    private void Start()
    {
        bombPartRigid = GetComponent<Rigidbody>();
    }
}
