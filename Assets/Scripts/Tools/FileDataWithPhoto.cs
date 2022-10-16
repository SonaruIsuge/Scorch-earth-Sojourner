
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// This script responsible for:
/// Turning one photo with / without data into byte[] file and saving to target path.
/// Reading file from target path and returning it to photo (and data).
/// </summary>

public class FileDataWithPhoto
{
    [Serializable]
    public class Header
    {
        public int jsonByteSize;
    }

    
    #region Save
    
    // Save format: HeaderSize(2 bytes) + Header(Record data byte size) + ItemPhotoData + Texture2D
    public static void Save(string json, Texture2D photo, string fullDataPath)
    {
        byte[] jsonByteArray = Encoding.Unicode.GetBytes(json);
        byte[] photoByteArray = photo.EncodeToPNG();

        var header = new Header
        {
            jsonByteSize = jsonByteArray.Length
        };

        var headerJson = JsonUtility.ToJson(header);
        byte[] headerJsonByteArray = Encoding.Unicode.GetBytes(headerJson);

        ushort headerSize = (ushort) headerJsonByteArray.Length;
        byte[] headerSizeByteArray = BitConverter.GetBytes(headerSize);

        List<byte> byteList = new List<byte>();
        byteList.AddRange(headerSizeByteArray);
        byteList.AddRange(headerJsonByteArray);
        byteList.AddRange(jsonByteArray);
        byteList.AddRange(photoByteArray);
        
        File.WriteAllBytes(fullDataPath, byteList.ToArray());
    }
    

    // Save format: HeaderSize(2 bytes) + Header(Record data byte size = 0) + Texture2D
    public static void Save(Texture2D photo, string fullDataPath)
    {
        byte[] photoByteArray = photo.EncodeToPNG();
        
        var header = new Header
        {
            jsonByteSize = 0
        };
        
        var headerJson = JsonUtility.ToJson(header);
        byte[] headerJsonByteArray = Encoding.Unicode.GetBytes(headerJson);

        ushort headerSize = (ushort) headerJsonByteArray.Length;
        byte[] headerSizeByteArray = BitConverter.GetBytes(headerSize);
        
        List<byte> byteList = new List<byte>();
        byteList.AddRange(headerSizeByteArray);
        byteList.AddRange(headerJsonByteArray);
        byteList.AddRange(photoByteArray);
        
        //List<byte> byteList = new List<byte>(photoByteArray);
        
        File.WriteAllBytes(fullDataPath, byteList.ToArray());
    }
    
    #endregion


    #region Load
    
    
    public static void Load(string fullDataPath, out ItemPhotoData data, out Texture2D photo)
    {
        byte[] byteArray = File.ReadAllBytes(fullDataPath);
        List<byte> byteList = new List<byte>(byteArray);
        
        //header size
        ushort headerSize = BitConverter.ToUInt16(new byte[] {byteList[0], byteList[1]}, 0);
        
        // header
        List<byte> headerByteList = byteList.GetRange(2, headerSize);
        string headerJson = Encoding.Unicode.GetString(headerByteList.ToArray());
        Header header = JsonUtility.FromJson<Header>(headerJson);
        
        // data
        if (header.jsonByteSize != 0)
        {
            List<byte> jsonByteList = byteList.GetRange(2 + headerSize, header.jsonByteSize);
            string dataJson = Encoding.Unicode.GetString(jsonByteList.ToArray());
            data = JsonUtility.FromJson<ItemPhotoData>(dataJson);
        }
        else data = null;

        //photo
        var startIndex = 2 + headerSize + header.jsonByteSize;
        var endIndex = byteArray.Length - startIndex;
        List<byte> photoByteList = byteList.GetRange(startIndex, endIndex);
        photo = new Texture2D(1, 1, TextureFormat.RGB24, false);
        photo.LoadImage(photoByteList.ToArray());
    }
    
    
    
    
    
    #endregion
}
