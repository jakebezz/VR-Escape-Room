using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                gameObject.AddComponent<GameManager>();
            }
            else
            {
                return _instance;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public bool canSeePlayer;
    public bool alertedGuards;
    public Vector3 alertedLocation;

    private void Start()
    {
        alertedGuards = false;
        canSeePlayer = false;
    }
}
