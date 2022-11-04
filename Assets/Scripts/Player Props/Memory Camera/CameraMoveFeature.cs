
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
        
        owner.memoryCameraLens.transform.position = owner.WorldCamera.ScreenToWorldPoint(new Vector3(x, y));
    }


}
