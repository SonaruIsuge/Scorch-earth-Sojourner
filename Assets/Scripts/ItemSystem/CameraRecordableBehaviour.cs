using System;
using UnityEngine;


public abstract class CameraRecordableBehaviour : MonoBehaviour
{
    public RecordableItem ItemData;
    [field: SerializeField] public bool IsClone { get; private set; }


    private void Awake()
    {
        IsClone = false;
    }

    public virtual void CameraHit()
    {
        //Debug.Log($"Hit Item : {ItemData.ItemName}");
    }

    public virtual void ItemUse(M_ProjectMachine machine)
    {
        IsClone = true;
        //Debug.Log($"Use Photo Item : {ItemData.ItemName}");
    }
}
