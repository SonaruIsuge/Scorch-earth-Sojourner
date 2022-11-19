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

    private IMaterialSwitcher materialSwitcher;
    
    private Camera worldCamera => Camera.main;
    
    // projector parts
    private Transform projectPoint;
    private RectTransform projectCanvas; 
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer projectorPointRenderer;
    private CanvasGroup imageGroup;
    private RawImage projectorCanvasMask;
    private RawImage projectImage;
    private Light2D projectLight;
    
    // projector data
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
        materialSwitcher = GetComponent<IMaterialSwitcher>();
            
        projectPoint = transform.Find("Project Point").transform;
        projectCanvas = projectPoint.Find("Project Screen").GetComponent<RectTransform>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectorPointRenderer = projectPoint.GetComponent<SpriteRenderer>();
        projectLight = projectPoint.GetComponentInChildren<Light2D>();
        imageGroup = projectCanvas.GetComponent<CanvasGroup>();
        projectorCanvasMask = projectCanvas.GetComponentInChildren<RawImage>();
        projectImage = projectorCanvasMask.transform.Find("Projector Image").GetComponent<RawImage>();
        
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
        materialSwitcher.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        isInteract = true;
        
        interactPlayer = player;
        allPlayerPhoto = player.AlbumBook.GetAllPhotoData();
        photoData = null;
        resizeCameraOrthographicSize = player.MemoryCamera.WorldCamera.orthographicSize * 2 * 100 / Screen.height;

        DOProjectorAnimation(2.0f, 0.7f, 1);

        choosePhotoIndex = Mathf.Max(Mathf.Min(choosePhotoIndex, allPlayerPhoto.Count - 1), 0);
        var startPhoto = allPlayerPhoto.Count > 0 ? allPlayerPhoto[choosePhotoIndex].photo : null;
        player.DelayDo(() => OnStartInteract?.Invoke(startPhoto), 1);
    }

    public void OnDeselect()
    {
        isSelect = false;
        interactPlayer = null;
        materialSwitcher.ResetMaterial();
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
        var itemPos = targetPhotoData.PositionFromCenter * itemScale + projectCanvas.anchoredPosition;
        itemObj.transform.localScale = Vector3.one * itemScale;
        itemObj.transform.localPosition = itemPos;
        return itemObj.GetComponent<CameraRecordableBehaviour>();
    }
    
    
    private void DOProjectorAnimation(float lightIntensity, float groupAlpha, float during)
    {
        var imageColor = projectImage.color;
        imageColor.a = groupAlpha;
        lightIntensityTween?.Kill();
        imageColorTween?.Kill();
        
        lightIntensityTween = DOTween.To(()=> projectLight.intensity, x=> projectLight.intensity = x, lightIntensity, during);
        imageColorTween = DOTween.To(() => imageGroup.alpha, x => imageGroup.alpha = x, groupAlpha, during);
        
        lightIntensityTween.Play();
        imageColorTween.Play();
    }


    private void InitProjectorScreen()
    {
        projectCanvas.sizeDelta = new Vector2(projectScale.x, projectScale.y);
        projectLight.pointLightOuterRadius = Mathf.Abs(projectCanvas.anchoredPosition.y) + projectScale.y / 2;
        projectLight.pointLightOuterAngle = 2 * Mathf.Atan((projectScale.x / 2) / Mathf.Abs(projectCanvas.anchoredPosition.y) )  * Mathf.Rad2Deg;
    }
}
