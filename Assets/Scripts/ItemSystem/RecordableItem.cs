using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recordable/Item", fileName = "New Item")]
public class RecordableItem : ScriptableObject
{
    public int ItemId;
    public string ItemName;
    public GameObject ItemObject;
    [TextArea(5, 10)]
    public string Description;
    
    public CameraRecordableBehaviour GetRecordable => ItemObject != null ? ItemObject.GetComponent<CameraRecordableBehaviour>() : null;

}
