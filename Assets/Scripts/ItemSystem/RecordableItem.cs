using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recordable/Item", fileName = "New Item")]
public class RecordableItem : ScriptableObject
{
    public int ItemId;
    public string ItemName;
    public GameObject ItemObject;

    public ICameraRecordable GetRecordable() => ItemObject != null ? ItemObject.GetComponent<ICameraRecordable>() : null;

}
