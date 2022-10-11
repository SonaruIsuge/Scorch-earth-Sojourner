using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumBook : MonoBehaviour
{
    public AlbumBookData BookData;
    public AlbumBookView BookView;

    
     private void Awake()
    {
        BookData.LoadAllData();
        BookView.InitPhoto(BookData.AllPhotoData);
    }
     
    
    private void OnEnable()
    {
        PhotoSaveLoadHandler.Instance.OnFileChanged += BookData.LoadAllData;
    }


    private void OnDisable()
    {
        //if(PhotoSaveLoadHandler.Instance != null) PhotoSaveLoadHandler.Instance.OnFileChanged -= BookData.LoadAllData;
    }


    private void Update()
    {
        
    }
}
