using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarDetection : MonoBehaviour
{
    Rigidbody crowbar;
    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    [SerializeField] float crowbarVelocity;

    void Start()
    {
        crowbar = GetComponent<Rigidbody>();
        crowbar.isKinematic = true;
    }

    void Update()
    {
        crowbarVelocity = crowbar.velocity.magnitude;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            crowbar.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor" && crowbarVelocity > 2)
        {
            Debug.Log("Guards Alerted");
            GameManager.alertedGuards = true;
        }
    }
}
