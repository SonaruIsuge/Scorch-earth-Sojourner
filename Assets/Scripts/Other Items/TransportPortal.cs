using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPortal : MonoBehaviour
{

    [SerializeField] private TransportPortal pairPortal;

    private bool isDestinationThisTime;
    private IRoomSwitcher roomSwitcher;


    private void Awake()
    {
        roomSwitcher = GetComponent<IRoomSwitcher>();
    }
    

    private void IsTransportDestination()
    {
        isDestinationThisTime = true;
    }
    
    
    private void OnDrawGizmos()
    {
        if(!pairPortal) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, pairPortal.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDestinationThisTime)
        {
            other.transform.position = pairPortal.transform.position;
            pairPortal.IsTransportDestination();
            pairPortal.roomSwitcher?.SetOnRoomChange();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDestinationThisTime) isDestinationThisTime = false;
    }
}
