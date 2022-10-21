using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumBook : MonoBehaviour, IPlayerProp
{
    public Player player { get; private set; }
    
    public AlbumBookData BookData;
    public AlbumBookView BookView;
    
    public int MaxSaveData;

    [SerializeField] private int currentPage;
    [SerializeField] private string currentPhotoName;
    public int CurrentPage
    {
        get => currentPage;
        set
        {
            currentPage = Mathf.Min(Mathf.Max(1, value), BookView.allPageList.Count);
            OnChangeCurrentPageIndex?.Invoke(currentPage);
        }
    }

    private event Action<int> OnChangeCurrentPageIndex;
    
    public bool OpenAlbumBtn { get; private set; }
    

    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        
        BookData.InitAllData();
        BookView.InitPhoto(BookData.AllPhotoData);
        
        bindViewEvent();

        CurrentPage = 1;
    }


    public void UnEquip()
    {
        unbindViewEvent();
    }


    public void EnableProp(bool enable)
    {
        enabled = enable;
        BookView.EnableView(enable);
    }


    private void updateAlbum()
    {
        BookData.UpdateData();
        BookView.UpdateView(BookData.AllPhotoData);
    }


    private void bindViewEvent()
    {
        BookView.albumBtn.onClick.AddListener(AlbumBtnClick);
        BookView.closeAlbumBtn.onClick.AddListener(AlbumCancelBtnClick);
        BookView.LastPageBtn.onClick.AddListener(() => CurrentPage--);
        BookView.NextPageBtn.onClick.AddListener(() => CurrentPage++);

        OnChangeCurrentPageIndex += BookView.ShowCurrentPage;
        PhotoSaveLoadHandler.Instance.OnFileChanged += updateAlbum;
    }


    private void unbindViewEvent()
    {
        BookView.albumBtn.onClick.RemoveAllListeners();
        BookView.closeAlbumBtn.onClick.RemoveAllListeners();
        BookView.LastPageBtn.onClick.RemoveAllListeners();
        BookView.NextPageBtn.onClick.RemoveAllListeners();
        
        OnChangeCurrentPageIndex -= BookView.ShowCurrentPage;
        if(PhotoSaveLoadHandler.Instance != null) PhotoSaveLoadHandler.Instance.OnFileChanged -= updateAlbum;
    }

    
    #region Bind Function

    private void AlbumBtnClick()
    {
        BookView.bookPanel.gameObject.SetActive(true);
        player.ChangePropState(UsingProp.AlbumBook);
    }

    private void AlbumCancelBtnClick()
    {
        BookView.bookPanel.gameObject.SetActive(false);
        player.ChangePropState(UsingProp.None);
    }

    #endregion
}
