using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform moveToLocation;                      //Location to keep Glasses

    private Rigidbody glassesRigid;

    private string glassesTrigger = "GlassesTrigger";                       //Tag

    private void Start()
    {
        glassesRigid = GetComponent<Rigidbody>();   
    }

    /// <summary>
    /// Set object to moveToLocation and Enable Hidden objects
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(glassesTrigger))
        {
            transform.position = moveToLocation.position;
            glassesRigid.isKinematic = true;

            player.EnableHidden();
        }
    }

    /// <summary>
    /// Disable Hidden objects when Player takes off Glasses
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(glassesTrigger))
        {
            glassesRigid.isKinematic = false;
            player.DisableHidden();
        }
    }
}
