using DG.Tweening;
using UnityEngine;


public class CameraRoomSwitcher : MonoBehaviour, IRoomSwitcher
{
    [field: SerializeField] public SpriteRenderer changeRoom { get; private set; }
    [SerializeField] private Transform levelCenter;
    
    private Camera mainCamera;
    private Texture roomTex => changeRoom.sprite.texture;
    private Vector2 levelCenterPos => levelCenter.position;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    

    public void SetOnRoomChange()
    {
        var cameraPos = mainCamera.transform.position;
        cameraPos.x = levelCenterPos.x;
        cameraPos.y = levelCenterPos.y;
        
        mainCamera.transform.DOMove(cameraPos, .05f);
        mainCamera.orthographicSize = changeRoom.sprite.bounds.size.y / 2;
    }
}
