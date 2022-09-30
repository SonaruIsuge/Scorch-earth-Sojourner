using UnityEngine;


public abstract class CameraRecordableBehaviour : MonoBehaviour
{
    public RecordableItem ItemData;

    public virtual void CameraHit()
    {
        Debug.Log($"Hit Item : {ItemData.ItemName}");
    }

    public virtual void ItemUse()
    {
        Debug.Log($"Use Photo Item : {ItemData.ItemName}");
    }
}
