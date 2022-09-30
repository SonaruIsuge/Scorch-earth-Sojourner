
using System.IO;
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


    public void SavePhoto(Texture2D target)
    {
        if (!enable) return;
        
        byte[] byteArray = target.EncodeToPNG();
        var path = Application.dataPath + owner.FilePath;
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        File.WriteAllBytes(path + "test01.png", byteArray);
    }


    public void LoadPhoto()
    {
        
    }
}
