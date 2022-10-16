
using UnityEngine;

public class CameraMoveFeature : ICameraFeature
{
    public MemoryCamera owner { get; private set; }
    public bool enable { get; private set; }


    public CameraMoveFeature(MemoryCamera camera)
    {
        owner = camera;
        enable = true;
    }
    
    
    public void EnableFeature(bool b)
    {
            enable = b;
    }
    
    
    public void MoveFrame(float x, float y)
    {
        if(!enable) return;
        
        x = Mathf.Clamp(x, owner.PhotoFrameRectTrans.rect.width / 2, Screen.width - owner.PhotoFrameRectTrans.rect.width / 2);
        y = Mathf.Clamp(y, owner.PhotoFrameRectTrans.rect.height / 2, Screen.height - owner.PhotoFrameRectTrans.rect.height / 2);
        owner.PhotoFrameRectTrans.position = new Vector2(x, y);
    }


}
