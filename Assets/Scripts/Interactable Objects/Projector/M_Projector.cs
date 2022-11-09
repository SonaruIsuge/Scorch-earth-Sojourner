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
    private Player interactPlayer;
    private List<FilePhotoData> allPlayerPhoto;
    
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }
    
    private Camera worldCamera => Camera.main;
    private SpriteRenderer spriteRenderer;    private Transform projectPoint;
    private RectTransform projectCanvas;
    private RawImage projectImage;
    private Light2D projectLight;
    
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 projectScale;
    [field:SerializeField] public string interactHint { get; private set; }
    
    [SerializeField] private ItemPhotoData photoData;
    [SerializeField] private int choosePhotoIndex;
    [SerializeField] private CameraRecordableBehaviour currentProjectItem;
    
    private SimpleTimer projectObjTimer;
    private Tweener lightIntensityTween, imageColorTween;
    private readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    private float resizeCameraOrthographicSize;

    public event Action<Texture> OnStartInteract;
    public event Action OnEndInteract;
    public event Action<Texture> OnChangeChoosePhoto;
    public event Action OnSubmitPhoto;
    public event Action OnExitProjector;
    
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectPoint = transform.Find("Project Point").transform;
        projectLight = projectPoint.GetComponentInChildren<Light2D>();
        projectCanvas = projectPoint.Find("Project Screen").GetComponent<RectTransform>();
        projectImage = projectCanvas.GetComponentInChildren<RawImage>();
        
        InitProjectorScreen();
        
        photoData = null;
        choosePhotoIndex = 0;
        currentProjectItem = null;
        projectObjTimer = new SimpleTimer(2);
        projectObjTimer.Pause();
    }


    public void OnSelect()
    {
        isSelect = true;
        spriteRenderer.material.SetFloat(OutlineThickness, 1.0f);
    }

    public void Interact(Player player)
    {
        isInteract = true;
        
        interactPlayer = player;
        allPlayerPhoto = player.AlbumBook.GetAllPhotoData();
        photoData = null;
        resizeCameraOrthographicSize = player.MemoryCamera.WorldCamera.orthographicSize * 2 * 100 / Screen.height;

        DOProjectorAnimation(0.8f, 0.7f, 1);

        choosePhotoIndex = Mathf.Max(Mathf.Min(choosePhotoIndex, allPlayerPhoto.Count - 1), 0);
        var startPhoto = allPlayerPhoto.Count > 0 ? allPlayerPhoto[choosePhotoIndex].photo : null;
        player.DelayDo(() => OnStartInteract?.Invoke(startPhoto), 1);
    }

    public void OnDeselect()
    {
        isSelect = false;
        interactPlayer = null;
        spriteRenderer.material.SetFloat(OutlineThickness, 0);
    }


    public void SetProjectPhoto(Texture2D photo, ItemPhotoData data = null)
    {
        projectImage.texture = photo;
        photoData = data;
    }


    public void ChangeCurrentPhoto(int addIndex)
    {
        choosePhotoIndex += addIndex;
        choosePhotoIndex = Mathf.Max(Mathf.Min(choosePhotoIndex, allPlayerPhoto.Count - 1), 0);
        if (allPlayerPhoto.Count > 0) OnChangeChoosePhoto?.Invoke(allPlayerPhoto[choosePhotoIndex].photo);
    }


    public void SubmitPhoto()
    {
        if (allPlayerPhoto.Count <= 0)
        {
            photoData = null;
            DealWithExitProgress();
            return;
        }
        
        photoData = allPlayerPhoto[choosePhotoIndex].data;
        SetProjectPhoto(allPlayerPhoto[choosePhotoIndex].photo, allPlayerPhoto[choosePhotoIndex].data);
        OnSubmitPhoto?.Invoke();
        DealWithExitProgress();
    }


    public void CancelChoosePhoto()
    {
        photoData = null;
        OnExitProjector?.Invoke();
        DealWithExitProgress();
    }


    private void DealWithExitProgress()
    {
        isInteract = false;
        if (photoData == null)
        {
            DOProjectorAnimation(0, 0, 1);
        }
        else
        {
            interactPlayer.DelayDo(() =>
            {
                if(currentProjectItem) Destroy(currentProjectItem.gameObject);
                currentProjectItem = InstantiateItem(photoData);
                currentProjectItem.ItemUse();
                
                // Fade projector screen
                DOProjectorAnimation(0, 0, 1);
            }, 2);
        }
        
        OnEndInteract.Invoke();
    }


    private CameraRecordableBehaviour InstantiateItem(ItemPhotoData targetPhotoData)
    {
        var generateItem = ItemControlHandler.Instance.GetRecordableItemById(targetPhotoData.TargetItemId);
        var itemObj = Instantiate(generateItem.ItemObject, projectPoint);
        
        var OrthoTimes = worldCamera.orthographicSize / targetPhotoData.cameraOrthoSize;
        var itemScale = projectScale.x * 100 / (projectImage.texture.width * resizeCameraOrthographicSize) * OrthoTimes;
        var itemPos = targetPhotoData.PositionFromCenter * itemScale + offset;
        
        itemObj.transform.localScale = Vector3.one * itemScale;
        itemObj.transform.localPosition = itemPos;
        return itemObj.GetComponent<CameraRecordableBehaviour>();
    }
    
    
    private void DOProjectorAnimation(float lightIntensity, float imageColorAlpha, float during)
    {
        var imageColor = projectImage.color;
        imageColor.a = imageColorAlpha;
        lightIntensityTween?.Kill();
        imageColorTween?.Kill();
        
        lightIntensityTween = DOTween.To(()=> projectLight.intensity, x=> projectLight.intensity = x, lightIntensity, during);
        imageColorTween = projectImage.DOColor(imageColor, during);
            
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
