using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugComponent : MonoBehaviour
{
    public CanvasScaler matchCanvas;
    public float pixelHeightOnCanvas = 256f;
    public SpriteRenderer sprite;

    private void Start() 
    {
        
    }

    void Update () {
        // You might not need to call this every Update,
        // just when something changes size/configuration.
        UpdateScale();
    }    

    void UpdateScale() {
        // The canvas will try to scale its reference resolution
        // to match the screen's dimensions in either x or y.
        // (Assuming it's in Overlay mode or using a fullscreen camera
        // - if rendering to a smaller rect, use that pixel rect instead)
        Vector2 scaleFactorRange = new Vector2(
            Screen.width / matchCanvas.referenceResolution.x,
            Screen.height / matchCanvas.referenceResolution.y);

        // When the screen's aspect ratio isn't the same as the reference,
        // the canvas picks between two scale factors with matchWidthOrHeight
        float scaleFactor = Mathf.Lerp(
            scaleFactorRange.x,
            scaleFactorRange.y,
            matchCanvas.matchWidthOrHeight);

        // We can now compute how much it will scale our in-canvas
        // dimensions to produce on-screen pixel dimensions.
        float heightInScreenPixels = pixelHeightOnCanvas * scaleFactor;

        // For the next part, we need to know what camera we're
        // being rendered by - consider caching this if it's constant.
        Camera cam = Camera.main;

        // We'll convert the screen height into a fraction of the camera's
        // vertical span (which might be less than the screen's if rendering
        // to a smaller viewport rect).
        float heightAsViewFraction = heightInScreenPixels / cam.pixelRect.height;

        // Now we can convert that to a desired world height by multiplying
        // by the camera's vertical size - note that orthographicSize is
        // only half the height of the camera's view, hence the 2x.    
        float heightInWorldUnits = 2f * cam.orthographicSize * heightAsViewFraction;

        // Lastly, we need to know how big "this" sprite is at scale = 1.
        float nativeWorldHeight = sprite.sprite.rect.height / sprite.sprite.pixelsPerUnit;
        Debug.Log(nativeWorldHeight);
        // And our scale factor is the multiplier that gets us from our
        // native world size to the desired world size.
        transform.localScale = Vector3.one * heightInWorldUnits / nativeWorldHeight;
    }
}
