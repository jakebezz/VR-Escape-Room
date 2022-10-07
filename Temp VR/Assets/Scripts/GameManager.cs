using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //PROABABLY CHANGE THIS TO SINGLETON CLASS
    //Maybe move these to guard class? will have to see
    public static bool alertedGuards;
    public static Transform alertedLocation;

    private void Start()
    {
        alertedGuards = false;
    }

}
