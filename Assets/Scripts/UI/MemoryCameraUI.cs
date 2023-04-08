using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCameraUI : MonoBehaviour
{
    private MemoryCamera memoryCamera;

    [SerializeField] private RectTransform PhotoTakeOuterFrame;
    [SerializeField] private RectTransform PhotoFrameRectTrans;
    [SerializeField] private RectTransform DetectHint;

    public event Action<bool> OnEnableUI;


    public void RegisterMemoryCameraUI(IPlayerProp prop)
    {
        if( prop is not MemoryCamera memoryCam ) return;

        memoryCamera = memoryCam;
        
        PhotoFrameRectTrans.sizeDelta = new Vector2(memoryCamera.PhotoWidth, memoryCamera.PhotoHeight);
        DetectHint.sizeDelta *= (float)memoryCamera.PhotoWidth / 1152;
        PhotoTakeOuterFrame.gameObject.SetActive(false);
        PhotoFrameRectTrans.gameObject.SetActive(false);
        DetectHint.transform.gameObject.SetActive(false);

        memoryCamera.OnPhotoFrameToggleEnable += CameraUIEnable;
        memoryCamera.OnPhotoFrameMove += CameraUIMove;
        memoryCamera.OnPhotoTake += CameraUIPhotoTake;
        memoryCamera.OnRecordableDetect += CameraUIDetectItem;
    }


    private void CameraUIEnable(bool enable)
    {
        gameObject.SetActive(enable);
        PhotoFrameRectTrans.gameObject.SetActive(enable);
        PhotoTakeOuterFrame.gameObject.SetActive(enable);
        OnEnableUI?.Invoke(enable);
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
        DetectHint.gameObject.SetActive(item);
    }
}
