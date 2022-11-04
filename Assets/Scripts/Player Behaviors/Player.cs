using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Player input
    private InputControl InputControl;
    public CommonInput CommonInput { get; private set; }
    public IInput CurrentInput { get; private set; }
    private Dictionary<InputType, IInput> AllInput;
    
    // Player components
    public PlayerMove PlayerMove { get; private set; }
    public PlayerInteractHandler InteractHandler { get; private set; }

    // Player equipments
    public MemoryCamera MemoryCamera;
    public AlbumBook AlbumBook;

    // Player use prop state machine
    private UsingProp currentState;
    private Dictionary<UsingProp, IPropState> stateDict;
    
    // Player equip prop event
    public event Action<IPlayerProp> OnPropEquiped;
    

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
        if (MemoryCamera)
        {
            MemoryCamera.Equip(this);
            OnPropEquiped?.Invoke(MemoryCamera);
        }

        if (AlbumBook)
        {
            AlbumBook.Equip(this);
            OnPropEquiped?.Invoke(AlbumBook);
        }

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
    

    #region Delay function
    
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

    #endregion
}
