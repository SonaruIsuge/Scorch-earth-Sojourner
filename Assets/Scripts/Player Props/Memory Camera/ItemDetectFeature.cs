
using System.Buffers;
using SonaruUtilities;
using UnityEngine;

public class ItemDetectFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }

    private Collider2D currentDetectItem;
    
    //private float detectHeight => owner.memoryCameraLens.orthographicSize * 2;
    //private float detectWidth => detectHeight * owner.memoryCameraLens.aspect;
    
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
        owner.DetectPoint.gameObject.SetActive(currentDetectItem);
        owner.DetectPoint.position = currentDetectItem ? owner.WorldCamera.WorldToScreenPoint(currentDetectItem.transform.position): Vector3.zero;
        //currentDetectItem = tempItem;
    }
    
    
    public ItemPhotoData DetectItem()
    {
        var targetItemData = new ItemPhotoData();
        if (!currentDetectItem) return null;
        
        var recordable = currentDetectItem.GetComponent<CameraRecordableBehaviour>();
        if (!recordable) return null;

        targetItemData.TargetItemId = recordable.ItemData.ItemId;
        targetItemData.PositionInPhoto = owner.WorldCamera.WorldToScreenPoint(currentDetectItem.transform.position) - owner.PhotoFrameRectTrans.position;
        

        return targetItemData;
    }
}

