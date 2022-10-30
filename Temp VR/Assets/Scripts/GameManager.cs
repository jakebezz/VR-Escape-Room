using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool canSeePlayer;
    public bool alertedGuards;
    public Vector3 alertedLocation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        alertedGuards = false;
        canSeePlayer = false;
    }

    public RaycastHit RaycastFromObject(GameObject raycastObject)
    {
        RaycastHit hit;

        if(Physics.Raycast(raycastObject.transform.position, raycastObject.transform.forward, out hit))
        {
            return hit;
        }
        return hit;
    }
}
