using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //PROABABLY CHANGE THIS TO SINGLETON CLASS

    public static bool alertedGuards;

    private void Start()
    {
        alertedGuards = false;
    }
}
