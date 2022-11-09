using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumBook : MonoBehaviour, IPlayerProp
{
    public Player player { get; private set; }
    
    [SerializeField] private AlbumBookData BookData;
    [SerializeField] private AlbumBookView BookView;
    
    public int MaxSaveData;
    
    [SerializeField] private string currentChoosePhotoName;
    public string CurrentSubmitPhotoName => BookView.SubmitPhotoName;
    

    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        
        BookData.InitAllData();
        BookView.InitPhoto(BookData.AllPhotoData);
        
        bindViewEvent();
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


    public void SetCurrentPage(int addPage)
    {
        BookView.ChangePage(addPage);
    }

    public void SetCurrentChoosePhoto(bool up, bool down, bool left, bool right)
    {
        BookView.SetCurrentChoosePhoto(up, down, left, right);
        currentChoosePhotoName = BookView.allPhotoList.Count > 0 ? BookView.allPhotoList[BookView.CurrentChooseViewObjIndex].PhotoName : null;
        BookData.CurrentPhotoData = BookData.GetFilePhotoData(currentChoosePhotoName);
    }


    public void SetSubmitPhoto()
    {
        BookView.SendSubmitMessage();
        BookData.CurrentPhotoData = BookData.GetFilePhotoData(CurrentSubmitPhotoName);
    }


    public void TryGetPhotoData(out Texture2D photo, out ItemPhotoData data)
    {
        var filePhotoData = BookData.GetFilePhotoData(CurrentSubmitPhotoName);
        photo = filePhotoData?.photo;
        data = filePhotoData?.data;
    }


    public List<FilePhotoData> GetAllPhotoData()
    {
        return BookData.AllPhotoData;
    }
    

    private void bindViewEvent()
    {
        BookView.albumBtn.onClick.AddListener(AlbumBtnClick);
        BookView.closeAlbumBtn.onClick.AddListener(AlbumCancelBtnClick);
        BookView.LastPageBtn.onClick.AddListener(() => BookView.ChangePage(-1));
        BookView.NextPageBtn.onClick.AddListener(() => BookView.ChangePage(1));
        
        PhotoSaveLoadHandler.Instance.OnFileChanged += updateAlbum;
    }


    private void unbindViewEvent()
    {
        BookView.albumBtn.onClick.RemoveAllListeners();
        BookView.closeAlbumBtn.onClick.RemoveAllListeners();
        BookView.LastPageBtn.onClick.RemoveAllListeners();
        BookView.NextPageBtn.onClick.RemoveAllListeners();
        
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
