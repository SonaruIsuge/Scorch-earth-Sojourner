using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlbumBookUI : MonoBehaviour
{
    private Player player;
    private AlbumBook albumBook;

    [SerializeField] private RectTransform albumUI;
    [SerializeField] private RectTransform photoPage;
    [SerializeField] private RectTransform bookPhotoArea;
    [SerializeField] private TMP_Text photoDescription;
    private RawImage currentDisplayPhoto;
    
    [SerializeField] private Button albumBtn;
    [SerializeField] private Button closeAlbumBtn;
    [SerializeField] private Button LastPageBtn;
    [SerializeField] private Button NextPageBtn;
    [SerializeField] private Button DeleteBtn;
    
    [SerializeField] private string presetDescription;
    

    // Panel data
    public float DisplayPhotoWidth;
    public float DisplayPhotoHeight;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        currentDisplayPhoto = bookPhotoArea.GetComponent<RawImage>();
    }
    
    
    private void OnEnable()
    {
        player.OnPropEquipped += RegisterAlbumBookUI;
    }


    private void OnDisable()
    {
        player.OnPropEquipped -= RegisterAlbumBookUI;
    }


    private void RegisterAlbumBookUI(IPlayerProp prop)
    {
        if(!(prop is AlbumBook)) return;
        
        albumBook = (AlbumBook) prop;
        albumUI.gameObject.SetActive(false);

        bookPhotoArea.sizeDelta = new Vector2(DisplayPhotoWidth, DisplayPhotoHeight);
        
        

        albumBook.OnAlbumBookToggleEnable += AlbumBookUIEnable;
        albumBook.OnAlbumChangeCurrentPhoto += ChangeCurrentPhotoData;
        albumBook.OnAlbumPageTypeChange += ChangeAlbumTypePage;

        albumBtn.onClick.AddListener(() => albumBook.EnableProp(true));
        closeAlbumBtn.onClick.AddListener(()=>albumBook.EnableProp(false));
        LastPageBtn.onClick.AddListener(OnLastBtnClick);
        NextPageBtn.onClick.AddListener(OnNextBtnClick);
        DeleteBtn.onClick.AddListener(OnDeleteBtnClick);
    }


    private void AlbumBookUIEnable(bool enable)
    {
        albumUI.gameObject.SetActive(enable);
        var firstPhoto = albumBook.GetCurrentChooseData();
        if (firstPhoto != null) ChangeCurrentPhotoData(firstPhoto);
        else photoDescription.text = presetDescription;

        var imageColor = currentDisplayPhoto.color;
        imageColor.a = firstPhoto != null ? 1 : 0;
        currentDisplayPhoto.color = imageColor;
    }


    private void ChangeCurrentPhotoData(FilePhotoData filePhotoData)
    {
        var imageColor = currentDisplayPhoto.color;
        
        if (filePhotoData == null)
        {
            currentDisplayPhoto.texture = null;
            photoDescription.text = presetDescription;
            imageColor.a = 0;
            currentDisplayPhoto.color = imageColor;
        }
        else
        {
            currentDisplayPhoto.texture = filePhotoData.photo;
            photoDescription.text = filePhotoData.data == null ? presetDescription : ItemControlHandler.Instance.GetRecordableItemById(filePhotoData.data.TargetItemId).Description;
            imageColor.a = 1;
            currentDisplayPhoto.color = imageColor;
        }
    }


    private void ChangeAlbumTypePage(AlbumPage targetPage)
    {
        photoPage.gameObject.SetActive(targetPage == AlbumPage.Photo);
    }
    

    private void OnLastBtnClick()
    {
        albumBook.SetCurrentChoosePhoto(true, false);
    }


    private void OnNextBtnClick()
    {
        albumBook.SetCurrentChoosePhoto(false, true);
    }


    private void OnDeleteBtnClick()
    {
        var targetPhoto = albumBook.GetCurrentChooseData();
        if (targetPhoto == null) return;
        PhotoSaveLoadHandler.Instance.RemoveData(targetPhoto.fileName);
    }
}

