
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class PhotoSaveLoadFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }
    
    private string CurrentTime => DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "_").Replace(":", "_");
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
        
        var path = Path.Combine(Application.persistentDataPath, owner.FileStorageFolder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var date = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "_").Replace(":", "_");
        Debug.Log(date);
        var fullPath = Path.Combine(path, $"{CurrentTime}.photodata");
        FileDataWithPhoto.Save(json, photo, fullPath);
        
        Debug.Log($"Save photo with data: {data.TargetItemId}");
    }


    public void SavePhotoWithoutData(Texture2D photo)
    {
        if(!enable) return;

        var path = Path.Combine(Application.persistentDataPath, owner.FileStorageFolder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var fullPath = Path.Combine(path, $"{CurrentTime}.photo");
        FileDataWithPhoto.Save(photo, fullPath);
        
        Debug.Log("Save photo");
    }


    public void LoadPhotoWithData(string fileName, out ItemPhotoData data, out Texture2D photo)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, owner.FileStorageFolder, $"{fileName}.photodata");
        FileDataWithPhoto.Load(fullPath, out data, out photo);
        
        Debug.Log("Load photo with data");
    }


    // public Texture2D LoadPhotoWithoutData(string fileName)
    // {
    //     var fullPath = Path.Combine(Application.persistentDataPath, owner.FileStorageFolder, $"{fileName}.photo");
    //     
    //     Debug.Log("Load photo");
    //     
    //     return FileDataWithPhoto.Load(fullPath);
    // }


    public List<FilePhotoData> GetAllSaveFiles()
    {
        var resultList = new List<FilePhotoData>();
        var filePaths = 
            Directory.GetFiles(Path.Combine(Application.persistentDataPath, owner.FileStorageFolder), "*.photodata");
        foreach (var file in filePaths)
        {
            FileDataWithPhoto.Load(file, out var data, out var photo);
            resultList.Add(new FilePhotoData() {data = data, photo = photo});
        }

        return resultList;
    }
    

    private string GetItemDataJSON(ItemPhotoData data)
    {
        return JsonUtility.ToJson(data);
    }
}