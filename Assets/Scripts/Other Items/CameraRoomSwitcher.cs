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
        var cameraPos = mainCamera.transform.position;
        cameraPos.x = data.RoomCenter.position.x;
        cameraPos.y = data.RoomCenter.position.y;
        
        mainCamera.transform.DOMove(cameraPos, .05f);
        mainCamera.orthographicSize = data.CameraSizeRef.sprite.bounds.size.y / 2;
    }
}
