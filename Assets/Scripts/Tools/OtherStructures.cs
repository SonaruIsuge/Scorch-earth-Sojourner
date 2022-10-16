
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemPhotoData
{
    public int TargetItemId;
    public Vector2 PositionInPhoto;
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
    private Button PhotoBtn;
    private TMP_Text PhotoText;

    public event Action<string> OnPhotoViewObjClick;

    public PhotoViewObj(RectTransform obj)
    {
        PhotoRectTransform = obj;

        PhotoImage = obj.GetComponent<RawImage>();
        PhotoBtn = obj.GetComponent<Button>();
        PhotoText = obj.GetComponentInChildren<TMP_Text>();

        PhotoBtn.onClick.AddListener(() => OnPhotoViewObjClick?.Invoke(PhotoName));
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
        OnPhotoViewObjClick += newEvent;
    }

    
    public void RemoveClickEvent()
    {
        OnPhotoViewObjClick = null;
    }
}