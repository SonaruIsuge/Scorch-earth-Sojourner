using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Player components
    private InputControl InputControl;
    public IInput CurrentInput { get; private set; }
    public CommonInput CommonInput { get; private set; }
    public Dictionary<InputType, IInput> AllInput;
    //public IInput PlayerInput { get; private set; }
    
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
        InputControl = new InputControl();
        CommonInput = new CommonInput(InputControl);
        
        AllInput = new Dictionary<InputType, IInput>()
        {
            {InputType.Player, new PlayerInputSystemInput(InputControl)},
            {InputType.MemoryCamera, new MemoryCameraInput(InputControl)},
            {InputType.AlbumBook, new AlbumInput(InputControl)},
        };

        CurrentInput = AllInput[InputType.Player];
        //PlayerInput = new PlayerInputSystemInput(InputControl);
        
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
        CurrentInput?.Register();
    }

    void OnDisable()
    {
        CurrentInput?.Unregister();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(MemoryCamera) MemoryCamera.Equip(this);
        if(AlbumBook) AlbumBook.Equip(this);

        ChangePropState(UsingProp.None);
    }

    // Update is called once per frame
    void Update()
    {
        CommonInput.ReadInput();
        
        stateDict[currentState].StayState();
    }


    public void ChangePropState(UsingProp newState, MoreInfo moreInfo = MoreInfo.None)
    {
        if(stateDict[currentState] != null ) stateDict[currentState].ExitState();
        if(stateDict.ContainsKey(newState)) currentState = newState;
        stateDict[currentState].EnterState(moreInfo);
    }


    public void EnableInputType(InputType newType)
    {
        foreach (var inputType in AllInput.Values) inputType.EnableInput(false);
        CurrentInput = AllInput[newType];
        CurrentInput.EnableInput(true);
    }
    
    
    // Delay function
    public void DelayDo(Action onComplete, float delay)
    {
        StartCoroutine(DelayDoInner(delay, onComplete));
    }

    public void DelayDo<T>(Action<T> onComplete, T param1, float delay)
    {
        StartCoroutine(DelayDoInner<T>(delay, onComplete, param1));
    }

    private IEnumerator DelayDoInner(float delay, Action onComplete = null)
    {
        yield return new WaitForSeconds(delay);
        
        onComplete?.Invoke();
    }

    private IEnumerator DelayDoInner<T>(float delay, Action<T> onComplete, T param1)
    {
        yield return new WaitForSeconds(delay);
        
        onComplete?.Invoke(param1);
    }
}
