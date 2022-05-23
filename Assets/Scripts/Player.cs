using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IControllable
{
    private IInput PlayerInput;
    private PlayerMove playerMove;
    private PlayerInteractHandler interactHandler;
    
    [field: SerializeReference]public bool isEnableControl { get; private set; }

    void Awake()
    {
        PlayerInput = new PlayerInputSystemInput();
        interactHandler = GetComponent<PlayerInteractHandler>();
        playerMove = GetComponent<PlayerMove>();
    }

    void OnEnable()
    {
        EnableControl(true, PlayerInput);
    }

    void OnDisable()
    {
        EnableControl(false, PlayerInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.ReadInput();
    }
    
    public void EnableControl(bool enable, IInput input = null)
    {
        isEnableControl = enable;
        if (enable) input?.Create();
        else input?.Destroy();
    }

    public IInput GetInput() => PlayerInput;
    
}
