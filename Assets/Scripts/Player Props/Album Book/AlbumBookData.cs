
using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class AlbumBookData : MonoBehaviour
{
    [SerializeField] private List<FilePhotoData> allPhotoData;
    public List<FilePhotoData> AllPhotoData => allPhotoData;

    private Dictionary<string, FilePhotoData> fileDataDict;
    

    #region Init data
    
    public void InitAllData()
    {
        allPhotoData = new List<FilePhotoData>();
        fileDataDict = new Dictionary<string, FilePhotoData>();
        
        allPhotoData = PhotoSaveLoadHandler.Instance.GetAllSaveFiles();
        InitDataDict();
    }

    
    private void InitDataDict()
    {
        foreach (var data in allPhotoData)
        {
            fileDataDict.Add(data.fileName, data);
        }
    }
    
    #endregion

    #region Renew data

    public void UpdateData()
    {
        allPhotoData = PhotoSaveLoadHandler.Instance.GetAllSaveFiles();
        fileDataDict.Clear();
        InitDataDict();
    }

    #endregion
    
    public FilePhotoData? GetFilePhotoData(string fileName)
    {
        if (!fileDataDict.ContainsKey(fileName)) return null;

        return fileDataDict[fileName];
    }


    
}
