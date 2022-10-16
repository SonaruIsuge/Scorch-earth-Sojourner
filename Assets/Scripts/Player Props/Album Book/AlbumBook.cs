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

    
    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        
        BookData.InitAllData();
        BookView.InitPhoto(BookData.AllPhotoData);
        
        bindViewButton();
    }


    public void UnEquip()
    {
        unbindViewButton();
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


    private void bindViewButton()
    {
        BookView.albumBtn.onClick.AddListener(AlbumBtnClick);
        BookView.closeAlbumBtn.onClick.AddListener(AlbumCancelBtnClick);

        PhotoSaveLoadHandler.Instance.OnFileChanged += updateAlbum;
    }


    private void unbindViewButton()
    {
        BookView.albumBtn.onClick.RemoveListener(AlbumBtnClick);
        BookView.closeAlbumBtn.onClick.RemoveListener(AlbumCancelBtnClick);
        
        if(PhotoSaveLoadHandler.Instance != null) PhotoSaveLoadHandler.Instance.OnFileChanged -= updateAlbum;
    }


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
}
