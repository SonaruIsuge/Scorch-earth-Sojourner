
using System.Buffers;
using SonaruUtilities;
using UnityEngine;

public class ItemDetectFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }

    public Collider2D currentDetectItem { get; private set; }

    public void EnableFeature(bool b)
    {
        enable = b;
    }


    public ItemDetectFeature(MemoryCamera camera)
    {
        owner = camera;
    }


    public void TryDetectItem()
    {
        var cameraWorldSize = UnityTool.GetOrthographicCameraWorldSize(owner.memoryCameraLens);
        var detectItems = 
            Physics2D.OverlapBoxAll(owner.memoryCameraLens.transform.position, cameraWorldSize, 0);
        if(detectItems.Length <= 0) return;
        
        var minDisFromPhotoCenter = Mathf.Infinity;
        currentDetectItem = null;
        foreach (var item in detectItems)
        {
            if (!item.TryGetComponent<CameraRecordableBehaviour>(out var recordItem)) continue;
            if(recordItem.IsClone) continue;

            var itemDistanceFromCenter =
                Vector2.Distance(owner.memoryCameraLens.transform.position, item.transform.position);
                
            if (itemDistanceFromCenter >= minDisFromPhotoCenter) continue;
            minDisFromPhotoCenter = itemDistanceFromCenter;
            currentDetectItem = item;
        }
        
        //currentDetectItem = tempItem;
    }
    
    
    public ItemPhotoData DetectItem()
    {
        if (!currentDetectItem) return null;
        
        var recordable = currentDetectItem.GetComponent<CameraRecordableBehaviour>();
        if (!recordable) return null;

        var targetItemData = new ItemPhotoData
        {
            TargetItemId = recordable.ItemData.ItemId,
            PositionFromCenter = currentDetectItem.transform.position - owner.memoryCameraLens.transform.position,
            cameraOrthoSize = owner.WorldCamera.orthographicSize
        };
        return targetItemData;
    }
}

