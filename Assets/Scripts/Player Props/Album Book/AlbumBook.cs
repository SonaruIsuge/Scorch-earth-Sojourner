using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumBook : MonoBehaviour, IPlayerProp
{
    public Player player { get; private set; }
    
    [SerializeField] private AlbumBookData BookData;
    public int MaxSaveData;
    
    [SerializeField] private int currentChoosePhotoIndex;

    public int CurrentChoosePhotoIndex
    {
        get => currentChoosePhotoIndex;
        set
        {
            currentChoosePhotoIndex = Mathf.Max(Mathf.Min(value, BookData.AllPhotoData.Count - 1), 0);
            OnAlbumChangeCurrentPhoto?.Invoke(BookData.AllPhotoData.Count > 0 ?BookData.AllPhotoData[currentChoosePhotoIndex] : null);
        }
    }

    public event Action<bool> OnAlbumBookToggleEnable;
    public event Action<FilePhotoData?> OnAlbumChangeCurrentPhoto;
    
    

    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        
        BookData.InitAllData();
        
        enabled = false;
        CurrentChoosePhotoIndex = 0;
        
        PhotoSaveLoadHandler.Instance.OnFileChanged += updateAlbum;
    }


    public void UnEquip()
    {
        player = null;
        if(PhotoSaveLoadHandler.Instance != null) PhotoSaveLoadHandler.Instance.OnFileChanged -= updateAlbum;
    }


    public void EnableProp(bool enable)
    {
        enabled = enable;
        OnAlbumBookToggleEnable?.Invoke(enable);
    }


    private void updateAlbum()
    {
        BookData.UpdateData();
        CurrentChoosePhotoIndex = Mathf.Max(Mathf.Min(CurrentChoosePhotoIndex, BookData.AllPhotoData.Count - 1), 0);
    }


    public void SetCurrentChoosePhoto(bool left, bool right)
    {
        if (left) CurrentChoosePhotoIndex--;
        if (right) CurrentChoosePhotoIndex++;
    }


    public List<FilePhotoData> GetAllPhotoData()
    {
        return BookData.AllPhotoData;
    }


    public FilePhotoData? GetCurrentChooseData()
    {
        if (BookData.AllPhotoData.Count <= 0) return null;
        
        return BookData.AllPhotoData[currentChoosePhotoIndex];
    }
    
    
    public int GetLastPhotoIndex()
    {
        if (BookData.AllPhotoData.Count <= 0) return 0;
        return BookData.AllPhotoData.Count - 1;
    }
}
