using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProjectorUI : MonoBehaviour
{
    //private Player player;
    private M_ProjectMachine projector;
    //private GeneralUI generalUI;

    private Dictionary<string, Texture2D> allDisplayPhotoDict;

    [SerializeField] private RectTransform projectorUI;
    [SerializeField] private RawImage CurrentPhotoDisplayArea;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private Button LastPhotoBtn;
    [SerializeField] private Button NextPhotoBtn;
    [SerializeField] private Button SubmitPhotoBtn;
    [SerializeField] private Button ExitProjectorBtn;

    private int currentPhotoIndex;

    public event Action<bool> OnEnableUI;
    
    private void Awake()
    {
        //player = FindObjectOfType<Player>();
        //generalUI = GetComponent<GeneralUI>();
    }
    

    private void OnEnable()
    {
        LastPhotoBtn.onClick.AddListener(OnLeftButtonClick);
        NextPhotoBtn.onClick.AddListener(OnRightButtonClick);
        SubmitPhotoBtn.onClick.AddListener(OnSubmitButtonClick);
        ExitProjectorBtn.onClick.AddListener(OnCancelButtonClick);
    }


    private void OnDisable()
    {
        LastPhotoBtn.onClick.RemoveAllListeners();
        NextPhotoBtn.onClick.RemoveAllListeners();
        SubmitPhotoBtn.onClick.RemoveAllListeners();
        ExitProjectorBtn.onClick.RemoveAllListeners();
    }


    private void Start()
    {
        //player.InteractHandler.OnItemInteract += ChangeTargetProjector;
    }
    


    public void ChangeTargetProjector(IInteractable interactItem)
    {
        if(interactItem is not M_ProjectMachine newProjector) return;

        if(projector) UnregisterProjector();
        projector = newProjector;
        RegisterProjector();
    }


    private void RegisterProjector()
    {
        projector.OnStartInteract += OpenProjectorUI;
        projector.OnEndInteract += CloseProjectorUI;
        projector.OnChangeChoosePhoto += ChangeDisplayPhoto;
        
    }


    private void UnregisterProjector()
    {
        projector.OnStartInteract -= OpenProjectorUI;
        projector.OnEndInteract -= CloseProjectorUI;
        projector.OnChangeChoosePhoto -= ChangeDisplayPhoto;
    }


    private void OpenProjectorUI(FilePhotoData firstData)
    {
        projectorUI.gameObject.SetActive(true);
        CurrentPhotoDisplayArea.texture = firstData?.photo;
        
        var textureColor = CurrentPhotoDisplayArea.color;
        textureColor.a = firstData == null ? 0 : 1;
        CurrentPhotoDisplayArea.color = textureColor;
        
        Description.text = firstData?.data == null ? "沒有描述" : ItemControlHandler.Instance.GetRecordableItemById(firstData.data.TargetItemId).Description;
        
        //generalUI.EnableGeneralUI(false);
        OnEnableUI?.Invoke(true);
    }


    private void CloseProjectorUI()
    {
        projectorUI.gameObject.SetActive(false);
        //generalUI.EnableGeneralUI(true);
        OnEnableUI?.Invoke(false);
    }
    

    private void ChangeDisplayPhoto(FilePhotoData data)
    {
        CurrentPhotoDisplayArea.texture = data?.photo;
        
        var textureColor = CurrentPhotoDisplayArea.color;
        textureColor.a = data == null ? 0 : 1;
        CurrentPhotoDisplayArea.color = textureColor;

        Description.text = data?.data == null ? "沒有描述" : ItemControlHandler.Instance.GetRecordableItemById(data.data.TargetItemId).Description;
    }
    
    
    // Button Click Function
    private void OnLeftButtonClick()
    {
        if (projector) projector.ChangeCurrentPhoto(-1);
    }

    private void OnRightButtonClick()
    {
        if (projector) projector.ChangeCurrentPhoto(1);   
    }

    private void OnSubmitButtonClick()
    {
        if(projector) projector.SubmitPhoto();
    }

    private void OnCancelButtonClick()
    {
        if(projector) projector.CancelChoosePhoto();
    }
}
