using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SonaruUtilities;
using UnityEngine;

public sealed class PhotoSaveLoadHandler : TSingletonMonoBehaviour<PhotoSaveLoadHandler>
{
    [SerializeField] private string photoDataStorageFolder = "SaveData";
    public List<FilePhotoData> AllFilePhotoList;
    //public List<PhotoData> AllPhotoList;
    private string storagePath => Path.Combine(Application.persistentDataPath, photoDataStorageFolder);
    private string currentTime => DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "_").Replace(":", "_");
    
    private string targetFullPath;

    public event Action OnFileChanged;
    
    protected override void Awake()
    {
        base.Awake();

        AllFilePhotoList = new List<FilePhotoData>();
        GetAllSaveFiles();
    }
    
    
    public void SavePhoto(Texture2D photo, ItemPhotoData data = null)
    {
        if (!Directory.Exists(storagePath)) Directory.CreateDirectory(storagePath);
        
        // Photo without data
        if (data == null)
        {
            targetFullPath = Path.Combine(storagePath, $"{currentTime}.photodata");
            FileDataWithPhoto.Save(photo, targetFullPath);
            OnFileChanged?.Invoke();
            return;
        }
        
        // Photo with data
        targetFullPath = Path.Combine(storagePath, $"{currentTime}.photodata");
        var json = GetItemDataJSON((ItemPhotoData)data);
        FileDataWithPhoto.Save(json, photo, targetFullPath);
        OnFileChanged?.Invoke();
    }


    public void LoadPhoto(string fileName, out ItemPhotoData data, out Texture2D photo)
    {
        var fullPath = Path.Combine(storagePath, $"{fileName}.photodata");
        FileDataWithPhoto.Load(fullPath, out data, out photo);
    }


    public List<FilePhotoData> GetAllSaveFiles()
    {
        var resultList = new List<FilePhotoData>();
        var filePaths = 
            Directory.GetFiles(storagePath, "*.photodata");
        foreach (var file in filePaths)
        {
            FileDataWithPhoto.Load(file, out var data, out var photo);
            resultList.Add(new FilePhotoData {fileName = Path.GetFileName(file), data = data, photo = photo});
        }

        AllFilePhotoList = resultList;
        return resultList;
    }


    public void RemoveData(string fileName)
    {
        
    }


    public void ClearAllData()
    {
        
    }
    

    private string GetItemDataJSON(ItemPhotoData data)
    {
        return JsonUtility.ToJson(data);
    }
}
