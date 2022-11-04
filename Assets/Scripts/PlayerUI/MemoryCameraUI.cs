using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCameraUI : MonoBehaviour
{
    private Player player;
    private MemoryCamera memoryCamera;
    private Camera WorldCamera => Camera.main;
    
    [SerializeField] public RectTransform PhotoTakeOuterFrame;
    [SerializeField] public RectTransform PhotoFrameRectTrans;
    [SerializeField] public RectTransform DetectPoint;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    private void OnEnable()
    {
        player.OnPropEquiped += RegisterMemoryCameraUI;
    }


    private void RegisterMemoryCameraUI(IPlayerProp prop)
    {
        if( !(prop is MemoryCamera) ) return;

        memoryCamera = (MemoryCamera) prop;
        
        PhotoFrameRectTrans.sizeDelta = new Vector2(memoryCamera.PhotoWidth, memoryCamera.PhotoHeight);
        PhotoTakeOuterFrame.gameObject.SetActive(false);
        PhotoFrameRectTrans.gameObject.SetActive(false);
        DetectPoint.transform.gameObject.SetActive(false);

        memoryCamera.OnPhotoFrameToggleEnable += CameraUIEnable;
        memoryCamera.OnPhotoFrameMove += CameraUIMove;
        memoryCamera.OnPhotoTake += CameraUIPhotoTake;
        memoryCamera.OnRecordableDetect += CameraUIDetectItem;
    }


    private void CameraUIEnable(bool enable)
    {
        PhotoFrameRectTrans.gameObject.SetActive(enable);
        PhotoTakeOuterFrame.gameObject.SetActive(enable);
    }


    private void CameraUIMove(float x, float y)
    {
        PhotoFrameRectTrans.position = new Vector2(x, y);
    }


    private void CameraUIPhotoTake()
    {
        // ui do nothing
    }


    private void CameraUIDetectItem(Collider2D item)
    {
        DetectPoint.gameObject.SetActive(item);
        DetectPoint.position = item ? WorldCamera.WorldToScreenPoint(item.transform.position): Vector3.zero;
    }
}
