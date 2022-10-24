using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class M_Projector : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;

    private Player interactPlayer;
    
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    private readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");

    private Transform projectPoint;
    private RectTransform projectCanvas;
    private RawImage projectImage;
    private Light2D projectLight;
    
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 projectScale;

    private Tweener lightIntensityTween, imageColorTween;
    [field:SerializeField] public string interactHint { get; private set; }
    
    [SerializeField] private ItemPhotoData photoData;
    private SimpleTimer projectObjTimer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectPoint = transform.Find("Project Point").transform;
        projectLight = projectPoint.GetComponentInChildren<Light2D>();
        projectCanvas = projectPoint.Find("Project Screen").GetComponent<RectTransform>();
        projectImage = projectCanvas.GetComponentInChildren<RawImage>();
        
        InitProjectorScreen();

        photoData = null;
        projectObjTimer = new SimpleTimer(3);
        projectObjTimer.Pause();
    }


    private void Update()
    {
        if (photoData != null && projectObjTimer.IsFinish)
        {
            // Generate recordable item by its id
            var generateItem = ItemControlHandler.Instance.GetRecordableItemById(photoData.TargetItemId);
            var itemObj = Instantiate(generateItem.ItemObject, projectPoint);
            
            var itemPos = new Vector2(photoData.PositionInPhoto.x / 100.0f, photoData.PositionInPhoto.y / 100.0f);
            itemObj.transform.localPosition = itemPos;
            var itemScale = projectScale.x * 100 / projectImage.texture.width;
            itemObj.transform.localScale = Vector3.one * itemScale;
            Debug.Log(itemPos);
            itemObj.GetComponent<CameraRecordableBehaviour>().ItemUse();

            // reset timer and photoData
            projectObjTimer.Reset(3);
            projectObjTimer.Pause();
            //photoData = null;
            
            // Fade projector screen
            DOProjectorAnimation(0, 0, 1);
        }
    }


    public void OnSelect()
    {
        isSelect = true;
        spriteRenderer.material.SetFloat(OutlineThickness, 1.0f);
    }

    public void Interact(Player player)
    {
        interactPlayer = player;
        photoData = null;

        if (player.AlbumBook)
        {
            DOProjectorAnimation(0.8f, 0.7f, 1);
            player.DelayDo(() => player.ChangePropState(UsingProp.AlbumBook, MoreInfo.FromProjector), 1);
            photoData = null;
        }
    }

    public void OnDeselect()
    {
        isSelect = false;
        interactPlayer = null;
        spriteRenderer.material.SetFloat(OutlineThickness, 0);
    }


    public void OnChoosePhotoOver()
    {
        if(photoData == null) DOProjectorAnimation(0, 0, 1);
        else projectObjTimer.Reset(3);
    }
    

    public void SetProjectPhoto(Texture2D photo, ItemPhotoData data = null)
    {
        projectImage.texture = photo;
        photoData = data;
    }
    
    
    private void DOProjectorAnimation(float lightIntensity, float imageColorAlpha, float during)
    {
        var imageColor = projectImage.color;
        lightIntensityTween?.Kill();
        imageColorTween?.Kill();
        
        lightIntensityTween = DOTween.To(()=> projectLight.intensity, x=> projectLight.intensity = x, lightIntensity, during);
        imageColorTween = projectImage.DOColor(new Color(imageColor.r, imageColor.g, imageColor.b, imageColorAlpha), during);
            
        lightIntensityTween.Play();
        imageColorTween.Play();
    }


    private void InitProjectorScreen()
    {
        projectCanvas.sizeDelta = new Vector2(projectScale.x, projectScale.y);
        projectLight.transform.localScale = projectCanvas.sizeDelta;
        projectCanvas.Translate(offset);
        projectLight.transform.Translate(offset);
    }
}
