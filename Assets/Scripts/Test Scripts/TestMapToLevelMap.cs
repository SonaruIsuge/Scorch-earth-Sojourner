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
            other.transform.localScale = Vector3.one * (2.0f / 3.0f); 
        }
    }
}
