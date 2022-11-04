


using Cysharp.Threading.Tasks;
using SonaruUtilities;
using UnityEngine;

public class PhotoTakeFeature : ICameraFeature
{
    public bool enable { get; private set; }
    public MemoryCamera owner { get; private set; }
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

        var currentRT = RenderTexture.active;
        RenderTexture.active= owner.memoryCameraFilm;
        screenCapture.ReadPixels(new Rect(0, 0, owner.memoryCameraFilm.width, owner.memoryCameraFilm.height),0,0);
        screenCapture.Apply();
        RenderTexture.active = currentRT;
        
        underCaptureProgress = false;
        return screenCapture;
    }
}
