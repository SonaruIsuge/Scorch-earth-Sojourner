using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class MemoryCamera : MonoBehaviour, IPlayerProp
{
    public Player player { get; private set; }
    
    [SerializeField] private int photoWidth;
    [SerializeField] private int photoHeight;
    public int PhotoWidth => photoWidth;
    public int PhotoHeight => photoHeight;
    
    public RectTransform PhotoFrameRectTrans;
    public RawImage PhotoDisplayArea;
    [SerializeField]private RectTransform photoFrame;
    [SerializeField] private Light2D cameraFlashLight;

    public string FileStorageFolder = "/SaveData/";
    
    private Rect frameRect => PhotoFrameRectTrans.rect;
    private Texture2D temporaryPhoto;
    private Camera cc => Camera.main;
    

    private PhotoTakeFeature photoTakeFeature;
    private CameraMoveFeature cameraMoveFeature;
    //private PhotoSaveLoadFeature photoSaveLoadFeature;
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
        //photoSaveLoadFeature = new PhotoSaveLoadFeature(this);
        itemDetectFeature = new ItemDetectFeature(this);
        
        PhotoFrameRectTrans.gameObject.SetActive(false);
    }


    public void UnEquip()
    {
        player = null;
        photoTakeFeature = null;
        cameraMoveFeature = null;
        itemDetectFeature = null;
    }


    public void EnableProp(bool enable)
    {
        enabled = enable;
        PhotoFrameRectTrans.gameObject.SetActive(enable);
    }


    public async void TakePhoto()
    {
        temporaryPhoto = await photoTakeFeature.TakePhoto();
        await CameraFlash();
        var item = itemDetectFeature.DetectItem();
        
        PhotoSaveLoadHandler.Instance.SavePhoto(temporaryPhoto, item);
    }
    

    public void MoveCamera(float x, float y) => cameraMoveFeature.MoveFrame(x, y);
    

    public void GetWidthHeightInWorld(out float width, out float height)
    {
        var rightTop = new Vector3((float)photoWidth / 2, (float)photoHeight / 2, 0);
        var leftBottom = new Vector3(-(float)photoWidth / 2, -(float)photoHeight / 2, 0);
        var worldRightTop = cc.ScreenToWorldPoint(rightTop);
        var worldLeftBottom = cc.ScreenToWorldPoint(leftBottom);
        width = (worldRightTop - worldLeftBottom).x;
        height = (worldRightTop - worldLeftBottom).y;
    }

    
    private async UniTask CameraFlash()
    {
        cameraFlashLight.intensity = 20;
        await UniTask.Delay(30);
        cameraFlashLight.intensity = 0;
    }
}
