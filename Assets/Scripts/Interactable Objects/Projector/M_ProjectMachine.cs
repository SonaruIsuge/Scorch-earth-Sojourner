using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SonaruUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class M_ProjectMachine : MonoBehaviour, IInteractable
{
    private Player interactPlayer;
    private List<FilePhotoData> allPlayerPhoto;
    
    public bool isInteract { get; private set; }
    public bool isSelect { get; private set; }

    private IMaterialSwitcher materialSwitcher;

    [SerializeField] private ProjectorDrone correspondProjector;
    
    private Camera worldCamera => Camera.main;

    // projector data
    [field:SerializeField] public string interactHint { get; private set; }
    
    [SerializeField] private Vector2 projectScale;

    [SerializeField] private ItemPhotoData photoData;
    [SerializeField] private int choosePhotoIndex;
    [SerializeField] private CameraRecordableBehaviour currentProjectItem;
    
    private SimpleTimer projectObjTimer;
    //private Tweener lightIntensityTween, imageColorTween;
    private float resizeCameraOrthographicSize;

    public event Action<Texture> OnStartInteract;
    public event Action OnEndInteract;
    public event Action<Texture> OnChangeChoosePhoto;
    public event Action OnSubmitPhoto;
    public event Action OnExitProjector;
    
    
    private void Awake()
    {
        materialSwitcher = GetComponent<IMaterialSwitcher>();

        photoData = null;
        choosePhotoIndex = 0;
        currentProjectItem = null;
        projectObjTimer = new SimpleTimer(2);
        projectObjTimer.Pause();
    }


    private void Start()
    {
        if(correspondProjector) correspondProjector.SetProjectorComponent(projectScale);
    }


    public void OnSelect()
    {
        isSelect = true;
        materialSwitcher?.ReplaceMaterial();
    }

    public void Interact(Player player)
    {
        if(!correspondProjector) return;
        
        isInteract = true;
        interactPlayer = player;
        allPlayerPhoto = player.AlbumBook.GetAllPhotoData();
        photoData = null;
        resizeCameraOrthographicSize = worldCamera.orthographicSize * 2 * 100 / Screen.height;

        correspondProjector.DOProjectorAnimation(2.0f, 0.7f, 1);

        choosePhotoIndex = Mathf.Max(Mathf.Min(choosePhotoIndex, allPlayerPhoto.Count - 1), 0);
        var startPhoto = allPlayerPhoto.Count > 0 ? allPlayerPhoto[choosePhotoIndex].photo : null;
        player.DelayDo(() => OnStartInteract?.Invoke(startPhoto), 1);
    }

    public void OnDeselect()
    {
        isSelect = false;
        interactPlayer = null;
        materialSwitcher?.ResetMaterial();
    }


    private void SetProjectPhoto(Texture2D photo, ItemPhotoData data = null)
    {
        correspondProjector.SetProjectImage(photo);
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
            correspondProjector.DOProjectorAnimation(0, 0, 1);
        }
        else
        {
            interactPlayer.DelayDo(() =>
            {
                if(currentProjectItem) Destroy(currentProjectItem.gameObject);
                currentProjectItem = InstantiateItem(photoData);
                currentProjectItem.ItemUse(this);
                
                // Fade projector screen
                correspondProjector.DOProjectorAnimation(0, 0, 1);
            }, 2);
        }
        
        OnEndInteract.Invoke();
    }


    private CameraRecordableBehaviour InstantiateItem(ItemPhotoData targetPhotoData)
    {
        var generateItem = ItemControlHandler.Instance.GetRecordableItemById(targetPhotoData.TargetItemId);
        var itemObj = Instantiate(generateItem.ItemObject);

        var OrthoTimes = worldCamera.orthographicSize / targetPhotoData.cameraOrthoSize;
        var itemScale = projectScale.x * 100 / (allPlayerPhoto[choosePhotoIndex].photo.width * resizeCameraOrthographicSize) * OrthoTimes;
        var itemPos = targetPhotoData.PositionFromCenter * itemScale + correspondProjector.GetProjectorCanvasPos();
        
        itemObj.transform.localScale = Vector3.one * itemScale;
        itemObj.transform.position = itemPos;
        return itemObj.GetComponent<CameraRecordableBehaviour>();
    }
}
