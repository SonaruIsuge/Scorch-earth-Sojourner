﻿
using SonaruUtilities;
using UnityEngine;

public class ItemDetectFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }

    private Camera targetCamera;
    
    
    public void EnableFeature(bool b)
    {
        enable = b;
    }


    public ItemDetectFeature(MemoryCamera camera)
    {
        owner = camera;
        targetCamera = owner.TargetCamera;
    }


    public ItemPhotoData DetectItem()
    {
        var getRecordableItem = false;
        var point = targetCamera.ScreenToWorldPoint(owner.PhotoFrameRectTrans.position);
        owner.GetWidthHeightInWorld(out var worldWidth, out var worldHeight);
        
        var detectItems = Physics2D.OverlapBoxAll(point, new Vector2(worldWidth,  worldHeight), 0);
        
        if (detectItems.Length <= 0) return null;
        
        var targetItemData = new ItemPhotoData();
        var minDisFromPhotoCenter = Mathf.Infinity;
        
        foreach (var item in detectItems)
        {
            var recordItem = item.GetComponent<CameraRecordableBehaviour>();
            if(!recordItem) continue;

            recordItem.CameraHit();
            getRecordableItem = true;
            
            var itemScreenPos = targetCamera.WorldToScreenPoint(item.transform.position);
            var itemScreenCenterDiff = Vector2.Distance(itemScreenPos, owner.PhotoFrameRectTrans.position);
            
            if (itemScreenCenterDiff >= minDisFromPhotoCenter) continue;
            
            minDisFromPhotoCenter = itemScreenCenterDiff;
            targetItemData.TargetItemId = recordItem.ItemData.ItemId;
            targetItemData.PositionInPhoto = itemScreenPos - owner.PhotoFrameRectTrans.position;
        }

        if (!getRecordableItem) targetItemData = null;

        return targetItemData;
    }
}

