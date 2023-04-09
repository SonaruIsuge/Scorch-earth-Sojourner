using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Player input
    private InputControl inputControl;
    public CommonInput CommonInput { get; private set; }
    public IInput CurrentInput { get; private set; }
    private Dictionary<InputType, IInput> allInput;
    
    // Player components
    [field: Header("Components")]
    [field: SerializeField] public PlayerMove PlayerMove { get; private set; }
    [field: SerializeField] public PlayerInteractHandler InteractHandler { get; private set; }

    // Player equipments
    [Header("Props")]
    public MemoryCamera MemoryCamera;
    public AlbumBook AlbumBook;

    // Player use prop state machine
    [Header("State")]
    [SerializeField] private UsingProp currentState;
    private Dictionary<UsingProp, IPropState> stateDict;
    
    // Player equip prop event
    public event Action<IPlayerProp> OnPropEquipped;


    void Awake()
    {
        inputControl = new InputControl();
        CommonInput = new CommonInput(inputControl);
        
        allInput = new Dictionary<InputType, IInput>()
        {
            {InputType.Player, new PlayerInputSystemInput(inputControl)},
            {InputType.MemoryCamera, new MemoryCameraInput(inputControl)},
            {InputType.AlbumBook, new AlbumInput(inputControl)},
        };

        CurrentInput = allInput[InputType.Player];
        
        //InteractHandler = GetComponent<PlayerInteractHandler>();
        //PlayerMove = GetComponent<PlayerMove>();

        stateDict = new Dictionary<UsingProp, IPropState>()
        {
            {UsingProp.None, new PlayerSelfState(this)},
            {UsingProp.MemoryCamera, new UseMemoryCameraState(this)},
            {UsingProp.AlbumBook, new UseAlbumBookState(this)},
            {UsingProp.Projector, new UseProjectorState(this)},
            {UsingProp.StartTransition, new StartTransitionState(this)}
        };
    }
    

    void OnDisable()
    {
        CurrentInput?.Unregister();
    }

    // Start is called before the first frame update
    public void InitPlayer()
    {
        CurrentInput?.Register();
        
        EquipProp(MemoryCamera);
        EquipProp(AlbumBook);

        ChangePropState(UsingProp.StartTransition);
        
        // register events in props
        MemoryCamera.OnRecordableItemPhotoTake += OnRecordableItemPhotoTake;
        AlbumBook.OnGetNewMemo += CheckNewMemo;
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        stateDict[currentState].StayState();
    }


    public void ChangePropState(UsingProp newState)
    {
        if(stateDict[currentState] != null ) stateDict[currentState].ExitState();
        if(stateDict.ContainsKey(newState)) currentState = newState;
        stateDict[currentState].EnterState();
    }


    public void EnableInputType(InputType newType)
    {
        foreach (var inputType in allInput.Values) inputType.EnableInput(false);
        CurrentInput = allInput[newType];
        CurrentInput.EnableInput(true);
    }


    private void EquipProp(IPlayerProp prop)
    {
        prop.Equip(this);
        OnPropEquipped?.Invoke(prop);
    }


    private void OnRecordableItemPhotoTake(Texture photo, ItemPhotoData data)
    {
        AlbumBook.CurrentChoosePhotoIndex = AlbumBook.GetLastPhotoIndex();
        AlbumBook.SetCurrentPage(AlbumPage.Photo);
        ChangePropState(UsingProp.AlbumBook);
    }


    private void CheckNewMemo(MemoData memo)
    {
        AlbumBook.CurrentChooseMemoIndex = AlbumBook.GetLastMemoIndex();
        AlbumBook.SetCurrentPage(AlbumPage.Memo);
        ChangePropState(UsingProp.AlbumBook);
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
