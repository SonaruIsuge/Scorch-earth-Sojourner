using System;
using System.Collections;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPhotoTaker : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private int photoWidth;
    [SerializeField] private int photoHeight;
    
    public RectTransform PhotoFrameRectTrans;
    public Image PhotoDisplayArea;
    
    private Rect frameRect => PhotoFrameRectTrans.rect;
    private Texture2D screenCapture;

    private bool underCaptureProgress;
    
    
    private void Awake()
    {
        player = GetComponent<Player>();
        
        screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
        PhotoFrameRectTrans.sizeDelta = new Vector2(photoWidth, photoHeight);

        underCaptureProgress = false;
    }

    public void TakePhoto()
    {
        if(!underCaptureProgress) StartCoroutine(CaptureScreenShot());
    }

    public void MoveFrame(float x, float y)
    {
        x = Mathf.Clamp(x, PhotoFrameRectTrans.rect.width / 2, Screen.width - PhotoFrameRectTrans.rect.width / 2);
        y = Mathf.Clamp(y, PhotoFrameRectTrans.rect.height / 2, Screen.height - PhotoFrameRectTrans.rect.height / 2);
        PhotoFrameRectTrans.position = new Vector2(x, y);
    }
    
    private IEnumerator CaptureScreenShot()
    {
        underCaptureProgress = true;
        yield return new WaitForEndOfFrame();
        
        //rect.x -> distance from left , rect.y -> distance from top
        var rect = UnityTool.RectTransformToScreenSpace(PhotoFrameRectTrans);

        var bottomBound = Screen.height - frameRect.height - rect.y;
        rect.y = bottomBound;
        screenCapture.ReadPixels(rect, 0,0,false);
        screenCapture.Apply();
        underCaptureProgress = false;
        
        showPhoto();
    }

    private void showPhoto()
    {
        var photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        PhotoDisplayArea.sprite = photoSprite;
    }
}
