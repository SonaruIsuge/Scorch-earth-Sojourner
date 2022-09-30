
using SonaruUtilities;
using UnityEngine;

public class ItemDetectFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }

    private Camera cc;
    
    
    public void EnableFeature(bool b)
    {
        enable = b;
    }


    public ItemDetectFeature(MemoryCamera camera)
    {
        owner = camera;
        cc = Camera.main;
    }


    public CameraRecordableBehaviour DetectItem()
    {
        CameraRecordableBehaviour targetItem = null;
        var point = cc.ScreenToWorldPoint(owner.PhotoFrameRectTrans.position);
        owner.GetWidthHeightInWorld(out var worldWidth, out var worldHeight);
        
        var detectItems = Physics2D.OverlapBoxAll(point, new Vector2(worldWidth,  worldHeight), 0);
        foreach (var item in detectItems)
        {
            var recordItem = item.GetComponent<CameraRecordableBehaviour>();
            if(!recordItem) continue;
            
            recordItem.CameraHit();
            var itemScreenPos = cc.WorldToScreenPoint(item.transform.position);
            //Debug.Log(itemScreenPos - owner.PhotoFrameRectTrans.position);
        }

        return targetItem;
    }
}


public struct ItemPhotoData
{
    public int Id;
    public Vector2 PositionInPhoto;
}
