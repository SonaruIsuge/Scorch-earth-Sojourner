
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileDataWithPhoto
{
    [Serializable]
    public class Header
    {
        public int jsonByteSize;
    }
    
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
        List<byte> jsonByteList = byteList.GetRange(2 + headerSize, header.jsonByteSize);
        string dataJson = Encoding.Unicode.GetString(jsonByteList.ToArray());
        data = JsonUtility.FromJson<ItemPhotoData>(dataJson);
        
        //photo
        var startIndex = 2 + headerSize + header.jsonByteSize;
        var endIndex = byteArray.Length - startIndex;
        List<byte> photoByteList = byteList.GetRange(startIndex, endIndex);
        photo = new Texture2D(1, 1, TextureFormat.RGB24, false);
        photo.LoadImage(photoByteList.ToArray());
    }
}
