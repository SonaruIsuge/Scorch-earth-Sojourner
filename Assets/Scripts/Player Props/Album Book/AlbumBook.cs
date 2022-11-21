using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumBook : MonoBehaviour, IPlayerProp
{
    public Player player { get; private set; }
    
    [SerializeField] private AlbumBookData BookData;
    [SerializeField] private BookMemoData memoData;
    public int MaxSaveData;

    [SerializeField] private AlbumPage currentPage;
    
    [SerializeField] private int currentChoosePhotoIndex;

    public int CurrentChoosePhotoIndex
    {
        get => currentChoosePhotoIndex;
        set
        {
            currentChoosePhotoIndex = Mathf.Max(Mathf.Min(value, BookData.AllPhotoData.Count - 1), 0);
            OnAlbumChangeCurrentPhoto?.Invoke(BookData.AllPhotoData.Count > 0 ? BookData.AllPhotoData[currentChoosePhotoIndex] : null);
        }
    }

    [SerializeField] private int currentChooseMemoIndex;

    public int CurrentChooseMemoIndex
    {
        get => currentChooseMemoIndex;
        set
        {
            currentChooseMemoIndex = Mathf.Max(Mathf.Min(value, memoData.AllMemoId.Count - 1), 0);
            var id = memoData.AllMemoId.Count > 0 ? memoData.AllMemoId[currentChooseMemoIndex] : -1;
            OnAlbumChangeCurrentMemo?.Invoke(memoData.GetTargetMemo(id));
        }
    }

    public event Action<bool> OnAlbumBookToggleEnable;
    public event Action<FilePhotoData> OnAlbumChangeCurrentPhoto;
    public event Action<AlbumPage> OnAlbumPageTypeChange;
    public event Action<MemoData> OnGetNewMemo;
    public event Action<MemoData> OnAlbumChangeCurrentMemo;
    
    

    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        
        BookData.InitAllData();
        memoData.LoadAllMemo();
        
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


    public void SetCurrentPage(bool turnLeft, bool turnRight)
    {
        var allPage = Enum.GetValues(typeof(AlbumPage)).Length;
        var currentPageInt = (int) currentPage;
        if (turnLeft)
        {
            currentPageInt--;
            if (currentPageInt < 0) currentPageInt = allPage - 1;
        }
        if (turnRight)
        {
            currentPageInt++;
            if (currentPageInt >= allPage) currentPageInt = 0;
        }

        currentPage = (AlbumPage) currentPageInt;
        OnAlbumPageTypeChange?.Invoke(currentPage);
    }


    public void GetNewMemo(MemoData newData)
    {
        memoData.AddNewMemo(newData);
        
        OnGetNewMemo?.Invoke(newData);
    }


    public void SetCurrentChoosePhoto(bool left, bool right)
    {
        if (left)
        {
            if (currentPage == AlbumPage.Photo) CurrentChoosePhotoIndex--;
            if (currentPage == AlbumPage.Memo) CurrentChooseMemoIndex--;
        }

        if (right)
        {
            if (currentPage == AlbumPage.Photo) CurrentChoosePhotoIndex++;
            if (currentPage == AlbumPage.Memo) CurrentChooseMemoIndex++;
        }
    }


    public List<FilePhotoData> GetAllPhotoData()
    {
        return BookData.AllPhotoData;
    }


    public FilePhotoData GetCurrentChooseData()
    {
        if (BookData.AllPhotoData.Count <= 0) return null;
        
        return BookData.AllPhotoData[currentChoosePhotoIndex];
    }


    public MemoData GetCurrentChooseMemo()
    {
        if (memoData.AllMemoId.Count <= 0) return null;
        return memoData.GetTargetMemo(memoData.AllMemoId[currentChooseMemoIndex]);
    }
    
    
    public int GetLastPhotoIndex()
    {
        if (BookData.AllPhotoData.Count <= 0) return 0;
        return BookData.AllPhotoData.Count - 1;
    }
}
