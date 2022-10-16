
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AlbumBookView : MonoBehaviour
{
    public Button albumBtn;
    public Button closeAlbumBtn;
    public RectTransform bookPanel;
    public RectTransform pagePrefab;
    public RectTransform photoPrefab;


    public float PhotoWidth;
    public float PhotoHeight;

    public float XSpacing;
    public float YSpacing;

    private float onePhotoWidth => photoPrefab.rect.width + XSpacing;
    private float onePhotoHeight => photoPrefab.rect.height + YSpacing;

    private int photoNumInRow => (int) ( (bookPanel.rect.width - PhotoWidth) / onePhotoWidth) + 1;
    private int photoNumInColumn => (int) ( (bookPanel.rect.height - PhotoHeight) / onePhotoHeight) + 1;
    private int containNum => photoNumInRow * photoNumInColumn;

    private float pageWidth => photoNumInRow * onePhotoWidth - XSpacing;
    private float pageHeight => photoNumInColumn * onePhotoHeight - YSpacing;

    [SerializeField] private List<RectTransform> allPageList;
    [SerializeField] private List<PhotoViewObj> allPhotoList;


    public void EnableView(bool enable)
    {
        bookPanel.gameObject.SetActive(enable);
    }


    public void InitPhoto(List<FilePhotoData> dataList)
    {
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
                allPhotoList.Add(photoObj);
            }
        }
    }


    public void UpdateView(List<FilePhotoData> dataList)
    {
        updatePageNum(dataList.Count);
        updatePhoto(dataList);
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


    private void updatePhoto(List<FilePhotoData> dataList)
    {
        var moreNewData = dataList.Count >= allPhotoList.Count;
        
        if (moreNewData)                    // new data  more than / equal with old data
        {
            for (var i = 0; i < dataList.Count; i++)
            {
                if (i >= allPhotoList.Count)
                {
                    var newPhoto = Instantiate(photoPrefab, allPageList[i / containNum].transform);
                    allPhotoList.Add(new PhotoViewObj(newPhoto));
                }
                allPhotoList[i].SetImage(dataList[i].photo);
                allPhotoList[i].SetName(dataList[i].fileName);
                allPhotoList[i].SetLocalPosition(calcPhotoPosInCanvas(i));
            }
        }
        else                               //new data less than old data
        {
            foreach (var (photoViewObj, i) in allPhotoList.Select((value, index) => (value, index)))
            {
                if (i < allPhotoList.Count)
                {
                    photoViewObj.SetImage(dataList[i].photo);
                    photoViewObj.SetName(dataList[i].fileName);
                    allPhotoList[i].SetLocalPosition(calcPhotoPosInCanvas(i));
                }
                else
                {
                    allPhotoList.Remove(photoViewObj);
                }
            }
        }
    }


    private Vector2 calcPhotoPosInCanvas(int index)
    {
        var indexInPage = index % containNum;
        
        var selfXPos = ( -(float) (photoNumInRow - 1) / 2 + indexInPage % photoNumInRow ) * onePhotoWidth;
        var selfYPos = ( (float) (photoNumInColumn - 1) / 2 - (int) (indexInPage / photoNumInRow) ) * onePhotoHeight;

        return new Vector2(selfXPos, selfYPos);
    }
}
