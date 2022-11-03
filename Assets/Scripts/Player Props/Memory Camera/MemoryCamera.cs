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
    public Camera WorldCamera => Camera.main;
    public RectTransform PhotoTakeOuterFrame;
    public RectTransform PhotoFrameRectTrans;
    private Light2D cameraFlashLight;
    public Camera memoryCameraLens { get; private set; }
    [field: SerializeField] public RenderTexture memoryCameraFilm { get; private set; }
    public RectTransform DetectPoint;
    public Player player { get; private set; }
    
    [SerializeField] private int photoWidth;
    [SerializeField] private int photoHeight;
    public int PhotoWidth => photoWidth;
    public int PhotoHeight => photoHeight;

    public string FileStorageFolder = "/SaveData/";
    
    private Texture2D temporaryPhoto;
    private Tweener cameraFlashTween;

    private PhotoTakeFeature photoTakeFeature;
    private CameraMoveFeature cameraMoveFeature;
    private ItemDetectFeature itemDetectFeature;
    

    public void Equip(Player newPlayer)
    {
        // Get component
        player = newPlayer;
        memoryCameraLens = GetComponentInChildren<Camera>();
        cameraFlashLight = GetComponentInChildren<Light2D>();
        
        // Set component data
        PhotoFrameRectTrans.sizeDelta = new Vector2(photoWidth, photoHeight);
        memoryCameraFilm.width = photoWidth;
        memoryCameraFilm.height = photoHeight;
        memoryCameraLens.orthographicSize = photoHeight * WorldCamera.orthographicSize / Screen.height;
        PhotoTakeOuterFrame.gameObject.SetActive(false);
        PhotoFrameRectTrans.gameObject.SetActive(false);
        DetectPoint.transform.gameObject.SetActive(false);
        
        //add feature
        photoTakeFeature = new PhotoTakeFeature(this);
        cameraMoveFeature = new CameraMoveFeature(this);
        itemDetectFeature = new ItemDetectFeature(this);
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
        PhotoTakeOuterFrame.gameObject.SetActive(enable);
    }


    public async void TakePhoto()
    {
        temporaryPhoto = await photoTakeFeature.TakePhoto();
        DOCameraFlash(20, 0.25f);
        var item = itemDetectFeature.DetectItem();
        
        PhotoSaveLoadHandler.Instance.SavePhoto(temporaryPhoto, item);
    }
    

    public void MoveCamera(float x, float y)
    {
        cameraMoveFeature.MoveFrame(x, y);
        itemDetectFeature.TryDetectItem();
    }


    private void DOCameraFlash(float intensity, float during)
    {
        cameraFlashTween?.Kill();
        cameraFlashTween = DOTween.To(()=> cameraFlashLight.intensity, x=> cameraFlashLight.intensity = x, intensity, during);
        cameraFlashTween.onComplete += () => DOTween.To(()=> cameraFlashLight.intensity, x=> cameraFlashLight.intensity = x, 0, during);
        cameraFlashTween.Play();
    }
}
