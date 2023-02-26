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
    
    
    // --------------------------------------------------------------------------
    // Add: Save new file
    #region Add

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

    #endregion
    

    // --------------------------------------------------------------------------
    //Remove: remove specific file, remove all files
    #region Remove

    public void RemoveData(string fileName)
    {
        var fullPath = Path.Combine(storagePath, fileName + ".photodata");
        if(!File.Exists(fullPath)) return;
        
        File.Delete(fullPath);
        OnFileChanged?.Invoke();
    }

    [ContextMenu("Clear All Data")]
    public void ClearAllData()
    {
        if (Directory.Exists(storagePath))
        {
            Directory.Delete(storagePath, true);
        }
        
        Directory.CreateDirectory(storagePath);
        OnFileChanged?.Invoke();
    }

    #endregion
    
    
    // --------------------------------------------------------------------------
    // Find: Load specific file, load all files
    #region Find

    // in: File Name
    // out: ItemPhotoData, Texture
    public void LoadPhoto(string fileName, out ItemPhotoData data, out Texture2D photo)
    {
        var fullPath = Path.Combine(storagePath, $"{fileName}.photodata");
        FileDataWithPhoto.Load(fullPath, out data, out photo);
    }

    
    // in: File Name
    // out: FilePhotoData
    public FilePhotoData LoadPhoto(string fileName)
    {
        LoadPhoto(fileName, out var data, out var photo);
        return new FilePhotoData() {fileName = fileName, data = data, photo = photo};
    }
    

    [ContextMenu("Load All Files")]
    public List<FilePhotoData> GetAllSaveFiles()
    {
        var resultList = new List<FilePhotoData>();
        
        // check if path exist
        if (!Directory.Exists(storagePath)) Directory.CreateDirectory(storagePath);
        
        var filePaths = 
            Directory.GetFiles(storagePath, "*.photodata");

        if (filePaths.Length <= 0) return new List<FilePhotoData>();

        foreach (var file in filePaths)
        {
            FileDataWithPhoto.Load(file, out var data, out var photo);
            resultList.Add(new FilePhotoData {fileName = Path.GetFileNameWithoutExtension(file), data = data, photo = photo});
        }

        AllFilePhotoList = resultList;
        return resultList;
    }

    #endregion
    
    
    // --------------------------------------------------------------------------
    //Change: Change specific file, change file name

    
    
    // --------------------------------------------------------------------------
    // Other function:
    #region Other

    public List<string> GetAllFileName()
    {
        var fileNameList = new List<string>();
        var allFilePaths = Directory.GetFiles(storagePath, "*.photodata");
        foreach (var file in allFilePaths)
        {
            fileNameList.Add(Path.GetFileName(file));
        }

        return fileNameList;
    }
    
    
    private string GetItemDataJSON(ItemPhotoData data)
    {
        return JsonUtility.ToJson(data);
    }

    #endregion
    
}
