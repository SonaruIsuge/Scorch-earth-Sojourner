using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player components
    public IInput PlayerInput { get; private set; }
    public PlayerMove PlayerMove { get; private set; }
    public PlayerInteractHandler InteractHandler { get; private set; }

    // Player equipments
    public MemoryCamera MemoryCamera;
    public AlbumBook AlbumBook;

    // Player use prop state machine
    private UsingProp currentState;
    private Dictionary<UsingProp, IPropState> stateDict;
    

    void Awake()
    {
        PlayerInput = new PlayerInputSystemInput();
        InteractHandler = GetComponent<PlayerInteractHandler>();
        PlayerMove = GetComponent<PlayerMove>();

        MemoryCamera = GetComponentInChildren<MemoryCamera>();
        AlbumBook = GetComponentInChildren<AlbumBook>();

        stateDict = new Dictionary<UsingProp, IPropState>()
        {
            {UsingProp.None, new PlayerSelfState(this)},
            {UsingProp.MemoryCamera, new UseMemoryCameraState(this)},
            {UsingProp.AlbumBook, new UseAlbumBookState(this)},
        };
    }

    void OnEnable()
    {
        PlayerInput?.Register();
    }

    void OnDisable()
    {
        PlayerInput?.Unregister();
    }

    // Start is called before the first frame update
    void Start()
    {
        MemoryCamera.Equip(this);
        AlbumBook.Equip(this);

        currentState = UsingProp.None;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.ReadInput();
        
        stateDict[currentState].StayState();
    }


    public void ChangePropState(UsingProp newState)
    {
        if(stateDict[currentState] != null ) stateDict[currentState].ExitState();
        if(stateDict.ContainsKey(newState)) currentState = newState;
        stateDict[currentState].EnterState();
    }
}
