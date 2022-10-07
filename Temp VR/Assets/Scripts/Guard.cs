using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public Transform guardStartLocation;

    private Transform moveLocation;
    private NavMeshAgent guardAgent;

    Player player;
    [SerializeField] GameObject playerObject;

    private void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        player = playerObject.GetComponent<Player>();
    }

    void Update()
    {
        if (GameManager.alertedGuards == true)
        {
            guardAgent.destination = GameManager.alertedLocation.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "DetectionTrigger")
        {
            if(player.isHidden == false)
            {
                Debug.Log("Player Caught");
            }
            else
            {
                Debug.Log("Player Hidden");
            }
        }
    }
}
