
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class PhotoSaveLoadFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }
    
    
    public void EnableFeature(bool b)
    {
        enable = b;
    }


    public PhotoSaveLoadFeature(MemoryCamera camera)
    {
        owner = camera;
        enable = true;
    }


    // public void SavePhoto(Texture2D target)
    // {
    //     if (!enable) return;
    //     
    //     byte[] byteArray = target.EncodeToPNG();
    //     var path = Application.dataPath + owner.FilePath;
    //     if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    //     File.WriteAllBytes(path + "test01.png", byteArray);
    // }


    public void SavePhotoWithData(Texture2D photo, ItemPhotoData data)
    {
        if(!enable) return;
        
        var json = GetItemDataJSON(data);
        
        var path = Application.dataPath + owner.FilePath;
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var fullPath = path + "SaveFile.photodata";
        FileDataWithPhoto.Save(json, photo, fullPath);
    }


    public void LoadPhoto(out ItemPhotoData data, out Texture2D photo)
    {
        var fullPath = Application.dataPath + owner.FilePath + "SaveFile.photodata";
        FileDataWithPhoto.Load(fullPath, out data, out photo);
    }



    private string GetItemDataJSON(ItemPhotoData data)
    {
        return JsonUtility.ToJson(data);
    }
}
