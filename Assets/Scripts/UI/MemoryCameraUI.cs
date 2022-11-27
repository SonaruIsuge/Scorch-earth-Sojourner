using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCameraUI : MonoBehaviour
{
    private Player player;
    private MemoryCamera memoryCamera;

    private GeneralUI generalUI;
    
    private Camera WorldCamera => Camera.main;
    
    [SerializeField] private RectTransform PhotoTakeOuterFrame;
    [SerializeField] private RectTransform PhotoFrameRectTrans;
    [SerializeField] private RectTransform DetectHint;
    [SerializeField] private Button TakePhotoBtn;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        generalUI = GetComponent<GeneralUI>();
    }


    private void OnEnable()
    {
        player.OnPropEquipped += RegisterMemoryCameraUI;
    }


    private void OnDisable()
    {
        player.OnPropEquipped -= RegisterMemoryCameraUI;
    }
    

    private void RegisterMemoryCameraUI(IPlayerProp prop)
    {
        if( !(prop is MemoryCamera) ) return;

        memoryCamera = (MemoryCamera) prop;
        
        PhotoFrameRectTrans.sizeDelta = new Vector2(memoryCamera.PhotoWidth, memoryCamera.PhotoHeight);
        DetectHint.sizeDelta *= (float)memoryCamera.PhotoWidth / 1152;
        PhotoTakeOuterFrame.gameObject.SetActive(false);
        PhotoFrameRectTrans.gameObject.SetActive(false);
        DetectHint.transform.gameObject.SetActive(false);

        memoryCamera.OnPhotoFrameToggleEnable += CameraUIEnable;
        memoryCamera.OnPhotoFrameMove += CameraUIMove;
        memoryCamera.OnPhotoTake += CameraUIPhotoTake;
        memoryCamera.OnRecordableDetect += CameraUIDetectItem;
        
        TakePhotoBtn.onClick.AddListener(() => memoryCamera.EnableProp(true));
    }


    private void CameraUIEnable(bool enable)
    {
        PhotoFrameRectTrans.gameObject.SetActive(enable);
        PhotoTakeOuterFrame.gameObject.SetActive(enable);
        generalUI.EnableGeneralUI(!enable);
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
        //DetectHint.position = item ? WorldCamera.WorldToScreenPoint(item.transform.position): Vector3.zero;
    }
}
