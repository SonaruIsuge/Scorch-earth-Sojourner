using UnityEngine;

namespace SonaruUtilities
{
    public static class UnityTool
    {
        public static Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
            rect.x -= (transform.pivot.x * size.x);
            rect.y -= ((1.0f - transform.pivot.y) * size.y);
            return rect;
        }


        public static Vector2 GetOrthographicCameraWorldSize(Camera camera)
        {
            if (!camera.orthographic) return Vector2.zero;

            var height = camera.orthographicSize * 2.0f;   
            return new Vector2(camera.aspect * height, height);
        }
    }
}