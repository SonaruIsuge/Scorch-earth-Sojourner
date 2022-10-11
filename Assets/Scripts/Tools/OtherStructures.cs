
using UnityEngine;


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

