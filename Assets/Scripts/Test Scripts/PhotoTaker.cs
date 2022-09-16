using System;
using System.Collections;
using System.Collections.Generic;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PhotoTaker : MonoBehaviour
{
    public int PhotoWidth;
    public int PhotoHeight;
    
    public RectTransform PhotoFrameRectTrans;
    public Image PhotoDisplayArea;
    
    private Rect frameRect => PhotoFrameRectTrans.rect;
    private Texture2D screenCapture;

    private void Awake()
    {
        screenCapture = new Texture2D(PhotoWidth, PhotoHeight, TextureFormat.RGB24, false);

        PhotoFrameRectTrans.sizeDelta = new Vector2(PhotoWidth, PhotoHeight);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(CapturePhoto());
        }        
    }

    private IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        
        //rect.x -> distance from left , rect.y -> distance from top
        var rect = UnityTool.RectTransformToScreenSpace(PhotoFrameRectTrans);

        var bottomBound = Screen.height - frameRect.height - rect.y;
        rect.y = bottomBound;
        screenCapture.ReadPixels(rect, 0,0,false);
        screenCapture.Apply();
        
        showPhoto();
    }

    private void showPhoto()
    {
        var photoSprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        PhotoDisplayArea.sprite = photoSprite;
    }
}
