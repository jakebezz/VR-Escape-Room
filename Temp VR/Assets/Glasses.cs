using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform moveToLocation;

    private Rigidbody glassesRigid;

    private string glassesTrigger = "GlassesTrigger";

    private void Start()
    {
        glassesRigid = GetComponent<Rigidbody>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(glassesTrigger))
        {
            transform.position = moveToLocation.position;
            glassesRigid.isKinematic = true;

            player.EnableHidden();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(glassesTrigger))
        {
            glassesRigid.isKinematic = false;
            player.DisableHidden();
        }
    }
}
