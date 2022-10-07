using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoLoadTest : MonoBehaviour
{
    public List<FilePhotoData> AllData;
    
    // Start is called before the first frame update
    void Awake()
    {
        AllData = new List<FilePhotoData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            AllData = PhotoSaveLoadHandler.Instance.GetAllSaveFiles();
        }    
    }
}
