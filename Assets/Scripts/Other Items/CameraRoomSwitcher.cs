using System;
using DG.Tweening;
using UnityEngine;


public class CameraRoomSwitcher : MonoBehaviour, IRoomSwitcher
{
    [field: SerializeField] public SpriteRenderer changeRoom { get; private set; }
    [field: SerializeField] public Room currentRoom { get; private set; }
    [SerializeField] private Transform levelCenter;
    
    private Camera mainCamera;
    private RoomsController roomController;
    private Texture roomTex => changeRoom.sprite.texture;
    private Vector2 levelCenterPos => levelCenter.position;

    private void Awake()
    {
        mainCamera = Camera.main;
        roomController = GetComponentInParent<RoomsController>();
    }

    private void OnEnable()
    {
        roomController.OnRoomChange += SetOnRoomChange;
    }
    
    
    private void OnDisable()
    {
        roomController.OnRoomChange -= SetOnRoomChange;
    }


    public void SetOnRoomChange(RoomData data)
    {
        var centerPos = data.RoomCenter.position;
        var cameraTrans = mainCamera.transform;
        
        var newCameraPos = new Vector3(centerPos.x, centerPos.y, cameraTrans.position.z);
        cameraTrans.position = newCameraPos;
        mainCamera.orthographicSize = data.CameraSizeRef.sprite.bounds.size.y / 2;
    }
}
