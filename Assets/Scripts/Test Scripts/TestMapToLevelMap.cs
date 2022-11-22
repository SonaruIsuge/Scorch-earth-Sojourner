using System;
using UnityEngine;


public class TestMapToLevelMap : MonoBehaviour
{
    private Camera mainCamera => Camera.main;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraFollowControl>().enabled = false;
        }
    }
}
