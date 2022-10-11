using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //REWATCH VIDEO AND COMMENT CODE, CHANGE IT TOO

    public float radius;
    [Range(0, 360)] public float angle;

    public GameObject playerObj;

    //Layer assigned to the Player, this layer will be spotted by the guard
    [SerializeField] private LayerMask playerMask;
    //Layer assigned to most objects, guard will no be able to see behind this mask
    [SerializeField] private LayerMask obstructionMask;

    //Rotating guard variables
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;
    private Quaternion lookRotation;
    private Vector3 rotationDirection;

    //Access to Guard Class
    private Guard guard;
    [SerializeField] private GameObject guardObj;

    private void Start()
    {
        StartCoroutine(FOVRoutine());

        guard = guardObj.GetComponent<Guard>();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    GameManager.Instance.canSeePlayer = true;

                    //Rotates guard to look towards player
                    rotationDirection = (target.position - transform.position).normalized;
                    lookRotation = Quaternion.LookRotation(rotationDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                    GameManager.Instance.alertedGuards = true;
                    GameManager.Instance.alertedLocation = playerObj.transform.position;

                    Debug.Log("Going to player");
                }
                else
                {
                    GameManager.Instance.canSeePlayer = false;
                }
            }
            else
            {
                GameManager.Instance.canSeePlayer = false;
            }
        }
        else if (GameManager.Instance.canSeePlayer)
        {
            GameManager.Instance.canSeePlayer = false;
        }
    }
}