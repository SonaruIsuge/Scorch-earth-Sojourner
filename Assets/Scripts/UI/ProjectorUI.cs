using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectorUI : MonoBehaviour
{
    private Player player;
    private M_ProjectMachine projector;

    private Dictionary<string, Texture2D> allDisplayPhotoDict;

    [SerializeField] private RectTransform ProjectorUIFrame;
    [SerializeField] private RawImage CurrentPhotoDisplayArea;
    [SerializeField] private Button LastPhotoBtn;
    [SerializeField] private Button NextPhotoBtn;
    [SerializeField] private Button SubmitPhotoBtn;
    [SerializeField] private Button ExitProjectorBtn;

    private int currentPhotoIndex;
    
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        
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
        player.InteractHandler.OnItemInteract += ChangeTargetProjector;
    }
    


    private void ChangeTargetProjector(IInteractable interactItem)
    {
        if(!(interactItem is M_ProjectMachine)) return;
        
        var newProjector = (M_ProjectMachine) interactItem;
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


    private void OpenProjectorUI(Texture firstTexture)
    {
        ProjectorUIFrame.gameObject.SetActive(true);
        CurrentPhotoDisplayArea.texture = firstTexture;
        
        var textureColor = CurrentPhotoDisplayArea.color;
        textureColor.a = !firstTexture ? 0 : 1;
        CurrentPhotoDisplayArea.color = textureColor;
    }


    private void CloseProjectorUI()
    {
        ProjectorUIFrame.gameObject.SetActive(false);
    }
    

    private void ChangeDisplayPhoto(Texture photo)
    {
        CurrentPhotoDisplayArea.texture = photo;
        
        var textureColor = CurrentPhotoDisplayArea.color;
        textureColor.a = !photo ? 0 : 1;
        CurrentPhotoDisplayArea.color = textureColor;
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
