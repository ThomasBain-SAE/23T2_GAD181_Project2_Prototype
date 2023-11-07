using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField] public GameObject upButton;
    [SerializeField] public GameObject downButton;

    [SerializeField] public GameObject elevatorDoorUpper;
    [SerializeField] public GameObject elevatorDoorLower;

    [SerializeField] public GameObject elevatorPosition;

    public float elevatorSpeed = 1.0f;

    public bool elevatorOnTopFloor = true;
    public bool elevatorOnBottomFloor = true;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("open door");

        //If trigger detects player near elevator door on upper level
        if (other.CompareTag("Player"))
        {
            Vector3 newPosition = elevatorDoorUpper.transform.position;
            newPosition.x += 10f;
            elevatorDoorUpper.transform.position = newPosition;
        }

        //If trigger detects player near elevator door on lower level
        if (other.CompareTag("Player"))
        {
            Vector3 newPosition = elevatorDoorLower.transform.position;
            newPosition.x += 10f;
            elevatorDoorLower.transform.position = newPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("close door");

        //If trigger detects player leaving collider on upper floor
        if (other.CompareTag("Player"))
        {
            Vector3 newPosition = elevatorDoorUpper.transform.position;
            newPosition.x += 1;
            elevatorPosition.transform.position = newPosition;
        }

        //If trigger detects player leaving collider on lower floor
        if (other.CompareTag("Player"))
        {
            Vector3 newPosition = elevatorDoorLower.transform.position;
            newPosition.x += 1;
            elevatorDoorLower.transform.position = newPosition;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("elevator rise");

        //If trigger detects player staying on elevator on lower level
        if (other.CompareTag("Player"))
        {
            if (!elevatorOnBottomFloor)
            {
                Vector3 newPosition = elevatorPosition.transform.position;
                newPosition.y += 25f;
                elevatorPosition.transform.position = newPosition;
            }

        }

        //If trigger detects player staying on elevator on lower level
        if (other.CompareTag("Player"))
        {
            if (!elevatorOnTopFloor)
            {
                Vector3 newPosition = elevatorPosition.transform.position;
                newPosition.y -= 25f;
                elevatorPosition.transform.position = newPosition;
            }

        }

    }
}
