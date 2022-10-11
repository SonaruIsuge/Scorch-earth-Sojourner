
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class AlbumBookData : MonoBehaviour
{
    [SerializeField] private List<FilePhotoData> allPhotoData;
    public List<FilePhotoData> AllPhotoData => allPhotoData;

    private Dictionary<string, FilePhotoData> fileDataDict;

    private void Awake()
    {
        allPhotoData = new List<FilePhotoData>();

        fileDataDict = new Dictionary<string, FilePhotoData>();
    }


    public void LoadAllData()
    {
        allPhotoData = PhotoSaveLoadHandler.Instance.GetAllSaveFiles();
    }


    public FilePhotoData? GetFilePhotoData(string fileName)
    {
        if (!fileDataDict.ContainsKey(fileName)) return null;

        return fileDataDict[fileName];
    }


    private void InitDataDict()
    {
        foreach (var data in allPhotoData)
        {
            fileDataDict.Add(data.fileName, data);
        }
    }
}
