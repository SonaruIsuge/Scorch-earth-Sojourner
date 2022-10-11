
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AlbumBookView : MonoBehaviour
{
    
    [SerializeField] private RectTransform bookPanel;
    [SerializeField] private RectTransform photoInCanvas;

    public float PhotoWidth;
    public float PhotoHeight;

    public float XSpacing;
    public float YSpacing;

    private float onePhotoWidth => photoInCanvas.rect.width + XSpacing;
    private float onePhotoHeight => photoInCanvas.rect.height + YSpacing;

    private int photoNumInRow => (int) ( (bookPanel.rect.width - PhotoWidth) / onePhotoWidth) + 1;
    private int photoNumInColumn => (int) ( (bookPanel.rect.height - PhotoHeight) / onePhotoHeight) + 1;
    private int containNum => photoNumInRow * photoNumInColumn;

    private float pageWidth => photoNumInRow * onePhotoWidth - XSpacing;
    private float pageHeight => photoNumInColumn * onePhotoHeight - YSpacing;
    
    
    public void OpenAlbum()
    {
        bookPanel.gameObject.SetActive(true);
    }


    public void CloseAlbum()
    {
        bookPanel.gameObject.SetActive(false);
    }


    public void InitPhoto(List<FilePhotoData> dataList)
    {
        var pageNum = Mathf.CeilToInt((float)dataList.Count / containNum );
        for (var nowPageNum = 0; nowPageNum < pageNum; nowPageNum++)
        {
            
            // add new page
            var pageObj = Instantiate(new GameObject($"Page {nowPageNum + 1}", typeof(RectTransform)), bookPanel);

            // add photo
            for (var i = 0 + containNum * nowPageNum; i < dataList.Count; i++)
            {
                var newPhoto = Instantiate(photoInCanvas, pageObj.transform);
                newPhoto.GetComponent<RawImage>().texture = dataList[i].photo;
                newPhoto.GetComponentInChildren<TMP_Text>().text = dataList[i].fileName;

                newPhoto.localPosition = new Vector3(-onePhotoWidth + (i % photoNumInRow) * onePhotoWidth,
                    onePhotoHeight - (i / photoNumInRow) * onePhotoHeight);
            }
        }
    }
}
