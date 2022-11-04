﻿
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemPhotoData
{
    public int TargetItemId;
    public Vector2 PositionFromCenter;
    public float cameraOrthoSize;
}


[System.Serializable]
public struct FilePhotoData
{
    public string fileName;
    public Texture2D photo;
    public ItemPhotoData data;
}


[System.Serializable]
public class PhotoViewObj
{
    public RectTransform PhotoRectTransform { get; private set; }
    public string PhotoName { get; private set; }

    private RawImage PhotoImage;
    private TMP_Text PhotoText;
    private Button PhotoBtn;
    private Button RemovePhotoBtn;

    private event Action<string> OnPhotoViewObjClick;
    private event Action<string> OnPhotoRemoveBtnClick;

    public PhotoViewObj(RectTransform obj)
    {
        PhotoRectTransform = obj;

        PhotoImage = obj.GetComponent<RawImage>();
        PhotoBtn = obj.GetComponent<Button>();
        PhotoText = obj.GetComponentInChildren<TMP_Text>();
        // Since get component also find parent's component, get second button component.
        RemovePhotoBtn = obj.GetComponentsInChildren<Button>()[1];
        
        PhotoBtn.onClick.AddListener(() => OnPhotoViewObjClick?.Invoke(PhotoName));
        RemovePhotoBtn.onClick.AddListener(() => OnPhotoRemoveBtnClick?.Invoke(PhotoName));
    }


    public void SetName(string newName)
    {
        PhotoName = newName;
        PhotoText.text = newName;
    }


    public void SetImage(Texture2D image)
    {
        PhotoImage.texture = image;
    }


    public void SetLocalPosition(Vector3 pos)
    {
        PhotoRectTransform.localPosition = pos;
    }
    

    public void SetClickEvent(Action<string> newEvent)
    {
        OnPhotoViewObjClick = null;
        OnPhotoViewObjClick += newEvent;
    }


    public void SetRemoveEvent(Action<string> newEvent)
    {
        OnPhotoRemoveBtnClick = null;
        OnPhotoRemoveBtnClick += newEvent;
    }

    
    public void RemoveAllClickEvent()
    {
        OnPhotoViewObjClick = null;
        OnPhotoRemoveBtnClick = null;
    }
    
}