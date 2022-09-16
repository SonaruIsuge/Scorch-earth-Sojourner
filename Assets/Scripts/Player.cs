using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IInput PlayerInput;
    private PlayerMove playerMove;
    private PlayerInteractHandler interactHandler;
    private PlayerPhotoTaker playerPhotoTaker;
    
    //[field: SerializeReference]public bool isEnableControl { get; private set; }

    void Awake()
    {
        PlayerInput = new PlayerInputSystemInput();
        interactHandler = GetComponent<PlayerInteractHandler>();
        playerMove = GetComponent<PlayerMove>();
        playerPhotoTaker = GetComponent<PlayerPhotoTaker>();
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

        if (PlayerInput.enablePhoto)
        {
            playerPhotoTaker.MoveFrame(PlayerInput.controlFrameArea.x, PlayerInput.controlFrameArea.y);
            
            if(PlayerInput.takePhoto) playerPhotoTaker.TakePhoto();
        }
        
    }

    public IInput GetInput() => PlayerInput;

    private void EnablePhotoTake(bool enable)
    {
        playerPhotoTaker.enabled = enable;
        playerMove.EnableMove(!enable);
    }
}
