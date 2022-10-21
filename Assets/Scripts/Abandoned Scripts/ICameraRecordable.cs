
using UnityEngine;

public interface ICameraRecordable
{
    GameObject targetObject { get; }


    void OnCameraHit();

    void OnPhotoUse();
}
