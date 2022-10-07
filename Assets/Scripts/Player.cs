using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player components
    private IInput PlayerInput;
    private PlayerMove playerMove;
    private PlayerInteractHandler interactHandler;
    
    // Player equipments
    [SerializeField] private MemoryCamera memoryCamera;
    
    
    //[field: SerializeReference]public bool isEnableControl { get; private set; }

    void Awake()
    {
        PlayerInput = new PlayerInputSystemInput();
        interactHandler = GetComponent<PlayerInteractHandler>();
        playerMove = GetComponent<PlayerMove>();

        memoryCamera = GetComponentInChildren<MemoryCamera>();
    }

    void OnEnable()
    {
        //EnableControl(true, PlayerInput);
        PlayerInput?.Register();
    }

    void OnDisable()
    {
        //EnableControl(false, PlayerInput);
        PlayerInput?.Unregister();
    }

    // Start is called before the first frame update
    void Start()
    {
        memoryCamera.Equip(this);
        memoryCamera.ShowCameraFrame(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.ReadInput();
        EnablePhotoTake(PlayerInput.enablePhoto);
        
        playerMove.UpdateMove(PlayerInput.horizontal, PlayerInput.vertical);
        interactHandler.UpdateSelect();
        
        
        if (PlayerInput.interact)
        {
            interactHandler.Interact();
        }

        if (memoryCamera && PlayerInput.enablePhoto)
        {
            memoryCamera.MoveCamera(PlayerInput.controlFrameArea.x, PlayerInput.controlFrameArea.y);
            
            if(PlayerInput.takePhoto) memoryCamera.TakePhoto();
        }
        
    }

    public IInput GetInput() => PlayerInput;

    private void EnablePhotoTake(bool enable)
    {
        memoryCamera.enabled = enable;
        memoryCamera.ShowCameraFrame(enable);
        playerMove.EnableMove(!enable);                 // if enable photo taker, locked move
        interactHandler.EnableInteract(!enable);        // if enable photo taker, locked interact
    }

    
}
