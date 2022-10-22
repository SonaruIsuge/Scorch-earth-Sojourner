
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;


public class AlbumBookView : MonoBehaviour
{
    // Components in canvas
    public RectTransform bookPanel;
    public Button albumBtn;
    
    public Button closeAlbumBtn;
    public Button LastPageBtn;
    public Button NextPageBtn;
    [SerializeField] private RectTransform choosingPhotoTrans;
    
    // Instantiate prefab
    [SerializeField] private RectTransform pagePrefab;
    [SerializeField] private RectTransform photoPrefab;

    // Panel data
    public float PhotoWidth;
    public float PhotoHeight;
    public float XSpacing;
    public float YSpacing;
    
    // Calculate data
    private float onePhotoWidth => photoPrefab.rect.width + XSpacing;
    private float onePhotoHeight => photoPrefab.rect.height + YSpacing;
    private int photoNumInRow => (int) ( (bookPanel.rect.width - PhotoWidth) / onePhotoWidth) + 1;
    private int photoNumInColumn => (int) ( (bookPanel.rect.height - PhotoHeight) / onePhotoHeight) + 1;
    private int containNum => photoNumInRow * photoNumInColumn;
    private float pageWidth => photoNumInRow * onePhotoWidth - XSpacing;
    private float pageHeight => photoNumInColumn * onePhotoHeight - YSpacing;
    
    // canvas data which interact with album data
    public List<RectTransform> allPageList {get; private set;}
    [field: SerializeField] public List<PhotoViewObj> allPhotoList {get; private set;}
    
    // current show page / current choose photo
    [SerializeField]
    private int currentPage {
        set => ShowCurrentPage(value);
    }
    // public int CurrentPage
    // {
    //     get => currentPage;
    //     set
    //     {
    //         currentPage = Mathf.Clamp(value, 0, allPageList.Count - 1);
    //         ShowCurrentPage(currentPage);
    //     }
    // }
    
    [SerializeField] private int currentViewObjIndex;
    public int CurrentViewObjIndex
    {
        get => currentViewObjIndex;
        private set
        {
            currentViewObjIndex = Mathf.Clamp(value, 0, allPhotoList.Count - 1);
            currentPage = currentViewObjIndex / containNum;
            if(choosingPhotoTrans) choosingPhotoTrans.localPosition = calcPhotoPosInCanvas(currentViewObjIndex);
        }
    }



    public void EnableView(bool enable)
    {
        bookPanel.gameObject.SetActive(enable);
    }


    private void GetAllNeedComponent()
    {
        closeAlbumBtn = bookPanel.Find("Exit Button").GetComponent<Button>();
        LastPageBtn = bookPanel.Find("Last Page Button").GetComponent<Button>();
        NextPageBtn = bookPanel.Find("Next Page Button").GetComponent<Button>();
        choosingPhotoTrans = bookPanel.Find("Choosing Photo Frame").GetComponent<RectTransform>();
    }


    public void InitPhoto(List<FilePhotoData> dataList)
    {
        GetAllNeedComponent();
        
        allPageList = new List<RectTransform>();
        allPhotoList = new List<PhotoViewObj>();
        
        var pageAmount = Mathf.CeilToInt((float)dataList.Count / containNum );
        for (var nowPageNum = 0; nowPageNum < pageAmount; nowPageNum++)
        {
            
            // add new page
            var pageObj = Instantiate(pagePrefab, bookPanel);
            pageObj.name = $"Page {nowPageNum + 1}";
            allPageList.Add(pageObj.GetComponent<RectTransform>());

            // add photo
            for (var i = 0 + containNum * nowPageNum; i < Mathf.Min((nowPageNum + 1) * containNum, dataList.Count); i++)
            {
                var newPhoto = Instantiate(photoPrefab, pageObj.transform);
                var photoObj = new PhotoViewObj(newPhoto); 
                
                photoObj.SetImage(dataList[i].photo);
                photoObj.SetName(dataList[i].fileName);
                photoObj.SetLocalPosition(calcPhotoPosInCanvas(i));
                photoObj.SetClickEvent(Debug.Log);
                photoObj.SetRemoveEvent(PhotoSaveLoadHandler.Instance.RemoveData);
                allPhotoList.Add(photoObj);
            }
        }

        currentPage = 1;
        CurrentViewObjIndex = 0;
        
        if(allPhotoList.Count == 0) choosingPhotoTrans.gameObject.SetActive(false);
        
        SetUIElementsLayer();
    }


    private void ShowCurrentPage(int pageIndex)
    {
        foreach(var page in allPageList) page.gameObject.SetActive(false);
        allPageList[pageIndex].gameObject.SetActive(true);
    }


    public void UpdateView(List<FilePhotoData> dataList)
    {
        updatePageNum(dataList.Count);
        updatePhoto(dataList);
        SetUIElementsLayer();
    }


    private void updatePageNum(int allDataCount)
    {
        var pageAmount = Mathf.CeilToInt((float)allDataCount / containNum );
        
        if (pageAmount > allPageList.Count)
        {
            for (var i = allPageList.Count; i < pageAmount; i++)
            {
                var newPage = Instantiate(pagePrefab, bookPanel);
                newPage.name = $"Page {i + 1}";
                allPageList.Add(newPage.GetComponent<RectTransform>());
            }
        }
        else if (pageAmount < allPageList.Count)
        {
            for (var i = allPageList.Count - 1; i >= pageAmount; i--)
            {
                var removePage = allPageList[i];
                allPageList.RemoveAt(i);
                Destroy(removePage.gameObject);
            }
        }
    }


    private void updatePhoto(List<FilePhotoData> newDataList)
    {
        // add new photo
        if (newDataList.Count >= allPhotoList.Count)
        {
            for (var i = 0; i < newDataList.Count; i++)
            {
                if (i >= allPhotoList.Count)
                {
                    var newPhoto = Instantiate(photoPrefab, allPageList[i / containNum].transform);
                    allPhotoList.Add(new PhotoViewObj(newPhoto));
                }
                allPhotoList[i].SetImage(newDataList[i].photo); 
                allPhotoList[i].SetName(newDataList[i].fileName);
                allPhotoList[i].SetLocalPosition(calcPhotoPosInCanvas(i));
                allPhotoList[i].SetRemoveEvent(PhotoSaveLoadHandler.Instance.RemoveData);
                allPhotoList[i].SetClickEvent(Debug.Log);
            }
        }
        // remove old photo
        else
        {
            foreach (var (photoViewObj, i) in allPhotoList.Select( (value, index) => (value, index) ).ToList())
            {
                if (i < newDataList.Count)
                {
                    photoViewObj.SetImage(newDataList[i].photo);
                    photoViewObj.SetName(newDataList[i].fileName);
                    photoViewObj.SetLocalPosition(calcPhotoPosInCanvas(i));
                    photoViewObj.SetRemoveEvent(PhotoSaveLoadHandler.Instance.RemoveData);
                    photoViewObj.SetClickEvent(Debug.Log);
                }
                else
                {
                    allPhotoList.Remove(photoViewObj);
                    Destroy(photoViewObj.PhotoRectTransform.gameObject);
                }
            }
        }
        
        CurrentViewObjIndex = allPageList.Count - 1;
        if(allPhotoList.Count == 0) choosingPhotoTrans.gameObject.SetActive(false);
    }


    public void SetCurrentChoosePhoto(bool up, bool down, bool left, bool right)
    {
        if (left) CurrentViewObjIndex--;
        if(right) CurrentViewObjIndex++;
        if (down) CurrentViewObjIndex += photoNumInRow;
        if(up) CurrentViewObjIndex -= photoNumInRow;
    }

    public void ChangePage(int addPage)
    {
        CurrentViewObjIndex += containNum * addPage;
    }


    private Vector2 calcPhotoPosInCanvas(int index)
    {
        var indexInPage = index % containNum;
        var selfXPos = ( -(float) (photoNumInRow - 1) / 2 + indexInPage % photoNumInRow ) * onePhotoWidth;
        var selfYPos = ( (float) (photoNumInColumn - 1) / 2 - (int) (indexInPage / photoNumInRow) ) * onePhotoHeight;
        return new Vector2(selfXPos, selfYPos);
    }


    private void SetUIElementsLayer()
    {
        choosingPhotoTrans.SetAsLastSibling();
        LastPageBtn.transform.SetAsLastSibling();
        NextPageBtn.transform.SetAsLastSibling();
        closeAlbumBtn.transform.SetAsLastSibling();
    }
}
