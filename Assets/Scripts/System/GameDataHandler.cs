


using System;
using System.Collections.Generic;
using System.IO;
using SonaruUtilities;
using UnityEngine;

public class GameDataHandler : TSingletonMonoBehaviour<GameDataHandler>
{
    private const string fileName = "gamedata.json";
    
    [SerializeField] private string SaveFolder = "GameSave";
    private string storagePath => Path.Combine(Application.persistentDataPath, SaveFolder);
    private string fileFullPath => Path.Combine(storagePath, fileName);

    [field: SerializeField] public GameSaveData SaveData { get; private set; }

    public event Action OnDataChanged;
    
    
    protected override void Awake()
    {
        base.Awake();
        LoadGameData();
    }
    
    [ContextMenu("Save Game Data")]
    public void SaveGameData()
    {
        if (!Directory.Exists(storagePath)) Directory.CreateDirectory(storagePath);
        
        var jsonString = JsonUtility.ToJson(SaveData);
        File.WriteAllText(fileFullPath, jsonString);
        
        OnDataChanged?.Invoke();
    }

    
    [ContextMenu("Load Game Data")]
    public void LoadGameData()
    {
        if (!Directory.Exists(storagePath)) Directory.CreateDirectory(storagePath);
        
        if (!File.Exists(fileFullPath))
        {
            SaveData = new GameSaveData();
            return;
        }
        
        var fileContents = File.ReadAllText(fileFullPath);
        SaveData = JsonUtility.FromJson<GameSaveData>(fileContents);
    }
}


[Serializable]
public class GameSaveData
{
    [field: SerializeField] public List<int> AllGetMemoId { get; private set; }

    public GameSaveData()
    {
        AllGetMemoId = new List<int>();
    }
}
