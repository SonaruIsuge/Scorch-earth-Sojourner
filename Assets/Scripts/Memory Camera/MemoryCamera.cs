using System;
using Cysharp.Threading.Tasks;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MemoryCamera : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private int photoWidth;
    [SerializeField] private int photoHeight;
    public int PhotoWidth => photoWidth;
    public int PhotoHeight => photoHeight;
    
    public RectTransform PhotoFrameRectTrans;
    public Image PhotoDisplayArea;
    [SerializeField]private RectTransform photoFrame;

    public string FilePath = "/SaveData/";
    
    private Rect frameRect => PhotoFrameRectTrans.rect;
    private Texture2D temporaryPhoto;
    private Camera cc => Camera.main;
    

    private PhotoTakeFeature photoTakeFeature;
    private CameraMoveFeature cameraMoveFeature;
    private PhotoSaveLoadFeature photoSaveLoadFeature;
    private ItemDetectFeature itemDetectFeature;
    

    public void Equip(Player newPlayer)
    {
        player = newPlayer;
        //ScreenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
        PhotoFrameRectTrans.sizeDelta = new Vector2(photoWidth, photoHeight);
        photoFrame.sizeDelta = new Vector2(photoWidth + 20, photoHeight + 20);

        //add feature
        photoTakeFeature = new PhotoTakeFeature(this);
        cameraMoveFeature = new CameraMoveFeature(this);
        photoSaveLoadFeature = new PhotoSaveLoadFeature(this);
        itemDetectFeature = new ItemDetectFeature(this);
    }
    
    
    public async void TakePhoto()
    {
        temporaryPhoto = await photoTakeFeature.TakePhoto();
        var item = itemDetectFeature.DetectItem();
        // photoSaveFeature.SavePhoto(temporaryPhoto);
        
        if(item != null) photoSaveLoadFeature.SavePhotoWithData(temporaryPhoto, (ItemPhotoData)item);
    }

    public void MoveCamera(float x, float y) => cameraMoveFeature.MoveFrame(x, y);


    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("P pressed");
            photoSaveLoadFeature.LoadPhoto(out var data, out var photo);
            var photoSprite = Sprite.Create(photo, new Rect(0, 0, photoWidth, photoHeight), new Vector2(0.5f, 0.5f), 100.0f);
            PhotoDisplayArea.sprite = photoSprite;
            
            Debug.Log(data.TargetItemId + ", " + data.PositionInPhoto);
        }
    }


    public void GetWidthHeightInWorld(out float width, out float height)
    {
        var rightTop = new Vector3((float)photoWidth / 2, (float)photoHeight / 2, 0);
        var leftBottom = new Vector3(-(float)photoWidth / 2, -(float)photoHeight / 2, 0);
        var worldRightTop = cc.ScreenToWorldPoint(rightTop);
        var worldLeftBottom = cc.ScreenToWorldPoint(leftBottom);
        width = (worldRightTop - worldLeftBottom).x;
        height = (worldRightTop - worldLeftBottom).y;
    }
}
