using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Player player;

    private void Update()
    {
        if(Physics.CheckSphere(transform.position, radius, layerMask) && player.isHidden == false)
        {
            Guard.alertedLocation = player.gameObject.transform.position;
            Guard.canSeePlayer = true;
            Debug.Log("Player Seen");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}