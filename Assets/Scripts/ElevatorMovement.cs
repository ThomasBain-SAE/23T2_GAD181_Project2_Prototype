using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{

    

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Open");
        // elevator doors open 
        
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Close");
        // elevator doors close

    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Move to point A or B");
        // elevator moves up / down

    }

}
