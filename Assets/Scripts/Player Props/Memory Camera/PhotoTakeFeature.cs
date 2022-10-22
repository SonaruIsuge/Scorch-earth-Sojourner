


using Cysharp.Threading.Tasks;
using SonaruUtilities;
using UnityEngine;

public class PhotoTakeFeature : ICameraFeature
{
    public bool enable { get; private set; }
    public MemoryCamera owner { get; private set; }
    private Rect frameRect => owner.PhotoFrameRectTrans.rect;
    private Texture2D screenCapture;

    private bool underCaptureProgress;
    
    
    public void EnableFeature(bool b)
    {
        enable = b;
    }


    public PhotoTakeFeature(MemoryCamera camera)
    {
        owner = camera;
        screenCapture = new Texture2D(camera.PhotoWidth, camera.PhotoHeight, TextureFormat.RGB24, false);
        enable = true;
    }

    public async UniTask<Texture2D> TakePhoto()
    {
        if (!enable) return null;
        if (underCaptureProgress) return null;
        
        var photo = await CaptureScreenShot();
        
        return photo;
    }
    
    
    private async UniTask<Texture2D> CaptureScreenShot()
    {
        underCaptureProgress = true;
        await UniTask.WaitForEndOfFrame(owner);
        
        //rect.x -> distance from left , rect.y -> distance from top
        var rect = UnityTool.RectTransformToScreenSpace(owner.PhotoFrameRectTrans);

        var bottomBound = Screen.height - frameRect.height - rect.y;
        rect.y = bottomBound;
        screenCapture.ReadPixels(rect, 0,0,false);
        screenCapture.Apply();
        underCaptureProgress = false;

        ShowPhoto();

        return screenCapture;
    }


    private void ShowPhoto()
    {
        if(!owner.PhotoDisplayArea) return;
        owner.PhotoDisplayArea.texture = screenCapture;
        owner.PhotoDisplayArea.SetNativeSize();
    }
}
