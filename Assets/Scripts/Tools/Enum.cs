

public enum InputType
{
    Player,
    MemoryCamera,
    AlbumBook,
}


public enum UsingProp
{
    MemoryCamera,
    AlbumBook,
    Projector,
    None,
    StartTransition
}


public enum SceneIndex
{
    StartScene,
    Level,
    Indoor,
    Outdoor
}


public enum AlbumPage
{
    Photo,
    Memo
}


public enum Room
{
    EmptyWarehouse,
    AbandonedCorridor,
    ReceptionRoom,
    LeftCorridor,
    RightCorridor,
    LeftHall,
    RightHall,
    LeftWarehouse,
    PowerSupplyRoom,
    Bedroom,
    LeftElevatorCorridor,
    RightElevatorCorridor,
    ElevatorRoom,
    LeftCorner,
    RightCorner
}


public enum KeyEvent
{
    WarehouseKeyGet,
    DoorEatenBySlime,
    EmergencyPowerOpened
}


public enum AudioType
{
    //Player move
    PlayerMove,
    //Interact
    DoorOpen,
    PickPaper,
    ProjectorOpen,
    //Camera
    CameraOpen,
    TakePhoto,
    //AlbumBook
    BookOpen,
    ChangePage,
    //Item
    BedroomSound,
    SlimeEscape,
    //UI
    CharPrint,
    ButtonClick
}