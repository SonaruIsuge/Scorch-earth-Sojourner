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
    
    // Need component
    public Camera WorldCamera => Camera.main;
    public Camera memoryCameraLens { get; private set; }
    [field: SerializeField] public RenderTexture memoryCameraFilm { get; private set; }
    private Light2D cameraFlashLight;
    
    // set properties
    [SerializeField] private int photoWidth;
    [SerializeField] private int photoHeight;
    public int PhotoWidth => photoWidth;
    public int PhotoHeight => photoHeight;

    public string FileStorageFolder = "/SaveData/";
    
    private Texture2D temporaryPhoto;
    private Tweener cameraFlashTween;
    
    // Add features
    private PhotoTakeFeature photoTakeFeature;
    private CameraMoveFeature cameraMoveFeature;
    private ItemDetectFeature itemDetectFeature;
    
    // Events
    public event Action<bool> OnPhotoFrameToggleEnable;
    public event Action<float, float> OnPhotoFrameMove;
    public event Action<Collider2D> OnRecordableDetect;
    public event Action OnPhotoTake;
    public event Action<Texture, ItemPhotoData> OnRecordableItemPhotoTake;
    

    public void Equip(Player newPlayer)
    {
        // Get component
        player = newPlayer;
        memoryCameraLens = GetComponentInChildren<Camera>();
        cameraFlashLight = GetComponentInChildren<Light2D>();
        
        // Set component data
        memoryCameraFilm.width = photoWidth;
        memoryCameraFilm.height = photoHeight;
        memoryCameraLens.orthographicSize = photoHeight * WorldCamera.orthographicSize / Screen.height;

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
        
        OnPhotoFrameToggleEnable = null;
        OnPhotoFrameMove = null;
        OnRecordableDetect = null;
        OnPhotoTake = null;
    }


    public void EnableProp(bool enable)
    {
        enabled = enable;
        OnPhotoFrameToggleEnable?.Invoke(enable);
    }


    public async void TakePhoto()
    {
        temporaryPhoto = await photoTakeFeature.TakePhoto();
        OnPhotoTake?.Invoke();
        
        DOCameraFlash(20, 0.25f);
        var item = itemDetectFeature.DetectItem();
        
        PhotoSaveLoadHandler.Instance.SavePhoto(temporaryPhoto, item);
        if (item != null) OnRecordableItemPhotoTake?.Invoke(temporaryPhoto, item);
    }
    

    public void MoveCamera(float x, float y)
    {
        x = Mathf.Clamp(x, photoWidth / 2.0f, Screen.width - photoWidth / 2.0f);
        y = Mathf.Clamp(y, photoHeight / 2.0f, Screen.height - photoHeight / 2.0f);
        
        cameraMoveFeature.MoveFrame(x, y);
        OnPhotoFrameMove?.Invoke(x, y);
        
        itemDetectFeature.TryDetectItem();
        OnRecordableDetect?.Invoke(itemDetectFeature.currentDetectItem);
    }


    private void DOCameraFlash(float intensity, float during)
    {
        cameraFlashTween?.Kill();
        cameraFlashTween = DOTween.To(()=> cameraFlashLight.intensity, x=> cameraFlashLight.intensity = x, intensity, during);
        cameraFlashTween.onComplete += () => DOTween.To(()=> cameraFlashLight.intensity, x=> cameraFlashLight.intensity = x, 0, during);
        cameraFlashTween.Play();
    }
}
